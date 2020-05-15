using UnityEngine;

namespace com.halbach.imageselection.input {
    public abstract class MouseDragState : MouseInputState 
    {
        private bool mouseDown;

        private float movingSpeed = 0.02f;

        private Vector3 lastMousePosition;

        public MouseDragState(TargetMousePosition mouseCorner, MousePropertyContainer mousePropertyContainer, RectTransform transformTarget) 
                                        : base(mouseCorner, mousePropertyContainer, transformTarget)
        {
            mouseDown = false;
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

        public override IMouseInputState UpdateMousePostion(Vector2 mousePosition)
        {   
            currentMousePosition = mousePosition;

            return OnMouseMove(mousePosition);
        }

        private IMouseInputState OnMouseMove(Vector2 mousePosition)
        {
            IMouseInputState newState = this;

            if(mouseDown)
            {
                Vector3 delta = CalculateChangeVector(mousePosition);

                UpdateSelection(delta);
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

        protected abstract void UpdateSelection(Vector3 delta);
    }
}
