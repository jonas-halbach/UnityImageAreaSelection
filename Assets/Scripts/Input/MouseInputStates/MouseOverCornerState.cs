using UnityEngine;
using com.halbach.imageselection.helpers;

namespace com.halbach.imageselection.input {
    public class MouseOverCornerState : MouseDragState
    {
        Vector3 oldRectPosition;

        Vector3 oldSizeDelta;

        public MouseOverCornerState(TargetMousePosition mouseCorner, MousePropertyContainer mousePropertyContainer, RectTransform transformTarget) 
                                        : base(mouseCorner, mousePropertyContainer, transformTarget)
        {
            oldRectPosition = transformTarget.position;
            oldSizeDelta = transformTarget.sizeDelta;
        }

        protected override void UpdateSelection(Vector3 delta)
        {
            Vector3[] rectCornersBeforeResizing = new Vector3[4];
            Vector3[] rectCornersAfterResizing = new Vector3[4];

            RectScaler scaler = new RectScaler(transformTarget);
            

            transformTarget.GetWorldCorners(rectCornersBeforeResizing);

            int horizontalMultiplicator = GetHorizontalMultiplicator();
            int verticalMultiplicator = GetVerticalMultiplicator();

            transformTarget.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, transformTarget.rect.size.x - (horizontalMultiplicator * delta.x));
            transformTarget.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, transformTarget.rect.size.y + (verticalMultiplicator * delta.y));

            transformTarget.GetWorldCorners(rectCornersAfterResizing);

            Vector3 maxVectorDistanceWorldSpace = MaxMovementDelta(rectCornersBeforeResizing, rectCornersAfterResizing, GetConvertedTargetMousePosition());

            transformTarget.position = transformTarget.position + maxVectorDistanceWorldSpace;

            UpdateSelection();
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
