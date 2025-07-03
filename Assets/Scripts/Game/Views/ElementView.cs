using Game.Enums;
using UnityEngine;

namespace Game.Views
{
    public class ElementView : MonoBehaviour
    {
        [SerializeField] private ElementType elementType;
        [SerializeField] private Animator animator;

        public ElementType ElementType => elementType;

        private const string DRAW_TRIGGER = "Draw";

        public void StartDrawAnimation()
        {
            animator.SetTrigger(DRAW_TRIGGER);
        }
    }
}