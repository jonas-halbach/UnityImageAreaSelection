using UnityEngine;
using System;

namespace com.halbach.imageselection.input {
    public class MouseOverCornerState : MouseInputState
    {
        public MouseOverCornerState(TargetMousePosition mouseCorner, MousePropertyContainer mousePropertyContainer, RectTransform transformTarget) 
                                        : base(mouseCorner, mousePropertyContainer, transformTarget)
        {
        }
    }
}