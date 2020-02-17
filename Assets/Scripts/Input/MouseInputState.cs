using UnityEngine;

namespace com.halbach.imageselection.input {
    public class MouseInputState : IMouseInputState
    {
        protected float triggerDistance;
        protected RectTransform transformTarget;
        protected Texture2D defaultCursorTexture;
        protected Texture2D scaleUpperLeftCornerCurserTexture;
        protected Texture2D scaleUpperRightCornerCurserTexture;
        protected Texture2D scaleLowerRightCornerCurserTexture;
        protected Texture2D scaleLowerLeftCornerCurserTexture;

        protected Texture2D moveCursorTexture;

        protected Texture2D[] mouseCursorTextures;
        protected Vector2 currentMousePosition;

        private Texture2D mouseCursorTexture;
        private TargetMousePosition mouseCorner;

        public MouseInputState(TargetMousePosition mouseCorner, Texture2D mouseCursorTexture) {
            this.mouseCorner = mouseCorner;
            this.mouseCursorTexture = mouseCursorTexture;

            InitializeState();
        }

        public MouseInputState(float triggerDistance,
                                RectTransform transformTarget,
                                Texture2D defaultCursorTexture, 
                                Texture2D scaleUpperLeftCornerCurserTexture,
                                Texture2D scaleUpperRightCornerCurserTexture,
                                Texture2D scaleLowerRightCornerCurserTexture,
                                Texture2D scaleLowerLeftCornerCurserTexture,
                                Texture2D moveCursorTexture)
        {
            this.triggerDistance = triggerDistance;
            this.transformTarget = transformTarget;
            this.defaultCursorTexture = defaultCursorTexture;
            this.scaleUpperLeftCornerCurserTexture = scaleUpperLeftCornerCurserTexture;
            this.scaleUpperRightCornerCurserTexture = scaleUpperRightCornerCurserTexture;
            this.scaleLowerLeftCornerCurserTexture = scaleLowerLeftCornerCurserTexture;
            this.scaleLowerRightCornerCurserTexture = scaleLowerRightCornerCurserTexture;
            this.moveCursorTexture = moveCursorTexture;

            currentMousePosition = Vector2.zero;

            FillMouseCursorTextureArray();

            InitializeState();
        }

        private void FillMouseCursorTextureArray()
        {
            mouseCursorTextures = new Texture2D[6];
            mouseCursorTextures[(int)TargetMousePosition.UPPER_LEFT] = scaleUpperLeftCornerCurserTexture;
            mouseCursorTextures[(int)TargetMousePosition.UPPER_RIGHT] = scaleUpperRightCornerCurserTexture;
            mouseCursorTextures[(int)TargetMousePosition.LOWER_LEFT] = scaleLowerLeftCornerCurserTexture;
            mouseCursorTextures[(int)TargetMousePosition.LOWER_RIGHT] = scaleLowerRightCornerCurserTexture;
            mouseCursorTextures[(int)TargetMousePosition.MOUSE_OVER] = moveCursorTexture;
            mouseCursorTextures[(int) TargetMousePosition.NO_CORNER] = defaultCursorTexture;
        }

        protected void InitializeState()
        {
            mouseCorner = TargetMousePosition.NO_CORNER;
            ChangeCursorTexture();
            PrintTriggerMessage(mouseCorner.ToString());
        }

        private void PrintTriggerMessage(string trigger) {
            Debug.Log("Triggered: " + trigger);
        }

        private void ChangeCursorTexture()
        {
            ChangeCursor(mouseCursorTexture);
        }

        private void ChangeCursor(Texture2D cursorTexture) {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        public IMouseInputState UpdateMousePostion(Vector2 mousePosition)
        {   
            IMouseInputState newState;

            currentMousePosition = mousePosition;

            newState = GetMouseOverState(mousePosition);

            return newState;
        }

        private IMouseInputState GetMouseOverState(Vector2 mousePosition)
        {
            IMouseInputState mouseOverState = HandleNotTriggerd();

            Vector3[] transformTargetCorners = GetRectCorners();
            
            if(TriggersUpperLeftCorner(transformTargetCorners))
            {
                mouseOverState = HandleUpperLeftTriggered();
            } 
            else if (TriggersUpperRightCorner(transformTargetCorners))
            {
                mouseOverState = HandleUpperRightTriggered();
            }
            else if(TriggersLowerRightCorner(transformTargetCorners))
            {
                mouseOverState =  HandleLowerRightTriggered();
            }
            else if(TriggersLowerLeftCorner(transformTargetCorners))
            {
                mouseOverState = HandleLowerLeftTriggered();
            }
            else if(MouseIsOverTransformTarget())
            {
                mouseOverState = HandleMouseOver();
            }

            return mouseOverState;
        }

        public IMouseInputState MouseDown(Vector2 mousePosition)
        {
            return this;
        }

        public IMouseInputState MouseUp(Vector2 mousePosition)
        {
            return this;
        }

        private Vector3[] GetRectCorners()
        {
            Vector3[] localSelectionCorners = new Vector3[4];

            transformTarget.GetWorldCorners(localSelectionCorners);

            return localSelectionCorners;
        }

        private bool TriggersUpperLeftCorner(Vector3[] transformTargetCorners){
            return TriggersCorner((int)TargetMousePosition.UPPER_LEFT, transformTargetCorners);
        }

        private bool TriggersUpperRightCorner(Vector3[] transformTargetCorners){
            return TriggersCorner((int)TargetMousePosition.UPPER_RIGHT, transformTargetCorners);
        }

        private bool TriggersLowerRightCorner(Vector3[] transformTargetCorners){
            return TriggersCorner((int)TargetMousePosition.LOWER_RIGHT, transformTargetCorners);
        }

        private bool TriggersLowerLeftCorner(Vector3[] transformTargetCorners){
            return TriggersCorner((int)TargetMousePosition.LOWER_LEFT, transformTargetCorners);
        }

        private bool MouseIsOverTransformTarget() {
            return false;
        }

        private IMouseInputState HandleUpperLeftTriggered() {
            TargetMousePosition corner = TargetMousePosition.UPPER_LEFT;
            
            return HandleCornerTriggered(corner);
        }

        private IMouseInputState HandleUpperRightTriggered() {
            TargetMousePosition corner = TargetMousePosition.UPPER_RIGHT;
            
            return HandleCornerTriggered(corner);
        }

        private IMouseInputState HandleLowerRightTriggered() {
            TargetMousePosition corner = TargetMousePosition.LOWER_RIGHT;

            return HandleCornerTriggered(corner);
        }

        private IMouseInputState HandleLowerLeftTriggered() {
            TargetMousePosition corner = TargetMousePosition.LOWER_LEFT;
            
            return HandleCornerTriggered(corner);
        }

        private IMouseInputState HandleCornerTriggered(TargetMousePosition corner)
        {
            IMouseInputState state = this;
            if(DoesStateChange(corner)) {
                Texture2D cursorTexture = GetCursorTexture(corner);
                state = new MouseOverCornerState(corner, cursorTexture);
            }
            return state;
        }

        private bool DoesStateChange(TargetMousePosition corner)
        {
            return corner != mouseCorner;
        }

        private IMouseInputState HandleNotTriggerd() {
            return new MouseInputState(TargetMousePosition.NO_CORNER, defaultCursorTexture);
        }

        private IMouseInputState HandleMouseOver() {
            return new MouseInputState(TargetMousePosition.MOUSE_OVER, moveCursorTexture);
        }
        
        private bool TriggersCorner(int cornerIndex, Vector3[] transformTargetCorners)
        {
            Vector3 cornerInWorldCoordinates = transformTargetCorners[cornerIndex];
            Vector3 cornerInScreenCoordinates = Camera.main.WorldToScreenPoint(cornerInWorldCoordinates);
            cornerInScreenCoordinates.z = 0;

            return isInTriggerDistance(cornerInScreenCoordinates, currentMousePosition);
        }

        private bool isInTriggerDistance(Vector3 point1, Vector3 point2)
        {
            Vector3 vector = point1 - point2;
            float distance = Mathf.Abs(vector.magnitude);

            bool isInTriggerDistance = distance <= triggerDistance;

            return isInTriggerDistance;
        }

        private Texture2D GetCursorTexture(TargetMousePosition corner) {
            return mouseCursorTextures[(int)corner];
        }
    }

    public enum TargetMousePosition {
        LOWER_LEFT,
        UPPER_LEFT,
        UPPER_RIGHT,
        LOWER_RIGHT,
        MOUSE_OVER,
        NO_CORNER
    }
}