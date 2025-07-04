using Cysharp.Threading.Tasks;
using Game.Enums;
using Game.Models;
using Game.Presenters;
using Game.Views;
using Utils;
using Zenject;

namespace Game.PlayerLogics
{
    public class PlayerLogicPresenter
    {
        [Inject] private GameWindowView gameWindow;
        [Inject] private GameModel model;
        [Inject] private GameWinPresenter gameWinPresenter;

        private PlayerLogic[] players;
        private int currentPlayerIndex;

        public void SetPlayers(PlayerType[] playerTypes)
        {
            players = new PlayerLogic[playerTypes.Length];
            for (int i = 0; i < players.Length; i++)
            {
                switch (playerTypes[i])
                {
                    case PlayerType.RealPlayer:
                        players[i] = new RealPlayerLogic(gameWindow, model);
                        break;
                    case PlayerType.EasyBot:
                        players[i] = new EasyBotLogic(gameWindow,
                                                      model,
                                                      gameWinPresenter,
                                                      i);
                        break;
                    case PlayerType.NormalBot:
                        players[i] = new NormalBotLogic(gameWindow,
                                                        model,
                                                        gameWinPresenter,
                                                        i);
                        break;
                    case PlayerType.HardBot:
                        players[i] = new HardBotLogic(gameWindow,
                                                      model,
                                                      gameWinPresenter,
                                                      i);
                        break;
                }
            }
        }

        public UniTask PlayerTurn()
        {
            var player = players[currentPlayerIndex];
            gameWindow.SetPlayerTurnMessage(currentPlayerIndex);
            currentPlayerIndex = currentPlayerIndex.IncrementInRange(players.Length);
            return player.SelectCell();
        }

        public void ResetPlayers()
        {
            currentPlayerIndex = 0;
        }
    }
}