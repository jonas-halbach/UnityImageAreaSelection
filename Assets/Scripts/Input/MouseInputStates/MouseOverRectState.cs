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
            currentMousePosition = mousePosition;

            return OnMouseMove(mousePosition);
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

        private IMouseInputState OnMouseMove(Vector2 mousePosition)
        {
            IMouseInputState newState = this;

            if(mouseDown)
            {
                Vector3 delta = CalculateChangeVector(mousePosition);

                UpdateSelectionIndicatorPosition(delta);
            }
            else
            {
                newState = GetMouseOverState(mousePosition);
            }
            lastMousePosition = currentMousePosition;

            return newState;
        }

        private Vector3 CalculateChangeVector(Vector3 mousePosition)
        {
            Vector3 delta = CalculateMouseMovementDelta(mousePosition);
            delta *= movingSpeed;
            return delta;
        }

        private Vector3 CalculateMouseMovementDelta(Vector3 newMoousePostion)
        {
            Vector3 currentMousePosition = newMoousePostion;
            Vector3 delta = lastMousePosition - currentMousePosition;

            return delta;
        }

        private void UpdateSelectionIndicatorPosition(Vector3 delta)
        {
            Vector3 oldRectPosition = transformTarget.position;
            oldRectPosition.x -= delta.x;
            oldRectPosition.y -= delta.y;
            transformTarget.position = oldRectPosition;

            UpdateSelection();
        }
    }
}