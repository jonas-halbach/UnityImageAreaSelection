using UnityEngine;

namespace com.halbach.imageselection.input {
    public class MouseInputState : IMouseInputState
    {
        private float triggerDistance;
        private RectTransform transformTarget;
        private Texture2D defaultCursorTexture;
        private Texture2D scaleUpperLeftCornerCurserTexture;
        private Texture2D scaleUpperRightCornerCurserTexture;
        private Texture2D scaleLowerRightCornerCurserTexture;
        private Texture2D scaleLowerLeftCornerCurserTexture;
        private Vector2 currentMousePosition;

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
            this.scaleLowerLeftCornerCurserTexture = scaleUpperLeftCornerCurserTexture;
            this.scaleUpperRightCornerCurserTexture = scaleUpperRightCornerCurserTexture;
            this.scaleLowerLeftCornerCurserTexture = scaleLowerLeftCornerCurserTexture;
            this.scaleLowerRightCornerCurserTexture = scaleLowerRightCornerCurserTexture;

            currentMousePosition = Vector2.zero;
        }

        public void UpdateMousePostion(Vector2 mousePosition)
        {            
            currentMousePosition = mousePosition;

            Vector3[] transformTargetCorners = GetRectCorners();
            
            if(triggersUpperLeftCorner(transformTargetCorners))
            {
                HandleUpperLeftTriggered();
            } 
            else if (triggersUpperRightCorner(transformTargetCorners))
            {
                HandleUpperRightTriggered();
            }
            else if(triggersLowerRightCorner(transformTargetCorners))
            {
                HandleLowerRightTriggered();
            }
            else if(triggersLowerLeftCorner(transformTargetCorners))
            {
                HandleLowerLeftTriggered();
            }
            else
            {
                HandleNotTriggerd();
            }
        }

        public void MouseDown(Vector2 mousePosition)
        {
            
        }

        public void MouseUp(Vector2 mousePosition)
        {
            
        }

        private Vector3[] GetRectCorners()
        {
            Vector3[] localSelectionCorners = new Vector3[4];

            transformTarget.GetWorldCorners(localSelectionCorners);

            return localSelectionCorners;
        }

        private bool triggersUpperLeftCorner(Vector3[] transformTargetCorners){
            return triggersCorner((int)RectCorners.UPPER_LEFT, transformTargetCorners);
        }

        private bool triggersUpperRightCorner(Vector3[] transformTargetCorners){
            return triggersCorner((int)RectCorners.UPPER_RIGHT, transformTargetCorners);
        }

        private bool triggersLowerRightCorner(Vector3[] transformTargetCorners){
            return triggersCorner((int)RectCorners.LOWER_RIGHT, transformTargetCorners);
        }

        private bool triggersLowerLeftCorner(Vector3[] transformTargetCorners){
            return triggersCorner((int)RectCorners.LOWER_LEFT, transformTargetCorners);
        }

        private void HandleUpperLeftTriggered() {
            ChangeToUpperLeftCursorTexture();
            PrintTriggerMessage(RectCorners.UPPER_LEFT.ToString());
        }

        private void HandleUpperRightTriggered() {
            ChangeToUpperRightCursorTexture();
            PrintTriggerMessage(RectCorners.UPPER_RIGHT.ToString());
        }

        private void HandleLowerRightTriggered() {
            ChangeToLowerLeftCursorTexture();
            PrintTriggerMessage(RectCorners.LOWER_RIGHT.ToString());
        }

        private void HandleLowerLeftTriggered() {
            ChangeToLowerLeftCursorTexture();
            PrintTriggerMessage(RectCorners.LOWER_LEFT.ToString());
        }

        private void HandleNotTriggerd() {
            ChangeToDefaultCursor();
            PrintTriggerMessage(RectCorners.NOTHING.ToString());
        }

        private void PrintTriggerMessage(string trigger) {
            Debug.Log("Triggered: " + trigger);
        }
        private bool triggersCorner(int cornerIndex, Vector3[] transformTargetCorners)
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

        private void ChangeToUpperLeftCursorTexture()
        {
            ChangeCursor(scaleUpperLeftCornerCurserTexture);
        }

        private void ChangeToUpperRightCursorTexture()
        {
            ChangeCursor(scaleUpperRightCornerCurserTexture);
        }

        private void ChangeToLowerRightCursorTexture()
        {
            ChangeCursor(scaleLowerRightCornerCurserTexture);
        }

        private void ChangeToLowerLeftCursorTexture()
        {
            ChangeCursor(scaleLowerLeftCornerCurserTexture);
        }

        private void ChangeToDefaultCursor()
        {
            ChangeCursor(defaultCursorTexture);
        }

        private void ChangeCursor(Texture2D cursorTexture) {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    enum RectCorners {
        LOWER_LEFT,
        UPPER_LEFT,
        UPPER_RIGHT,
        LOWER_RIGHT,
        
        NOTHING
    }
}