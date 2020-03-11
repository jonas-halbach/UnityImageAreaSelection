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
            RectTransform selection = this.rectTransform;

            Vector3[] localSelectionCorners = new Vector3[4];

            selection.GetWorldCorners(localSelectionCorners);

            Vector3 upperLeftSelectionCorner = localSelectionCorners[0];
            Vector3 lowerRightSelectionCorner = localSelectionCorners[2];

            Debug.Log("Upper left selection corner: " + upperLeftSelectionCorner + " lower right selection corner: " + lowerRightSelectionCorner);

            Vector4 upperLeftCornerScreenspace = TransformToScreenSpace(upperLeftSelectionCorner.x, upperLeftSelectionCorner.y); 
            currentSelection.x = upperLeftCornerScreenspace.x;
            currentSelection.y = upperLeftCornerScreenspace.y;

            Vector4 lowerRightCornerScreenSpace = TransformToScreenSpace(lowerRightSelectionCorner.x, lowerRightSelectionCorner.y); 
            
            Vector4 diagonalVector = lowerRightCornerScreenSpace - upperLeftCornerScreenspace;

            currentSelection.width = diagonalVector.x;
            currentSelection.height = diagonalVector.y;

            RaiseSelectionUpdatedEvent();
        }

        private Vector2 TransformToScreenSpace(float x, float y)
        {
            Vector4 pointInWorldSpace = new Vector4(x, y, 0, 1);
            return TransformToScreenSpace(pointInWorldSpace);
        } 

        private Vector4 TransformToScreenSpace(Vector2 point)
        {
            Vector4 pointInWorldSpace = new Vector4(point.x, point.y, 0, 1);
            return TransformToScreenSpace(pointInWorldSpace);
        }

        private Vector4 TransformToScreenSpace(Vector4 point)
        {
            return previewRenderCamera.WorldToScreenPoint(point);
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
