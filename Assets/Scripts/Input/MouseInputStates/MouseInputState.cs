using UnityEngine;

namespace com.halbach.imageselection.input {
    public class MouseInputState : IMouseInputState
    {
        public event SelectionChanged OnSelectionChanged;
        protected RectTransform transformTarget;
        protected MousePropertyContainer mousePropertyContainer;
        protected Vector2 currentMousePosition;

        private Texture2D mouseCursorTexture;
        protected TargetMousePosition mouseCorner;

        public MouseInputState(TargetMousePosition mouseCorner, MousePropertyContainer mousePropertyContainer, RectTransform transformTarget) {
            this.mouseCorner = mouseCorner;
            this.transformTarget = transformTarget;

            this.mousePropertyContainer = mousePropertyContainer;

            InitializeState();
        }

        public MouseInputState( MousePropertyContainer mousePropertyContainer,
                                RectTransform transformTarget)
        {
            this.mouseCorner = TargetMousePosition.NO_CORNER;
            this.transformTarget = transformTarget;

            this.mousePropertyContainer = mousePropertyContainer;

            currentMousePosition = Vector2.zero;

            InitializeState();
        }

        protected virtual void InitializeState()
        {
            mouseCursorTexture = mousePropertyContainer.GetTexture(mouseCorner);
            ChangeCursorTexture();
            PrintTriggerMessage(mouseCorner.ToString());
        }

        protected void PrintTriggerMessage(string trigger) {
            Debug.Log("Triggered: " + trigger);
        }

        protected void ChangeCursorTexture()
        {
            ChangeCursor(mouseCursorTexture);
        }

        private void ChangeCursor(Texture2D cursorTexture) {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        public virtual IMouseInputState UpdateMousePostion(Vector2 mousePosition)
        {   
            IMouseInputState newState;

            currentMousePosition = mousePosition;

            newState = GetMouseOverState(mousePosition);

            return newState;
        }

        public IMouseInputState GetMouseOverState(Vector2 mousePosition)
        {
            IMouseInputState mouseOverState;

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
            else if(MouseIsOverTransformTarget(transformTargetCorners))
            {
                mouseOverState = HandleMouseOver();
            }
            else 
            {
                mouseOverState = HandleNotTriggerd();
            }

            return mouseOverState;
        }

        public virtual IMouseInputState MouseDown(Vector2 mousePosition)
        {
            return this;
        }

        public virtual IMouseInputState MouseUp(Vector2 mousePosition)
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

        private bool MouseIsOverTransformTarget(Vector3[] transformTargetCorners) {
            
            Vector3 upperLeftCornerInScreenSpace = GetRectCornerInWorldCoordinates((int)TargetMousePosition.UPPER_LEFT, transformTargetCorners);
            Vector3 lowerRighttCornerInScreenSpace = GetRectCornerInWorldCoordinates((int)TargetMousePosition.LOWER_RIGHT, transformTargetCorners);
            
            bool isInsideRectWidth = currentMousePosition.x > upperLeftCornerInScreenSpace.x && currentMousePosition.x < lowerRighttCornerInScreenSpace.x;
            bool isInsideRectHeight= currentMousePosition.y < upperLeftCornerInScreenSpace.y && currentMousePosition.y > lowerRighttCornerInScreenSpace.y;

            return isInsideRectWidth && isInsideRectHeight;
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
                state = new MouseOverCornerState(corner, mousePropertyContainer, transformTarget);
                state.OnSelectionChanged += OnSelectionChanged;
            }
            return state;
        }

        private bool DoesStateChange(TargetMousePosition corner)
        {
            return corner != mouseCorner;
        }

        private IMouseInputState HandleNotTriggerd() {
            IMouseInputState state = this;
            if(DoesStateChange(TargetMousePosition.NO_CORNER)) {
                state = new MouseInputState(TargetMousePosition.NO_CORNER, mousePropertyContainer, transformTarget);
                state.OnSelectionChanged += OnSelectionChanged;
            }

            return state;
        }

        private IMouseInputState HandleMouseOver() {
            IMouseInputState state = this;
            if(DoesStateChange(TargetMousePosition.MOUSE_OVER)) {
                state = new MouseOverRectState(TargetMousePosition.MOUSE_OVER, mousePropertyContainer, transformTarget);
                state.OnSelectionChanged += OnSelectionChanged;
            }
            return state;
        }
        
        private bool TriggersCorner(int cornerIndex, Vector3[] transformTargetCorners)
        {
            Vector3 cornerInScreenCoordinates = GetRectCornerInWorldCoordinates(cornerIndex, transformTargetCorners);

            return isInTriggerDistance(cornerInScreenCoordinates, currentMousePosition);
        }

        private Vector3 GetRectCornerInWorldCoordinates(int cornerIndex, Vector3[] transformTargetCorners) {
            Vector3 cornerInWorldCoordinates = transformTargetCorners[cornerIndex];
            cornerInWorldCoordinates.z = 0;

            return cornerInWorldCoordinates;
        }

        private bool isInTriggerDistance(Vector3 point1, Vector3 point2)
        {
            Vector3 vector = point1 - point2;
            float distance = Mathf.Abs(vector.magnitude);

            bool isInTriggerDistance = distance <= mousePropertyContainer.TriggerDistance;

            return isInTriggerDistance;
        }

        private Texture2D GetCursorTexture(TargetMousePosition corner) {
            return mousePropertyContainer.GetTexture(corner);
        }

        protected void UpdateSelection() {
            if(OnSelectionChanged != null)
            {
                OnSelectionChanged(this);
            }
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