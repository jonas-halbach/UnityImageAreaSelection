using UnityEngine;

namespace com.halbach.imageselection.input {
    public class MouseOverRectState : MouseInputState
    {
        private Texture2D mouseOverRectTexture;

        private Vector3 lastMousePosition;

        private bool mouseDown;

        private float movingSpeed = 0.02f;

        public MouseOverRectState(TargetMousePosition mouseCorner, MousePropertyContainer mousePropertyContainer, RectTransform transformTarget) 
                                        : base(mouseCorner, mousePropertyContainer, transformTarget)
        {
            mouseDown = false;
        }

        public override IMouseInputState UpdateMousePostion(Vector2 mousePosition)
        {   
            IMouseInputState newState;

            currentMousePosition = mousePosition;

            newState = GetMouseOverState(mousePosition);

            OnMouseMove();

            return newState;
        }

        public override IMouseInputState MouseDown(Vector2 mousePosition)
        {
            lastMousePosition = mousePosition;
            mouseDown = true;
            return this;
        }

        public override IMouseInputState MouseUp(Vector2 mousePosition)
        {
            lastMousePosition = mousePosition;
            mouseDown = false;
            return this;
        }

        private void OnMouseMove()
        {
            if(mouseDown)
            {
                Vector3 currentMousePosition = Input.mousePosition;
                Vector3 delta = lastMousePosition - currentMousePosition;
                lastMousePosition = currentMousePosition;

                delta *= movingSpeed;

                UpdateSelectionPosition(delta);
            }
        }
    }
}