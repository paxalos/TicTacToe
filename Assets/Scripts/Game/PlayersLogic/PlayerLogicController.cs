using Cysharp.Threading.Tasks;
using Game.Enums;
using Game.Models;
using Game.Presenters;
using Game.Views;
using Utils;
using Zenject;

namespace Game.PlayerLogics
{
    public class PlayerLogicController
    {
        [Inject] private GameView view;
        [Inject] private GameModel model;
        [Inject] private GameWinController gameWinController;

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
                        players[i] = new RealPlayerLogic(view, model);
                        break;
                    case PlayerType.EasyBot:
                        players[i] = new EasyBotLogic(view,
                                                      model,
                                                      gameWinController,
                                                      i);
                        break;
                    case PlayerType.NormalBot:
                        players[i] = new NormalBotLogic(view,
                                                        model,
                                                        gameWinController,
                                                        i);
                        break;
                    case PlayerType.HardBot:
                        players[i] = new HardBotLogic(view,
                                                      model,
                                                      gameWinController,
                                                      i);
                        break;
                }
            }
        }

        public UniTask PlayerTurn()
        {
            var player = players[currentPlayerIndex];
            view.SetPlayerTurnMessage(currentPlayerIndex);
            currentPlayerIndex = currentPlayerIndex.IncrementInRange(players.Length);
            return player.SelectCell();
        }

        public void ResetPlayers()
        {
            currentPlayerIndex = 0;
        }
    }
}