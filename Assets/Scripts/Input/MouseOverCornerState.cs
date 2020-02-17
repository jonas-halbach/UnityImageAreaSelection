using UnityEngine;
namespace com.halbach.imageselection.input {
    public class MouseOverCornerState : MouseInputState
    {
        public MouseOverCornerState(RectCorner mouseCorner, Texture2D mouseCursorTexture) : base(mouseCorner, mouseCursorTexture) {
         
        }
    }
}