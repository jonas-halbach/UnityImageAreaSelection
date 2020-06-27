using UnityEngine;
using UnityEngine.UI; 

using System.Collections.Generic;

namespace com.halbach.imageselection.input {
    public class MobileInput : PointerInput
    {
        MobileInputState currentState;

        MobileInputStateEvaluator stateEvaluator;

        protected override void InitializeInternal(RectTransform transformTarget, BoxCollider2D selectionRectCollider, Camera renderCamera) {

            InitializePropertyContainer();

            currentState = new MobileInputState(transformTarget);

            stateEvaluator = new MobileInputStateEvaluator(renderCamera, transformTarget, selectionRectCollider);

            pointerState = stateEvaluator.GetNewState(currentState);
        }

        public override void UpdateInternal()
        {
            MobileInputState newState = stateEvaluator.GetNewState(currentState);
            if(newState != currentState) {
                currentState = newState;
                currentState.AddOnSelectionChangedEvent(GetOnSelectionChangedEvent());
            }
            List<Vector2> touchPositions = stateEvaluator.TouchPositionsWorlCoordinates;
            currentState.Update(touchPositions);
            
        }
    }
}
