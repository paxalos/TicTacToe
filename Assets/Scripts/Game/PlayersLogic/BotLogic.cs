using Cysharp.Threading.Tasks;
using Game.Enums;
using Game.Models;
using Game.Presenters;
using Game.Views;
using System;

namespace Game.PlayerLogics
{
    public abstract class BotLogic : PlayerLogic
    {
        private GameWinController gameWinController;
        private int playerIndex;

        protected abstract int MaxDepth { get; }

        public BotLogic(GameView view, 
                        GameModel model,
                        GameWinController gameWinController,
                        int playerIndex) : base(view, model)
        {
            this.gameWinController = gameWinController;
            this.playerIndex = playerIndex;
        }

        private int Minimax(ElementType[] cellElements,
                            int depth,
                            bool isMaximizing)
        {
            var gameResult = gameWinController.CalculateGameResult(cellElements, out _);

            var winningGameResult = model.GetWinningResultByPlayerIndex(playerIndex);
            if (gameResult == winningGameResult)
            {
                return 10 - depth;
            }
            else if (gameResult != GameResultType.InGame && gameResult != GameResultType.Draw)
            {
                return depth - 10;
            }
            else if (gameResult == GameResultType.Draw)
            {
                return 0;
            }

            if (depth >= MaxDepth)
            {
                return 0;
            }

            var element = model.GetElementByPlayerIndex(playerIndex);
            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < GameModel.CELLS_COUNT; i++)
                {
                    if (cellElements[i] == ElementType.Empty)
                    {
                        cellElements[i] = element;
                        int score = Minimax(cellElements, depth + 1, false);
                        cellElements[i] = ElementType.Empty;
                        bestScore = Math.Max(score, bestScore);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < GameModel.CELLS_COUNT; i++)
                {
                    if (cellElements[i] == ElementType.Empty)
                    {
                        cellElements[i] = model.GetEnemyElement(element);
                        int score = Minimax(cellElements, depth + 1, true);
                        cellElements[i] = ElementType.Empty;
                        bestScore = Math.Min(score, bestScore);
                    }
                }
                return bestScore;
            }
        }

        public async override UniTask SelectCell()
        {
            await UniTask.RunOnThreadPool(async () =>
            {
                int bestScore = int.MinValue;
                int selectedIndex = -1;

                var cellElements = model.CellElements;
                var element = model.GetElementByPlayerIndex(playerIndex);

                for (int i = 0; i < GameModel.CELLS_COUNT; i++)
                {
                    if (cellElements[i] == ElementType.Empty)
                    {
                        cellElements[i] = element;
                        int score = Minimax(cellElements, 0, false);
                        cellElements[i] = ElementType.Empty;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            selectedIndex = i;
                        }
                    }
                }

                await UniTask.SwitchToMainThread();

                view.SelectCell(selectedIndex);
            });
        }
    }
}