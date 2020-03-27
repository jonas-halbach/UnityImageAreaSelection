using UnityEngine;
using System;

namespace com.halbach.imageselection.input {
    public class MouseOverCornerState : MouseDragState
    {
        public MouseOverCornerState(TargetMousePosition mouseCorner, MousePropertyContainer mousePropertyContainer, RectTransform transformTarget) 
                                        : base(mouseCorner, mousePropertyContainer, transformTarget)
        {
        }

        protected override void UpdateSelection(Vector3 delta)
        {
            transformTarget.sizeDelta = new Vector2(transformTarget.sizeDelta.x - delta.x, transformTarget.sizeDelta.y);

            UpdateSelection();
        }
    }
}