using UnityEngine;
using com.halbach.imageselection.input;

namespace com.halbach.imageselection.selection
{
    public delegate void SelectionUpdatedEventHandler(object sender, Rect selectionRect);

    public class SelectionInteractionManager : MonoBehaviour {

        public event SelectionUpdatedEventHandler SelectionUpdated;

        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private float movingSpeed = 0.02f;

        [SerializeField]
        private Camera previewRenderCamera;

        private BoxCollider2D selectionCollider;

        private Rect currentSelection;

        private PointerInput input;

        public RectTransform RectTransform {
            get {return rectTransform;}
            set {this.rectTransform = value;} 
        }

        public Camera PreviewCamera {
            get {return previewRenderCamera;}
            set {this.previewRenderCamera = value;}
        }
        private void Start()
        {
            InitializeCollider();
            InitializeSelection();
        }

        private void OnSelectionRectChanged()
        {
            UpdateSelection();
            Debug.Log(currentSelection);
        }

        private void InitializeCollider()
        {
            selectionCollider = GetComponent<BoxCollider2D>();
            if ( selectionCollider == null)
            {
                selectionCollider = gameObject.AddComponent<BoxCollider2D>();
                selectionCollider.size = Vector2.one;
            }
        }

        private void InitializeSelection()
        {
            currentSelection = new Rect();
            UpdateSelection();
        }

        public void InitializeEvents()
        {
            input = GetComponent<PointerInput>();
            if(input != null)
            {
                input.SetEvents(OnUpdateSelection);
            }
        }

        private void OnUpdateSelection(object sender)
        {
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            SelectionToScreenSpaceConverter converter = new SelectionToScreenSpaceConverter(this.rectTransform, previewRenderCamera);
        
            Vector3 upperLeftCornerScreenspace  = converter.UpperLeftCornerScreenSpace;

            currentSelection.x = upperLeftCornerScreenspace.x;
            currentSelection.y = upperLeftCornerScreenspace.y;

            currentSelection.width = converter.WidthScreenSpace;
            currentSelection.height = converter.HeightScreenSpace;

            RaiseSelectionUpdatedEvent();
        }

        private void RaiseSelectionUpdatedEvent()
        {
            if ( SelectionUpdated != null )
            {
                SelectionUpdated(this, currentSelection);
            }
        }
    }
}
