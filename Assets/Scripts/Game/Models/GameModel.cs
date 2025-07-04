using System;
using System.Linq;
using Game.Enums;
using Game.Views;
using Utils;
using Zenject;

namespace Game.Models
{
    public class GameModel
    {
        [Inject] private GameWindowView gameWindow;
        private readonly PlaySymbolType[] playSymbolsQueue = { PlaySymbolType.Cross, PlaySymbolType.Circle };
        private int[] playersWinCount = new int[PLAYERS_COUNT];
        private int drawCount;
        private PlaySymbolType[] cellPlaySymbols = new PlaySymbolType[CELLS_COUNT];

        public const int CELLS_COUNT = 9;
        public const int PLAYERS_COUNT = 2;

        public int CurrentPlayerIndex { get; private set; }
        public PlaySymbolType[] CellPlaySymbols
        {
            get
            {
                var result = new PlaySymbolType[CELLS_COUNT];
                Array.Copy(cellPlaySymbols, result, CELLS_COUNT);
                return result;
            }
        }

        public void SetPlayerChosenCell(int cellIndex)
        {
            var playerElement = playSymbolsQueue[CurrentPlayerIndex];
            cellPlaySymbols[cellIndex] = playerElement;
            gameWindow.SetPlaySymbolInCell(cellIndex, playerElement);
            CurrentPlayerIndex = CurrentPlayerIndex.IncrementInRange(PLAYERS_COUNT);
        }

        public void SetGameResult(GameResultType gameResult, int winningLineIndex)
        {
            if (gameResult == GameResultType.Draw)
                gameWindow.SetDrawCount(++drawCount);
            else
            {
                int playerIndex = GetPlayerIndexByGameResult(gameResult);
                gameWindow.SetPlayerWinCount(playerIndex, ++playersWinCount[playerIndex]);
            }

            gameWindow.SetGameResult(gameResult, winningLineIndex);
        }

        public int[] GetCellIndexesForChoise()
        {
            return Enumerable.Range(0, cellPlaySymbols.Length)
                             .Where(index => cellPlaySymbols[index] == PlaySymbolType.Empty)
                             .ToArray();
        }

        public void ResetCells()
        {
            for (int i = 0; i < cellPlaySymbols.Length; i++)
                cellPlaySymbols[i] = PlaySymbolType.Empty;

            CurrentPlayerIndex = 0;
        }

        public PlaySymbolType GetEnemyPlaySymbol(PlaySymbolType element)
        {
            switch (element)
            {
                case PlaySymbolType.Cross:
                    return PlaySymbolType.Circle;
                case PlaySymbolType.Circle:
                    return PlaySymbolType.Cross;
                default:
                    return PlaySymbolType.Empty;
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

        public PlaySymbolType GetPlaySymbolByPlayerIndex(int playerIndex)
        {
            switch (playerIndex)
            {
                case 0:
                    return PlaySymbolType.Cross;
                case 1:
                    return PlaySymbolType.Circle;
                default:
                    return PlaySymbolType.Empty;
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