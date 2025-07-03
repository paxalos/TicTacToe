using Game.Enums;
using Game.Views;
using System.Linq;
using Utils;
using Zenject;

namespace Game.Models
{
    public class GameModel
    {
        [Inject] private GameView view;
        private readonly ElementType[] elementsQueue = { ElementType.Cross, ElementType.Circle };
        private int[] playersWinCount = new int[PLAYERS_COUNT];
        private int drawCount;

        private const int CELLS_COUNT = 9;
        private const int PLAYERS_COUNT = 2;

        public int CurrentPlayerIndex { get; private set; }
        public ElementType[] CellElements { get; private set; } = new ElementType[CELLS_COUNT];

        public void SetPlayerChosenCell(int cellIndex)
        {
            var playerElement = elementsQueue[CurrentPlayerIndex];
            CellElements[cellIndex] = playerElement;
            view.SetElementInCell(cellIndex, playerElement);
            CurrentPlayerIndex = CurrentPlayerIndex.IncrementInRange(PLAYERS_COUNT);
        }

        public void SetGameResult(GameResultType gameResult, int winningLineIndex)
        {
            if (gameResult == GameResultType.Draw)
                view.SetDrawCount(++drawCount);
            else
            {
                int playerIndex = (int)gameResult;
                view.SetPlayerWinCount(playerIndex, ++playersWinCount[playerIndex]);
            }

            view.SetGameResult(gameResult, winningLineIndex);
        }

        public int[] GetCellIndexesForChoise()
        {
            return Enumerable.Range(0, CellElements.Length)
                             .Where(index => CellElements[index] == ElementType.Empty)
                             .ToArray();
        }

        public void ResetCells()
        {
            for (int i = 0; i < CellElements.Length; i++)
                CellElements[i] = ElementType.Empty;

            CurrentPlayerIndex = 0;
        }
    }
}