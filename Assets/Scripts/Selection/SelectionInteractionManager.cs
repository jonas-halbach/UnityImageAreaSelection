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

        private MouseInput input;

        private void Start()
        {
            InitializeCollider();
            InitializeSelection();
            InitializeEvents();
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

        private void InitializeEvents()
        {
            input = GetComponent<MouseInput>();
            if(input != null)
            {
                input.OnSelectionMoved += OnUpdateSelection;
            }
        }

        private void UpdateSelectionIndicatorPosition(Vector3 delta)
        {
            Vector3 oldRectPosition = rectTransform.position;
            oldRectPosition.x -= delta.x;
            oldRectPosition.y -= delta.y;
            rectTransform.position = oldRectPosition;

            UpdateSelection();
        }

        private void OnUpdateSelection(object sender, Vector3 delta)
        {
            UpdateSelectionIndicatorPosition(delta);
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
