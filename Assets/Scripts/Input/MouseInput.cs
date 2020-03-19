using UnityEngine;


namespace com.halbach.imageselection.input {

    public delegate void SelectionChanged(object sender);

    public class MouseInput : MonoBehaviour
    {
        public event SelectionChanged OnSelectionChanged;

        [SerializeField]
        private RectTransform transformTarget;

        [SerializeField]
        private float triggerDistance;

        [SerializeField]
        private Texture2D defaultCursorTexture;

        [SerializeField]
        private MousePropertyContainer mouseCursorTextureContainer;
        private IMouseInputState mouseInputState;

        void Start()
        {
            mouseInputState = new MouseInputState(mouseCursorTextureContainer, transformTarget);
            mouseInputState.OnSelectionChanged += OnSelectionChanged;
        }

        void Update()
        {
            UpdateMousePos(); 
        }

        private void UpdateMousePos()
        {
            mouseInputState = mouseInputState.UpdateMousePostion(Input.mousePosition);
        } 

        void OnMouseDown() 
        {
            mouseInputState = mouseInputState.MouseDown(Input.mousePosition);
        }

        void OnMouseUp() {
            mouseInputState = mouseInputState.MouseUp(Input.mousePosition);
        }
    }
}