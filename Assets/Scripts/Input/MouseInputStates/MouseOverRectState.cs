using UnityEngine;

namespace com.halbach.imageselection.input {
    
    public class MouseOverRectState : MouseDragState
    {
        private Texture2D mouseOverRectTexture;

        public MouseOverRectState(TargetMousePosition mouseCorner, MousePropertyContainer mousePropertyContainer, RectTransform transformTarget) 
                                        : base(mouseCorner, mousePropertyContainer, transformTarget)
        {
        }

        protected override void UpdateSelection(Vector3 delta)
        {
            Vector3 oldRectPosition = transformTarget.position;
            oldRectPosition.x -= delta.x;
            oldRectPosition.y -= delta.y;
            transformTarget.position = oldRectPosition;

            UpdateSelection();
        }
    }
}