using Game.Models;
using Game.Presenters;
using Game.Views;

namespace Game.PlayerLogics
{
    public class EasyBotLogic : BotLogic
    {
        public EasyBotLogic(GameView view, 
                            GameModel model, 
                            GameWinController gameWinController, 
                            int playerIndex) : base(view, 
                                                    model, 
                                                    gameWinController, 
                                                    playerIndex)
        {
        }

        protected override int MaxDepth => 1;
    }
}