using Cysharp.Threading.Tasks;
using Game.Models;
using Game.Views;

namespace Game.PlayerLogics
{
    public class RealPlayerLogic : PlayerLogic
    {
        public RealPlayerLogic(GameView view, GameModel model) : base(view, model)
        {
        }

        public override UniTask SelectCell()
        {
            int[] indexesForChoise = model.GetCellIndexesForChoise();
            view.ActivateCells(indexesForChoise);
            return view.GetCellClickWait(indexesForChoise);
        }
    }
}