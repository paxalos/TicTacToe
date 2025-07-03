using Cysharp.Threading.Tasks;
using Game.Enums;
using Game.Models;
using Game.PlayerLogics;
using System.Linq;
using Zenject;

namespace Game.Presenters
{
    public class GamePresenter
    {
        [Inject] private GameModel model;
        [Inject] private PlayerLogicController playerLogicController;

        private const float ELEMENT_DRAW_DELAY = 20f / 60f;
        private const float WIN_LINE_DRAW_DELAY = 20f / 60f;

        public void SetPlayers(PlayerType[] playerTypes)
        {
            playerLogicController.SetPlayers(playerTypes);
        }

        public void SelectCell(int cellIndex)
        {
            model.SetPlayerChosenCell(cellIndex);
        }

        public async void StartGame()
        {
            model.ResetCells();
            playerLogicController.ResetPlayers();

            var gameResult = GameResultType.InGame;
            int winningLineIndex = -1;
            while(gameResult == GameResultType.InGame)
            {
                await playerLogicController.PlayerTurn().SuppressCancellationThrow();
                await UniTask.WaitForSeconds(ELEMENT_DRAW_DELAY).SuppressCancellationThrow();
                gameResult = CalculateGameResult(out winningLineIndex);
            }

            await UniTask.WaitForSeconds(WIN_LINE_DRAW_DELAY).SuppressCancellationThrow();

            model.SetGameResult(gameResult, winningLineIndex);
        }

        private GameResultType CalculateGameResult(out int winLineIndex)
        {
            var cellElements = model.CellElements;

            int[][] winningsCombinations = new int[][]
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8},
                new int[] {0, 3, 6},
                new int[] {1, 4, 7},
                new int[] {2, 5, 8},
                new int[] {0, 4, 8},
                new int[] {2, 4, 6},
            };

            for (int i = 0; i < winningsCombinations.Length; i++)
                if (IsWinningCombination(cellElements,
                                         winningsCombinations[i],
                                         out var gameResult))
                {
                    winLineIndex = i;
                    return gameResult;
                }

            winLineIndex = -1;

            if (cellElements.Any(cellElement => cellElement == ElementType.Empty))
                return GameResultType.InGame;

            return GameResultType.Draw;
        }

        private bool IsWinningCombination(ElementType[] cellElements,
                                          int[] indexes,
                                          out GameResultType gameResultType)
        {
            gameResultType = GameResultType.InGame;
            var combination = indexes.Select(index => cellElements[index]);

            if (combination.All(element => element != ElementType.Empty))
            {
                var elementForCompare = combination.First();
                if (combination.All(element => element == elementForCompare))
                {
                    switch (elementForCompare)
                    {
                        case ElementType.Cross:
                            gameResultType = GameResultType.CrossWin;
                            break;
                        case ElementType.Circle:
                            gameResultType = GameResultType.CircleWin;
                            break;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
