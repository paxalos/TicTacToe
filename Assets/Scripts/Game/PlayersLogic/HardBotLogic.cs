using Game.Models;
using Game.Presenters;
using Game.Views;

namespace Game.PlayerLogics
{
    public class HardBotLogic : BotLogic
    {
        public HardBotLogic(GameWindowView gameWindow, 
                            GameModel model, 
                            GameWinPresenter gameWinPresenter, 
                            int playerIndex) : base(gameWindow, 
                                                    model, 
                                                    gameWinPresenter, 
                                                    playerIndex)
        {
        }

        protected override int MaxDepth => int.MaxValue;
    }
}