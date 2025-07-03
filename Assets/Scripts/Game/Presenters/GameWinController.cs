using Game.Enums;
using System.Linq;

namespace Game.Presenters
{
    public class GameWinController
    {
        public GameResultType CalculateGameResult(ElementType[] cellElements, out int winLineIndex)
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