using UnityEngine;
namespace com.halbach.imageselection.input {
    public class MouseOverCornerState : MouseInputState
    {
        public MouseOverCornerState(TargetMousePosition mouseCorner, Texture2D mouseCursorTexture) : base(mouseCorner, mouseCursorTexture) {
         
        }
    }
}