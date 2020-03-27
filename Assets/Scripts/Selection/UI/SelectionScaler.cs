using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.halbach.imageselection.selection.ui
{
    public class SelectionScaler : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float aditionalColliderSize = 0.1f;

        [SerializeField]
        private BoxCollider2D myCollider;

        [SerializeField]
        private SelectionInteractionManager selectionInteractionManager;

        void Start () {
            Initialize();
        }

        private void Initialize() 
        {
            InitializeSelectionInteractionManager();
            InitialieSpriteRenderer();
            InitializeRectTransform();
            InitializeCollider();
        }

        private void InitializeSelectionInteractionManager()
        {
            if(selectionInteractionManager == null) {
                selectionInteractionManager = GetComponent<SelectionInteractionManager>();
            }
            if (selectionInteractionManager != null) {
                selectionInteractionManager.SelectionUpdated += Resize;
            }
        }

        private void InitialieSpriteRenderer() {
            if(spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        private void InitializeRectTransform() {
            if(rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
        }

        private void InitializeCollider()
        {
            if(myCollider == null) {
                myCollider = GetComponent<BoxCollider2D>();
            }
        }

        public void Resize(object sender, Rect selectionRect) 
        {
            ResizeTexture(selectionRect);
            ResizeCollider(selectionRect);
        }

        private void ResizeTexture(Rect selectionRect) 
        {
            spriteRenderer.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        }

        private void ResizeCollider(Rect selectionRect)
        {
            myCollider.size = new Vector2(rectTransform.rect.width + aditionalColliderSize, rectTransform.rect.height + aditionalColliderSize);
        }
    }
}