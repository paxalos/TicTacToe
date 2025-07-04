using Game.Models;
using Game.Presenters;
using Game.Views;

namespace Game.PlayerLogics
{
    public class EasyBotLogic : BotLogic
    {
        public EasyBotLogic(GameWindowView gameWindow,
                            GameModel model,
                            GameWinPresenter gameWinPresenter,
                            int playerIndex) : base(gameWindow,
                                                    model,
                                                    gameWinPresenter,
                                                    playerIndex)
        {
        }

        protected override int MaxDepth => 1;
    }
}