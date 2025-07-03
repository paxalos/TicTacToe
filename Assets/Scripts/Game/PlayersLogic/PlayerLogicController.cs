using Game.Models;
using Game.Enums;
using Game.Views;
using Zenject;
using Utils;
using Cysharp.Threading.Tasks;

namespace Game.PlayerLogics
{
    public class PlayerLogicController
    {
        [Inject] private GameView view;
        [Inject] private GameModel model;

        private PlayerLogic[] players;
        private int currentPlayerIndex;

        public void SetPlayers(PlayerType[] playerTypes)
        {
            players = new PlayerLogic[playerTypes.Length];
            for (int i = 0; i < players.Length; i++)
            {
                switch(playerTypes[i])
                {
                    case PlayerType.RealPlayer:
                        players[i] = new RealPlayerLogic(view, model);
                        break;
                    case PlayerType.EasyBot:
                        players[i] = new EasyBotLogic(view, model);
                        break;
                    case PlayerType.HardBot:
                        players[i] = new EasyBotLogic(view, model);
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