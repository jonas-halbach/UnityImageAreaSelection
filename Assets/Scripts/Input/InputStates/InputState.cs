using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.halbach.imageselection.input {
    public class InputState
    {
        public event SelectionChanged OnSelectionChanged;

        public void AddOnSelectionChangedEvent(SelectionChanged selectionChangedEvent) {
            OnSelectionChanged += selectionChangedEvent;
        }

        public SelectionChanged GetOnSelectionChangedEvent() {
            return OnSelectionChanged;
        }

        protected void UpdateSelection() {
            if(OnSelectionChanged != null)
            {
                OnSelectionChanged(this);
            }
        }
    }
}
