using UnityEngine;

namespace Game.Views
{
    public class FieldView : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public readonly float DrawFieldAnimationDuration = 63f / 60f;

        private const string DRAW_FIELD_LINES_TRIGGER = "DrawFieldLines";
        private const string DRAW_WIN_LINE_TRIGGER = "DrawWinLine";
        private const string DRAW_WIN_LINE_INDEX = "DrawWinLineIndex";

        public void DrawFieldLines()
        {
            animator.SetTrigger(DRAW_FIELD_LINES_TRIGGER);
        }

        public void DrawWinLine(int drawWinLineIndex)
        {
            animator.SetInteger(DRAW_WIN_LINE_INDEX, drawWinLineIndex);
            animator.SetTrigger(DRAW_WIN_LINE_TRIGGER);
        }
    }
}
