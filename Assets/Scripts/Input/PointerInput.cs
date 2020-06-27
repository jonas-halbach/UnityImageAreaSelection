using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.halbach.imageselection.input {
    public abstract class PointerInput : MonoBehaviour, IPointerInput
    {
        public event SelectionChanged OnSelectionChanged;
        private bool isInitialized = false;

        protected InputState pointerState;

        public void Initialize(RectTransform transformTarget, BoxCollider2D selectionRectCollider, Camera renderCamera)
        {
            
            InitializeInternal(transformTarget, selectionRectCollider, renderCamera);
            isInitialized = true;
        }

        public void SetEvents(SelectionChanged selectionChanged) {
            OnSelectionChanged += selectionChanged;
            pointerState.OnSelectionChanged += OnSelectionChanged;
        }
        protected abstract void InitializeInternal(RectTransform transformTarget, BoxCollider2D selectionRectCollider, Camera renderCamera);

        void Update() {
            if(isInitialized) {
                UpdateInternal();
            }
        }

        public abstract void UpdateInternal();

        public SelectionChanged GetOnSelectionChangedEvent() {
            return OnSelectionChanged;
        }

        public void AddOnSelectionChangedEvent(SelectionChanged selectionChangedEvent) {
            OnSelectionChanged += selectionChangedEvent;
        }

    }
}
