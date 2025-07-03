using Cysharp.Threading.Tasks;
using Game.Models;
using Game.Views;

namespace Game.PlayerLogics
{
    public abstract class PlayerLogic
    {
        protected GameView view;
        protected GameModel model;

        protected PlayerLogic(GameView view, GameModel model)
        {
            this.view = view;
            this.model = model;
        }

        public abstract UniTask SelectCell();
    }
}