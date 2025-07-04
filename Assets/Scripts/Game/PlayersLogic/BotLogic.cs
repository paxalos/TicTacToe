using System;
using Cysharp.Threading.Tasks;
using Game.Enums;
using Game.Models;
using Game.Presenters;
using Game.Views;

namespace Game.PlayerLogics
{
    public abstract class BotLogic : PlayerLogic
    {
        private GameWinPresenter gameWinPresenter;
        private int playerIndex;

        protected abstract int MaxDepth { get; }

        public BotLogic(GameWindowView gameWindow,
                        GameModel model,
                        GameWinPresenter gameWinPresenter,
                        int playerIndex) : base(gameWindow, model)
        {
            this.gameWinPresenter = gameWinPresenter;
            this.playerIndex = playerIndex;
        }

        private int Minimax(PlaySymbolType[] cellPlaySymbols,
                            int depth,
                            bool isMaximizing)
        {
            var gameResult = gameWinPresenter.CalculateGameResult(cellPlaySymbols, out _);

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

            var element = model.GetPlaySymbolByPlayerIndex(playerIndex);
            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < GameModel.CELLS_COUNT; i++)
                {
                    if (cellPlaySymbols[i] == PlaySymbolType.Empty)
                    {
                        cellPlaySymbols[i] = element;
                        int score = Minimax(cellPlaySymbols, depth + 1, false);
                        cellPlaySymbols[i] = PlaySymbolType.Empty;
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
                    if (cellPlaySymbols[i] == PlaySymbolType.Empty)
                    {
                        cellPlaySymbols[i] = model.GetEnemyPlaySymbol(element);
                        int score = Minimax(cellPlaySymbols, depth + 1, true);
                        cellPlaySymbols[i] = PlaySymbolType.Empty;
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

                var cellPlaySymbols = model.CellPlaySymbols;
                var element = model.GetPlaySymbolByPlayerIndex(playerIndex);

                for (int i = 0; i < GameModel.CELLS_COUNT; i++)
                {
                    if (cellPlaySymbols[i] == PlaySymbolType.Empty)
                    {
                        cellPlaySymbols[i] = element;
                        int score = Minimax(cellPlaySymbols, 0, false);
                        cellPlaySymbols[i] = PlaySymbolType.Empty;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            selectedIndex = i;
                        }
                    }
                }

                await UniTask.SwitchToMainThread();

                gameWindow.SelectCell(selectedIndex);
            });
        }
    }
}