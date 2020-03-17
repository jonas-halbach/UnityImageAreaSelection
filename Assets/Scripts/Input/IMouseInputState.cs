using UnityEngine;

namespace com.halbach.imageselection.input {
    public interface IMouseInputState
    {
        event SelectionChanged OnSelectionChanged;
        IMouseInputState MouseUp(Vector2 mousePosition);
        IMouseInputState MouseDown(Vector2 mousePosition);
        IMouseInputState UpdateMousePostion(Vector2 mousePosition);
    }
}