using UnityEngine;


namespace com.halbach.imageselection.input {

    public delegate void SelectionMoved(object sender, Vector3 mouseMovementDelta);

    public class MouseInput : MonoBehaviour
    {
        public event SelectionMoved OnSelectionMoved;

        [SerializeField]
        private RectTransform transformTarget;

        [SerializeField]
        private float triggerDistance;

        [SerializeField]
        private Texture2D defaultCursorTexture;

        [SerializeField]
        private MousePropertyContainer mouseCursorTextureContainer;
        private IMouseInputState mouseInputState;

        // Start is called before the first frame update
        void Start()
        {
            mouseInputState = new MouseInputState(mouseCursorTextureContainer, transformTarget);
            mouseInputState.OnSelectionMoved += OnSelectionMoved;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateMousePos(); 
        }

        private void UpdateMousePos()
        {
            mouseInputState = mouseInputState.UpdateMousePostion(Input.mousePosition);
        } 

        void OnMouseDown() {
             mouseInputState = mouseInputState.MouseDown(Input.mousePosition);
        }

        void OnMouseUp() {
             mouseInputState = mouseInputState.MouseUp(Input.mousePosition);
        }
    }
}