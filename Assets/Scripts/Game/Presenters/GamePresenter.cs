using Cysharp.Threading.Tasks;
using Game.Enums;
using Game.Models;
using Game.PlayerLogics;
using Zenject;

namespace Game.Presenters
{
    public class GamePresenter
    {
        [Inject] private GameModel model;
        [Inject] private PlayerLogicPresenter playerLogicPresenter;
        [Inject] private GameWinPresenter gameWinPresenter;

        private const float ELEMENT_DRAW_DELAY = 20f / 60f;
        private const float WIN_LINE_DRAW_DELAY = 20f / 60f;

        public void SetPlayers(PlayerType[] playerTypes)
        {
            playerLogicPresenter.SetPlayers(playerTypes);
        }

        public void SelectCell(int cellIndex)
        {
            model.SetPlayerChosenCell(cellIndex);
        }

        public async void StartGame()
        {
            model.ResetCells();
            playerLogicPresenter.ResetPlayers();

            var gameResult = GameResultType.InGame;
            int winningLineIndex = -1;
            while (gameResult == GameResultType.InGame)
            {
                await playerLogicPresenter.PlayerTurn().SuppressCancellationThrow();
                await UniTask.WaitForSeconds(ELEMENT_DRAW_DELAY).SuppressCancellationThrow();
                var cellElements = model.CellPlaySymbols;
                gameResult = gameWinPresenter.CalculateGameResult(cellElements, out winningLineIndex);
            }

            await UniTask.WaitForSeconds(WIN_LINE_DRAW_DELAY).SuppressCancellationThrow();

            model.SetGameResult(gameResult, winningLineIndex);
        }
    }
}
