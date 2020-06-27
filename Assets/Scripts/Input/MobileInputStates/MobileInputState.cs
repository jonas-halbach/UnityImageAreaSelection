using UnityEngine;
using System.Collections.Generic;

namespace com.halbach.imageselection.input {
    public class MobileInputState : InputState
    {
        protected RectTransform transformTarget;

        public Vector2 oldDelta;

        public MobileInputState(RectTransform transformTarget) { 
            this.transformTarget = transformTarget;
            oldDelta = Vector2.zero; 
        }

        public void Update(List<Vector2> touchPositions)
        {
            Vector2 newDelta = CalculateDelta(touchPositions);
            if(DidDeltaChange(newDelta)) {
                UpdateInternal(touchPositions, newDelta);
                UpdateSelection();
            }
            oldDelta = newDelta; 
        }

        protected virtual void UpdateInternal(List<Vector2> touchPositions, Vector2 delta) { 

        }

        protected virtual Vector2 CalculateDelta(List<Vector2> touchPositions) {
            return Vector2.zero;
        }

        protected bool DidDeltaChange(Vector2 newDelta) {
            return (newDelta - oldDelta).magnitude > float.Epsilon;
        }
    }
}