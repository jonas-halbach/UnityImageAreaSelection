using UnityEngine;

namespace com.halbach.imageselection.input {
    public class MouseOverCornerState : MouseDragState
    {
        Vector3 oldRectPosition;

        Vector3 oldSizeDelta;

        public MouseOverCornerState(TargetMousePosition mouseCorner, InputPropertyContainer mousePropertyContainer, RectTransform transformTarget) 
                                        : base(mouseCorner, mousePropertyContainer, transformTarget)
        {
            oldRectPosition = transformTarget.position;
            oldSizeDelta = transformTarget.sizeDelta;
        }

        protected override void UpdateSelection(Vector3 delta)
        {
            Vector3[] rectCornersBeforeResizing = new Vector3[4];
            Vector3[] rectCornersAfterResizing = new Vector3[4];

            transformTarget.GetWorldCorners(rectCornersBeforeResizing);

            float newHorizontalSize = CalculateUpdatedHorizontalSize(delta);
            float newVerticalSize = CalculateUpdatedVerticalSize(delta);

            transformTarget.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newHorizontalSize);
            transformTarget.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newVerticalSize);

            transformTarget.GetWorldCorners(rectCornersAfterResizing);

            Vector3 maxVectorDistanceWorldSpace = MaxMovementDelta(rectCornersBeforeResizing, rectCornersAfterResizing, GetConvertedTargetMousePosition());

            transformTarget.position = transformTarget.position + maxVectorDistanceWorldSpace;

            UpdateSelection();
        }

        private float CalculateUpdatedVerticalSize(Vector3 delta) {
            int verticalMultiplicator = GetVerticalMultiplicator();
            float newVerticalSize = transformTarget.rect.size.y + (verticalMultiplicator * delta.y);

            newVerticalSize = Mathf.Max(newVerticalSize,mousePropertyContainer.MinimumHeight);
            newVerticalSize = Mathf.Min(newVerticalSize,mousePropertyContainer.MaximumHeight);

            return newVerticalSize;
        }

        private float CalculateUpdatedHorizontalSize(Vector3 delta) {
            int horizontalMultiplicator = GetHorizontalMultiplicator();
            float newHorizontalSize = transformTarget.rect.size.x - (horizontalMultiplicator * delta.x);

            newHorizontalSize = Mathf.Max(newHorizontalSize, mousePropertyContainer.MinimumWidth);
            newHorizontalSize = Mathf.Min(newHorizontalSize, mousePropertyContainer.MaximumWidth);

            return newHorizontalSize;
        }
        private Vector3 MaxMovementDelta(Vector3[] rectCornersBeforeResizing, Vector3[] rectCornersAfterResizing, int index) {
            
            Vector3 delta = rectCornersBeforeResizing[index] - rectCornersAfterResizing[index];

            return delta;
        }

        private int GetConvertedTargetMousePosition() {
            int convertedMousePosition = 0;
            if(mouseCorner == TargetMousePosition.UPPER_LEFT) {
                convertedMousePosition = 3;
            }else if (mouseCorner == TargetMousePosition.UPPER_RIGHT) {
                convertedMousePosition = 0;
            } else if(mouseCorner == TargetMousePosition.LOWER_RIGHT) {
                convertedMousePosition = 1;
            } else {
                convertedMousePosition = 2;
            }

            return convertedMousePosition;
        }

        private int GetVerticalMultiplicator() {
            int verticalMultiplicator = 1;
            
            if(mouseCorner == TargetMousePosition.UPPER_LEFT || mouseCorner == TargetMousePosition.UPPER_RIGHT)
            {
                verticalMultiplicator = -1;
            }
            
            return verticalMultiplicator;
        }

        private int GetHorizontalMultiplicator() {
            int horizontalMultiplicator = 1;

            if(mouseCorner == TargetMousePosition.UPPER_LEFT || mouseCorner == TargetMousePosition.LOWER_LEFT)
            {
                horizontalMultiplicator = -1;
            }
            return horizontalMultiplicator;
        }
    }
}
