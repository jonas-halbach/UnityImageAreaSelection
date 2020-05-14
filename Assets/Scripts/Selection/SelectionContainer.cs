using UnityEngine.UI;
using UnityEngine;
using com.halbach.imageselection.image;

namespace com.halbach.imageselection.selection
{
    public class SelectionContainer : MonoBehaviour {

        Image parentImage;

        [SerializeField]
        private Camera gameCamera;

        [SerializeField]
        private Vector2Int outPutImageSize = new Vector2Int(768, 768);

        [SerializeField]
        private Image previewImage;

        [SerializeField]
        private bool updatePreviewOnSelectionChange = false;

        SelectionInteractionManager selectionInteractionmanager;

        private RenderTexture imageSelection;

        private ImageConverter imageConverter;

        private Rect currentSelectionRect;

        void Start () {
            Initialize();
        }
        
        void Update () {
            
        }

        private void Initialize()
        {
            if ( parentImage == null )
            {
                parentImage = GetComponentInParent<Image>();
            }

            if( selectionInteractionmanager == null)
            {
                selectionInteractionmanager = GetComponentInChildren<SelectionInteractionManager>();
                selectionInteractionmanager.SelectionUpdated += SelectionUpdated;
            }

            InitializeCamera();
            InitializeImageConverter();
        }

        private void InitializeCamera()
        {
            if ( gameCamera == null )
            {
                gameCamera = Camera.main;
            }

            Rect viewportRect = Camera.main.pixelRect;
            float viewportWith = viewportRect.width;
            float viewportHeight = viewportRect.height;

            imageSelection = new RenderTexture((int)viewportWith, (int)viewportHeight, 32);
            
        }

        private void InitializeImageConverter() {
            imageConverter = new ImageConverter(imageSelection.width, imageSelection.height, outPutImageSize.x, outPutImageSize.y);
            imageConverter.FillingColor = Color.gray;
        }

        private void SelectionUpdated(object sender, Rect selectionRect )
        {
            currentSelectionRect = selectionRect;

            if(updatePreviewOnSelectionChange) {
                if (imageSelection != null ) {

                    Color[] selectedImageExtraction = GrabSelectionImage(selectionRect);

                    UpdatePreviewImage(selectedImageExtraction);
                }
            }
        }

        public Color[] GrabSelectionImage() {
            Color[] selectionImage;
            if(currentSelectionRect != null) {
                selectionImage = GrabSelectionImage(currentSelectionRect);
            } else {
                selectionImage = new Color[0];
            }
            return selectionImage;
        }


        private Color[] GrabSelectionImage(Rect selectionRect) {
                    Color[] imageColors = CaptureCameraImage();

                    Color[] extractedSection = ExtractSection(imageColors, selectionRect);

                    Color[] transformedImage = TransformImageDimensions(extractedSection, selectionRect, outPutImageSize);
                    
                    return transformedImage;
        }

        private Color[] CaptureCameraImage() {
            gameCamera.targetTexture = imageSelection;
            Texture2D tex = GetRTPixels(imageSelection);
            Color[] imageColors = tex.GetPixels();

            return imageColors;
        }

        private Color[] ExtractSection(Color[] imageColors, Rect selectionRect)
        {
            ImageSelectionExtractor selectionExtractor = new ImageSelectionExtractor(imageColors, imageSelection.width, imageSelection.height);
            Color[] extractedSection = selectionExtractor.ExtractSection(selectionRect);

            return extractedSection;
        }

        private Color[] TransformImageDimensions(Color[] inputImage, Rect selectionRect, Vector2Int outPutImageSize) {
            imageConverter = new ImageConverter((int)selectionRect.width, (int)selectionRect.height, outPutImageSize.x, outPutImageSize.y);
            Color[] scaledImage = imageConverter.ResizeImage(inputImage);

            return scaledImage;
        }

        /// <summary>
        /// Copied from: https://docs.unity3d.com/ScriptReference/RenderTexture-active.html 
        /// Date: 19.05.2019
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        static public Texture2D GetRTPixels( RenderTexture rt )
        {
            
            // Remember currently active render texture
            RenderTexture currentActiveRT = RenderTexture.active;

            // Set the supplied RenderTexture as the active one
            RenderTexture.active = rt;

            // Create a new Texture2D and read the RenderTexture image into it
            Texture2D tex = new Texture2D(rt.width, rt.height);
            tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

            // Restore previously active render texture
            RenderTexture.active = currentActiveRT;

            return tex;
        }

        private void UpdatePreviewImage(Color[] image)
        {
            if ( previewImage != null )
            {
                previewImage.sprite.texture.SetPixels(image);
                previewImage.sprite.texture.Apply();
            } else {
                Debug.LogError("You did not specify an Image on which the preview shall be rendered. Please specify a preview image!");
            }
        }
    }
}