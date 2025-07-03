using Game.Enums;
using Game.Views;
using System;
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
        private ElementType[] cellElements = new ElementType[CELLS_COUNT];

        public const int CELLS_COUNT = 9;
        private const int PLAYERS_COUNT = 2;

        public int CurrentPlayerIndex { get; private set; }
        public ElementType[] CellElements
        {
            get
            {
                var result = new ElementType[CELLS_COUNT];
                Array.Copy(cellElements, result, CELLS_COUNT);
                return result;
            }
        }

        public void SetPlayerChosenCell(int cellIndex)
        {
            var playerElement = elementsQueue[CurrentPlayerIndex];
            cellElements[cellIndex] = playerElement;
            view.SetElementInCell(cellIndex, playerElement);
            CurrentPlayerIndex = CurrentPlayerIndex.IncrementInRange(PLAYERS_COUNT);
        }

        public void SetGameResult(GameResultType gameResult, int winningLineIndex)
        {
            if (gameResult == GameResultType.Draw)
                view.SetDrawCount(++drawCount);
            else
            {
                int playerIndex = GetPlayerIndexByGameResult(gameResult);
                view.SetPlayerWinCount(playerIndex, ++playersWinCount[playerIndex]);
            }

            view.SetGameResult(gameResult, winningLineIndex);
        }

        public int[] GetCellIndexesForChoise()
        {
            return Enumerable.Range(0, cellElements.Length)
                             .Where(index => cellElements[index] == ElementType.Empty)
                             .ToArray();
        }

        public void ResetCells()
        {
            for (int i = 0; i < cellElements.Length; i++)
                cellElements[i] = ElementType.Empty;

            CurrentPlayerIndex = 0;
        }

        public ElementType GetEnemyElement(ElementType element)
        {
            switch (element)
            {
                case ElementType.Cross:
                    return ElementType.Circle;
                case ElementType.Circle:
                    return ElementType.Cross;
                default:
                    return ElementType.Empty;
            }
        }

        public GameResultType GetWinningResultByPlayerIndex(int playerIndex)
        {
            switch (playerIndex)
            {
                case 0:
                    return GameResultType.CrossWin;
                case 1:
                    return GameResultType.CircleWin;
                default:
                    return GameResultType.InGame;
            }
        }

        public ElementType GetElementByPlayerIndex(int playerIndex)
        {
            switch (playerIndex)
            {
                case 0:
                    return ElementType.Cross;
                case 1:
                    return ElementType.Circle;
                default:
                    return ElementType.Empty;
            }
        }

        private int GetPlayerIndexByGameResult(GameResultType gameResult)
        {
            switch (gameResult)
            {
                case GameResultType.CrossWin:
                    return 0;
                case GameResultType.CircleWin:
                    return 1;
                default:
                    return -1;
            }
        }
    }
}