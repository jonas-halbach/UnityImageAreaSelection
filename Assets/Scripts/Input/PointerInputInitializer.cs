using com.halbach.imageselection.selection;
using UnityEngine;

namespace com.halbach.imageselection.input {
    public class PointerInputInitializer : MonoBehaviour
    {
        [SerializeField]
        RectTransform transformTarget;
        
        [SerializeField]
        BoxCollider2D selectionRectCollider;
        
        [SerializeField]
        Camera renderCamera;

        [SerializeField]
        SelectionInteractionManager interactionManager;

        private PointerInput input;

        void Start()
        {
            Initialize();
        }

        void Initialize() {
            InitializeInput();
            InitializeInteractionManager();
            InitializeInteractionManagerEvents();
        }

        void InitializeInput() {
            bool isMobilePlatform = Application.isMobilePlatform;
            if(isMobilePlatform) {
                AddMobileInput();
            } else {
                AddMouseInput();
            }
            input.Initialize(transformTarget, selectionRectCollider, renderCamera);
        }

        void InitializeInteractionManager() {
            if(interactionManager == null) {
                interactionManager = gameObject.GetComponent<SelectionInteractionManager>();
                if(interactionManager == null)
                {
                    interactionManager = gameObject.AddComponent<SelectionInteractionManager>();
                    interactionManager.PreviewCamera = renderCamera;
                    interactionManager.RectTransform = transformTarget;
                }
            }
        }

        void InitializeInteractionManagerEvents() {
            interactionManager.InitializeEvents();
        }

        void AddMouseInput() {
            input = gameObject.AddComponent<MouseInput>();
        }

        void AddMobileInput() {
            input = gameObject.AddComponent<MobileInput>();
        }
    }
}