using UnityEngine;

namespace com.halbach.imageselection.input {
    public class ScaleState : MouseInputState {

        public ScaleState(float triggerDistance, InputPropertyContainer mousePropertyContainer, RectTransform transformTarget) : 
                                    base(mousePropertyContainer, transformTarget)
        {
            currentMousePosition = Vector2.zero;
        }

        public override IMouseInputState MouseUp(Vector2 mousePos)
        {
            base.MouseUp(mousePos);

            return new MouseInputState(propertyContainer, transformTarget);
        }

    }
}