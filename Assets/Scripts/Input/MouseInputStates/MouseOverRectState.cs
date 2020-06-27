using com.halbach.imageselection.selection.RectModifier;
using UnityEngine;

namespace com.halbach.imageselection.input {
    
    public class MouseOverRectState : MouseDragState
    {
        private Texture2D mouseOverRectTexture;

        private RectMover mover;

        public MouseOverRectState(TargetMousePosition mouseCorner, InputPropertyContainer mousePropertyContainer, RectTransform transformTarget) 
                                        : base(mouseCorner, mousePropertyContainer, transformTarget)
        {
            mover = new RectMover(transformTarget);
        }

        protected override void UpdateSelection(Vector3 delta)
        {
            MoveSelectionRect(delta);

            UpdateSelection();
        }

        public void MoveSelectionRect(Vector3 delta) {
            mover.Move(delta);
        }
    }
}