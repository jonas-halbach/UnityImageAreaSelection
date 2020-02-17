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

        protected Texture2D[] mouseCursorTextures;
        protected Vector2 currentMousePosition;

        private Texture2D mouseCursorTexture;
        private RectCorner mouseCorner;

        public MouseInputState(RectCorner mouseCorner, Texture2D mouseCursorTexture) {
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
                                Texture2D scaleLowerLeftCornerCurserTexture)
        {
            this.triggerDistance = triggerDistance;
            this.transformTarget = transformTarget;
            this.defaultCursorTexture = defaultCursorTexture;
            this.scaleUpperLeftCornerCurserTexture = scaleUpperLeftCornerCurserTexture;
            this.scaleUpperRightCornerCurserTexture = scaleUpperRightCornerCurserTexture;
            this.scaleLowerLeftCornerCurserTexture = scaleLowerLeftCornerCurserTexture;
            this.scaleLowerRightCornerCurserTexture = scaleLowerRightCornerCurserTexture;

            currentMousePosition = Vector2.zero;

            FillMouseCursorTextureArray();

            InitializeState();
        }

        private void FillMouseCursorTextureArray()
        {
            mouseCursorTextures = new Texture2D[5];
            mouseCursorTextures[(int)RectCorner.UPPER_LEFT] = scaleUpperLeftCornerCurserTexture;
            mouseCursorTextures[(int)RectCorner.UPPER_RIGHT] = scaleUpperRightCornerCurserTexture;
            mouseCursorTextures[(int)RectCorner.LOWER_LEFT] = scaleLowerLeftCornerCurserTexture;
            mouseCursorTextures[(int)RectCorner.LOWER_RIGHT] = scaleLowerRightCornerCurserTexture;
            mouseCursorTextures[(int) RectCorner.NO_CORNER] = defaultCursorTexture;
        }

        protected void InitializeState()
        {
            mouseCorner = RectCorner.NO_CORNER;
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
                mouseOverState = HandleNotTriggerd();
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
            return TriggersCorner((int)RectCorner.UPPER_LEFT, transformTargetCorners);
        }

        private bool TriggersUpperRightCorner(Vector3[] transformTargetCorners){
            return TriggersCorner((int)RectCorner.UPPER_RIGHT, transformTargetCorners);
        }

        private bool TriggersLowerRightCorner(Vector3[] transformTargetCorners){
            return TriggersCorner((int)RectCorner.LOWER_RIGHT, transformTargetCorners);
        }

        private bool TriggersLowerLeftCorner(Vector3[] transformTargetCorners){
            return TriggersCorner((int)RectCorner.LOWER_LEFT, transformTargetCorners);
        }

        private bool MouseIsOverTransformTarget() {
            return false;
        }

        private IMouseInputState HandleUpperLeftTriggered() {
            RectCorner corner = RectCorner.UPPER_LEFT;
            
            return HandleCornerTriggered(corner);
        }

        private IMouseInputState HandleUpperRightTriggered() {
            RectCorner corner = RectCorner.UPPER_RIGHT;
            
            return HandleCornerTriggered(corner);
        }

        private IMouseInputState HandleLowerRightTriggered() {
            RectCorner corner = RectCorner.LOWER_RIGHT;

            return HandleCornerTriggered(corner);
        }

        private IMouseInputState HandleLowerLeftTriggered() {
            RectCorner corner = RectCorner.LOWER_LEFT;
            
            return HandleCornerTriggered(corner);
        }

        private IMouseInputState HandleCornerTriggered(RectCorner corner)
        {
            IMouseInputState state = this;
            if(DoesStateChange(corner)) {
                Texture2D cursorTexture = GetCursorTexture(corner);
                state = new MouseOverCornerState(corner, cursorTexture);
            }
            return state;
        }

        private bool DoesStateChange(RectCorner corner)
        {
            return corner != mouseCorner;
        }

        private IMouseInputState HandleNotTriggerd() {
            return new MouseInputState(RectCorner.NO_CORNER, defaultCursorTexture);
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

        private Texture2D GetCursorTexture(RectCorner corner) {
            return mouseCursorTextures[(int)corner];
        }
    }

    public enum RectCorner {
        LOWER_LEFT,
        UPPER_LEFT,
        UPPER_RIGHT,
        LOWER_RIGHT,
        NO_CORNER
    }
}