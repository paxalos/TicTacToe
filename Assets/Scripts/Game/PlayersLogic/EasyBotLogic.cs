using Cysharp.Threading.Tasks;
using Game.Models;
using Game.Views;
using UnityEngine;

namespace Game.PlayerLogics
{
    public class EasyBotLogic : PlayerLogic
    {
        public EasyBotLogic(GameView view, GameModel model) : base(view, model)
        {
        }

        public override UniTask SelectCell()
        {
            int[] indexesForChoise = model.GetCellIndexesForChoise();
            int chosenIndex = Random.Range(0, indexesForChoise.Length);
            view.SelectCell(indexesForChoise[chosenIndex]);
            return new UniTask();
        }
    }
}