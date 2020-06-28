using UnityEngine;
using System.Collections.Generic;

namespace com.halbach.imageselection.input {

    public class MobileSelectionRectScaleState : MobileInputState
    {
        private Vector2 touchPointConnection;

        InputPropertyContainer propertyContainer;

        int verticalMultiplicator = 1;

        int horizontalMultiplicator = 1;

        bool isInitialized = false;

        public MobileSelectionRectScaleState(RectTransform transformTarget, InputPropertyContainer propertyContainer) : base(transformTarget) {
            touchPointConnection = Vector2.zero;
            this.propertyContainer = propertyContainer;
        }

        protected override void UpdateInternal(List<Vector2> touchPositions, Vector2 newTouchPointConnection)
        {
            if(touchPointConnection != Vector2.zero) {

                Initialize(touchPositions);

                Vector3 delta = touchPointConnection - newTouchPointConnection;

                ScaleSelectionRect(delta);
            }
            touchPointConnection = newTouchPointConnection;
        }

        protected override Vector2 CalculateDelta(List<Vector2> touchPoints) {
            Vector2 touchPointConnection = Vector2.zero;
            if(touchPoints.Count > 1) {
                Vector2 firstTouchPoint = touchPoints[0];
                Vector2 secondTouchPoint = touchPoints[1];

                touchPointConnection = firstTouchPoint - secondTouchPoint;
            }

            return touchPointConnection;
        }

        private void ScaleSelectionRect(Vector3 delta) {
            float horizontalSize = propertyContainer.CalculateUpdatedHorizontalSize(delta, transformTarget.rect, horizontalMultiplicator);
            float verticalSize = propertyContainer.CalculateUpdatedVerticalSize(delta, transformTarget.rect, verticalMultiplicator);
            transformTarget.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalSize);
            transformTarget.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, verticalSize);
        }

        private void Initialize(List<Vector2> touchPositions) {
            if(!isInitialized) {
                isInitialized = true;
                if(touchPositions.Count > 1) {
                    horizontalMultiplicator = CalculateHorizontalMultiplicator(touchPositions);
                    verticalMultiplicator = CalculateVeritcalMultiplicator(touchPositions);
                }
            }
        }

        private int CalculateHorizontalMultiplicator(List<Vector2> touchPositions) {
            return CalculateMultiplicator(touchPositions[0].x, touchPositions[1].x);
        }

        private int CalculateVeritcalMultiplicator(List<Vector2> touchPositions) {
            return -1 * CalculateMultiplicator(touchPositions[0].y, touchPositions[1].y);
        }

        private int CalculateMultiplicator(float value1, float value2) {
            return value1 >= value2 ? 1 : -1;
        }
    }
}