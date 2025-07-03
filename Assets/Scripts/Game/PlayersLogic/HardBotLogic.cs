using Game.Models;
using Game.Presenters;
using Game.Views;

namespace Game.PlayerLogics
{
    public class HardBotLogic : BotLogic
    {
        public HardBotLogic(GameView view, 
                            GameModel model, 
                            GameWinController gameWinController, 
                            int playerIndex) : base(view, 
                                                    model, 
                                                    gameWinController, 
                                                    playerIndex)
        {
        }

        protected override int MaxDepth => int.MaxValue;
    }
}