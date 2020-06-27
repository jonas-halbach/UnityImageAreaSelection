using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using com.halbach.imageselection.selection.RectModifier;

namespace com.halbach.imageselection.input {

    public class MobileSelectionRectMoveState : MobileInputState
    {
        BoxCollider2D collider;

        RectMover mover;

        Vector2 lastTouchPosition;

        bool lastTouchPositionSet = false;
        public MobileSelectionRectMoveState(RectTransform transformTarget, BoxCollider2D collider) : base (transformTarget)
        {
            mover = new RectMover(transformTarget);
        }

        protected override void UpdateInternal(List<Vector2> touchPositions, Vector2 delta)
        {
            mover.Move(delta);
        }

        protected override Vector2 CalculateDelta(List<Vector2> touchPositions) {
            Vector2 delta = Vector2.zero;
            
            if(lastTouchPositionSet) {
                if(touchPositions.Count >= 1) {
                    Vector2 newTouchPosition = touchPositions.FirstOrDefault();

                    delta = lastTouchPosition - newTouchPosition;
                }
            }

            if(touchPositions.Count >= 1) {
                lastTouchPosition = touchPositions.FirstOrDefault();
                lastTouchPositionSet = true;
            }

            return delta;
        }
    }
}