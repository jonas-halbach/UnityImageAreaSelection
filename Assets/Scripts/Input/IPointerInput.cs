
using UnityEngine;

namespace com.halbach.imageselection.input {
    public interface IPointerInput
    {
        void Initialize(RectTransform transformTarget, BoxCollider2D selectionRectCollider, Camera renderCamera);

        void UpdateInternal();
    }
}
