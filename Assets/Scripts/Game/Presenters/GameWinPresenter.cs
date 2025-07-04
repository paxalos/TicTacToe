using Game.Enums;
using System.Linq;

namespace Game.Presenters
{
    public class GameWinPresenter
    {
        public GameResultType CalculateGameResult(PlaySymbolType[] cellPlaySymbols, out int winLineIndex)
        {
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
                if (IsWinningCombination(cellPlaySymbols,
                                         winningsCombinations[i],
                                         out var gameResult))
                {
                    winLineIndex = i;
                    return gameResult;
                }

            winLineIndex = -1;

            if (cellPlaySymbols.Any(cellPlaySymbol => cellPlaySymbol == PlaySymbolType.Empty))
                return GameResultType.InGame;

            return GameResultType.Draw;
        }

        private bool IsWinningCombination(PlaySymbolType[] cellPlaySymbols,
                                          int[] indexes,
                                          out GameResultType gameResultType)
        {
            gameResultType = GameResultType.InGame;
            var combination = indexes.Select(index => cellPlaySymbols[index]);

            if (combination.All(playSymbol => playSymbol != PlaySymbolType.Empty))
            {
                var playSymbolForCompare = combination.First();
                if (combination.All(element => element == playSymbolForCompare))
                {
                    switch (playSymbolForCompare)
                    {
                        case PlaySymbolType.Cross:
                            gameResultType = GameResultType.CrossWin;
                            break;
                        case PlaySymbolType.Circle:
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