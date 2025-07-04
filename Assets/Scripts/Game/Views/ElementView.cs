using Game.Enums;
using UnityEngine;

namespace Game.Views
{
    public class ElementView : MonoBehaviour
    {
        [SerializeField] private PlaySymbolType playSymbol;
        [SerializeField] private Animator animator;

        public PlaySymbolType PlaySymbol => playSymbol;

        private const string DRAW_TRIGGER = "Draw";

        public void StartDrawAnimation()
        {
            animator.SetTrigger(DRAW_TRIGGER);
        }
    }
}