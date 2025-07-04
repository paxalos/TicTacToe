using Cysharp.Threading.Tasks;
using Game.Models;
using Game.Views;

namespace Game.PlayerLogics
{
    public abstract class PlayerLogic
    {
        protected GameWindowView gameWindow;
        protected GameModel model;

        protected PlayerLogic(GameWindowView gameWindow, GameModel model)
        {
            this.gameWindow = gameWindow;
            this.model = model;
        }

        public abstract UniTask SelectCell();
    }
}