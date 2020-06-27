using UnityEngine;
using System.Collections.Generic;

namespace com.halbach.imageselection.input {
    public class MobileInputStateEvaluator
    {
        MobileInputState currentState;

        int currentTouchCount = 0;

        Camera camera;

        BoxCollider2D selectionRectCollider;

        RectTransform selectionRect;

        public List<Vector2> TouchPositions {
            get; private set;
        }

        public List<Vector2> TouchPositionsWorlCoordinates {
            get; private set;
        }

        public MobileInputStateEvaluator(Camera camera, RectTransform selectionRect, BoxCollider2D selectionRectCollider) {
            this.camera = camera;
            this.selectionRectCollider = selectionRectCollider;
            this.selectionRect = selectionRect;
            List<Touch> currentTouches = GetTouches();
            UpdateTouchPositions(currentTouches);
            currentTouchCount = TouchPositions.Count;
        }

        public MobileInputState GetNewState(MobileInputState currentState) {
            
            MobileInputState newState = currentState;
            
            if(Application.isMobilePlatform) {
                List<Touch> currentTouches = GetTouches();
                UpdateTouchPositions(currentTouches);

                int updatedTouchCount = TouchPositions.Count;
                if(DidStateChange(updatedTouchCount)) {

                currentTouchCount = updatedTouchCount;

                newState = GetMobileInputStateByTouchCount(currentTouchCount);
                }
            }
            else {
                if(Input.GetMouseButtonDown(0)) {
                    DidRaycastHitSelectionRectCollider(Input.mousePosition);
                }
            }

            return newState;
        }

        private List<Touch> GetTouches() {
            List<Touch> touches = new List<Touch>();
            int touchCount = Input.touchCount;
            for(int i = 0; i < touchCount; i++) {
                Touch currentTouch = Input.GetTouch(i);
                touches.Add(currentTouch);
            }
            return touches;
        }

        private bool DidStateChange(int currentTouchCount) { 
            return currentTouchCount != this.currentTouchCount;
        }

        private void UpdateTouchPositions(List<Touch> touches) {
            TouchPositions = new List<Vector2>();
            TouchPositionsWorlCoordinates = new List<Vector2>();

            for(int i = 0; i < touches.Count; i++) {

                Vector2 touchPosition = touches[i].position;

                bool didHit = DidRaycastHitSelectionRectCollider(touchPosition);
                if(didHit) {
                    TouchPositions.Add(touchPosition);
                    Vector3 touchPosition3d = new Vector3(touchPosition.x, touchPosition.y, 0);
                    Vector3 touchPositionWorldCoordinates = Camera.main.ScreenToWorldPoint(touchPosition3d);
                    TouchPositionsWorlCoordinates.Add(touchPositionWorldCoordinates);
                }
            }
        }

        private bool DidRaycastHitSelectionRectCollider(Vector2 touchPosition) {
            bool didHit = false;

            Ray raycast = Camera.main.ScreenPointToRay(touchPosition);
            Debug.DrawRay(raycast.origin, raycast.direction * 1000000, Color.red, 1000);

            RaycastHit2D raycastHit  = Physics2D.Raycast(raycast.origin, raycast.direction);
            if (raycastHit != null)
            {
                
                if(raycastHit.collider != null) {
                    if (raycastHit.collider.Equals(selectionRectCollider))
                    {
                        didHit = true;
                    }
                }
            }

            return didHit;
        }

        private MobileInputState GetMobileInputStateByTouchCount(int touchCount) {
            MobileInputState mobileInputState = null;
            
            if(touchCount == 1) {
                mobileInputState = new MobileSelectionRectMoveState(selectionRect, selectionRectCollider);
            } else if(touchCount == 2) {
                mobileInputState = new MobileSelectionRectScaleState(selectionRect);
            } else {
                mobileInputState = new MobileInputState(selectionRect);
            }

            return mobileInputState;
        }
    }
}