using Cysharp.Threading.Tasks;
using Game.Models;
using Game.Views;

namespace Game.PlayerLogics
{
    public class RealPlayerLogic : PlayerLogic
    {
        public RealPlayerLogic(GameWindowView gameWindow, GameModel model) : base(gameWindow, model)
        {
        }

        public override UniTask SelectCell()
        {
            int[] indexesForChoise = model.GetCellIndexesForChoise();
            gameWindow.ActivateCells(indexesForChoise);
            return gameWindow.GetCellClickWait(indexesForChoise);
        }
    }
}