using UnityEngine;

namespace com.halbach.imageselection.input {
    interface IMouseInputState
    {
        void MouseUp(Vector2 mousePosition);
        void MouseDown(Vector2 mousePosition);
        void UpdateMousePostion(Vector2 mousePosition);
    }
}