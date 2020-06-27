using UnityEngine;

namespace com.halbach.imageselection.selection.RectModifier
{
    public class RectMover
    {
        protected RectTransform transformTarget;
        public RectMover(RectTransform transformTarget) {
            this.transformTarget = transformTarget;
        }

        public void Move(Vector3 delta) { 
                Vector3 oldRectPosition = transformTarget.position;
                oldRectPosition.x -= delta.x;
                oldRectPosition.y -= delta.y;
                transformTarget.position = oldRectPosition;
        }
    }
}
