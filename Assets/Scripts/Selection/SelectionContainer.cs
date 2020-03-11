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

        SelectionInteractionManager selectionInteractionmanager;

        private RenderTexture imageSelection;

        private ImageConverter imageConverter;

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
                Debug.Log("BG Image: " + parentImage.rectTransform.rect);
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
            if ( imageSelection != null )
            {
                Debug.Log("Selection updated: " + sender);

                gameCamera.targetTexture = imageSelection;
                Texture2D tex = GetRTPixels(imageSelection);
                Color[] imageColors = tex.GetPixels();

                Debug.LogWarning("SelectionRect, width:" + selectionRect.width + " height: " + selectionRect.height);

                ImageSelectionExtractor selectionExtractor = new ImageSelectionExtractor(imageColors, imageSelection.width, imageSelection.height);

                //selectionRect = new Rect (0, 0, 724, 724);

                imageColors = selectionExtractor.ExtractSection(selectionRect);
                //gameCamera.targetTexture = null;
                
                //Color[] imageColors = GetRandomColorArray(tex.width, tex.height, 50);
                //Color[] imageColors = tex.GetPixels((int)selectionRect.x, (int)selectionRect.y, (int)selectionRect.width, (int)selectionRect.height);

                imageConverter = new ImageConverter((int)selectionRect.width, (int)selectionRect.height, outPutImageSize.x, outPutImageSize.y);
                Color[] scaledImage = imageConverter.ResizeImage(imageColors);
                
                UpdatePreviewImage(scaledImage);
            }
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
                Debug.Log("PreviewImageRect: " + previewImage.rectTransform.rect);
                previewImage.sprite.texture.SetPixels(image);
                previewImage.sprite.texture.Apply();
            }
        }

        private Color[] GetRandomColorArray(int width, int height, int colorCount)
        {
            Color[] colors = new Color[width * height];
            Color[] imageColors = new Color[colorCount];
            System.Random random = new System.Random();
            
            
            for(int i = 0; i < imageColors.Length; i++)
            {
                float r = 1.0f / random.Next(50);
                float g = 1.0f / random.Next(50);
                float b = 1.0f / random.Next(50);

                imageColors[i] = new Color(r, g, b);
            }

            for(int i = 0; i < colors.Length; i++)
            {
                colors[i] = imageColors[random.Next(colorCount)];
            }

            return colors;
        }
    }
}