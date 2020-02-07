using UnityEngine;


namespace com.halbach.imageselection.input {
    public class MouseInput : MonoBehaviour
    {

        [SerializeField]
        private RectTransform transformTarget;

        [SerializeField]
        private float triggerDistance;

        [SerializeField]
        private Texture2D defaultCursorTexture;

        [SerializeField]
        private Texture2D scaleUpperLeftCornerCurserTexture;
        
        [SerializeField]
        private Texture2D scaleUpperRightCornerCurserTexture;


        [SerializeField]
        private Texture2D scaleLowerRightCornerCurserTexture;


        [SerializeField]
        private Texture2D scaleLowerLeftCornerCurserTexture;

        private IMouseInputState mouseInputState;

        // Start is called before the first frame update
        void Start()
        {
            mouseInputState = new MouseInputState(triggerDistance, transformTarget, defaultCursorTexture,
                                                    scaleUpperLeftCornerCurserTexture, scaleUpperRightCornerCurserTexture,
                                                    scaleLowerRightCornerCurserTexture, scaleLowerLeftCornerCurserTexture);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateMousePos(); 
        }

        private void UpdateMousePos()
        {
            mouseInputState.UpdateMousePostion(Input.mousePosition);
        } 
    }
}