using UnityEngine;

namespace com.halbach.imageselection.image
{
    public class ImageConverter {

        private int OriginalWidth
        {
            get;set;
        } 

        private int OriginalHeight
        {
            get;set;
        }

        private int TargetWidth
        {
            get; set;
        }

        private int TargetHeight
        {
            get;set;
        }

        public Color FillingColor {
            get; set;
        }

        private Color[] targetImage;

        private int leftBorder;
        private int upperBorder;
        private int leftImageToIgnorePart;
        private int upperImageToIgnorePart;

        private ArrayIndexTransformer arrayIndexTransformer;

        public ImageConverter(int originalWidth, int originalHeight, int targetWidth, int targetHeight){
            OriginalWidth = originalWidth;
            OriginalHeight = originalHeight;
            TargetWidth = targetWidth;
            TargetHeight = targetHeight;
            
            FillingColor = Color.clear;
            targetImage = new Color[TargetWidth * TargetHeight];

            arrayIndexTransformer = new ArrayIndexTransformer(OriginalWidth, originalHeight, TargetWidth, targetHeight);
        }


        private void CalculateLeftImageToIgnorePart()
        {
            leftImageToIgnorePart = CalculateToIgnorePart(OriginalWidth, TargetWidth);
        }

        private void CalculateUpperImageToIgnorePart()
        {
            upperImageToIgnorePart = CalculateToIgnorePart(OriginalHeight, TargetHeight);
        }

        private int CalculateToIgnorePart(int originalSize, int targetSize)
        {
            return Mathf.Max(originalSize - targetSize, 0) / 2;
        }

        public Color[] ResizeImage(Color[] imageToResize) {
            for(int row = 0; row < TargetHeight; row++)
            {
                for(int col = 0; col < TargetWidth; col++)
                {
                    int targetImagePixelIndex = arrayIndexTransformer.CalculateTargetPixelCoordinateToIndex(col, row);
                    Vector2Int oringinalImageCoordinate = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(col, row);
                    int originalImageIndex = arrayIndexTransformer.CalculateOriginalImageCoordinatesToIndex(oringinalImageCoordinate);

                    if(originalImageIndex >= 0 && originalImageIndex < imageToResize.Length)   
                    {
                        targetImage[targetImagePixelIndex] = imageToResize[originalImageIndex];
                    }
                    else
                    {
                        targetImage[targetImagePixelIndex] = FillingColor;
                    }

                }
            }

            return targetImage;
        }

        private bool ShallTakePixelFromImage(int targetCol, int targetRow)
        {
           int indexInOriginalImage = arrayIndexTransformer.TransformTargetImageCoordinateToSourceImageIndex(targetCol, targetRow);

           return indexInOriginalImage >= 0;
        }

        private bool IsImagePixelColumnInOriginalImage(int targetCol)
        {
            return targetCol > leftBorder && targetCol < (TargetWidth - leftBorder); 
        }

        private bool IsImagePixelRowInTargetImage(int targetRow)
        {
            return targetRow > upperImageToIgnorePart && targetRow < TargetHeight - upperImageToIgnorePart;
        }
     }  
}