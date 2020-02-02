using UnityEngine;

namespace com.halbach.imageselection.image
{
    public class ArrayIndexTransformer 
    {
        public int TargetWidth{
            get; set;
        }

        public int TargetHeight{
            get; set;
        }

        public int OriginalHeight{
            get;set;
        }

        public int OriginalWidth {
            get;set;
        }

        private int UpperBorder {
            get; set;
        }

        private int LeftBorder {
            get; set;
        }

        private int RightBorder {
            get; set;
        }

        public ArrayIndexTransformer(int originalWidth, int originalHeight, int targetWidth, int targetHeight)
        {
            OriginalWidth = originalWidth;
            OriginalHeight = originalHeight;
            TargetWidth = targetWidth;
            TargetHeight = targetHeight;

            Initialize();
        }

        private void Initialize() {
            UpperBorder = CalculateUpperBorder();
            LeftBorder = CalculateLeftBorder();
            RightBorder = LeftBorder;
        }

        public int TransformTargetImageCoordinateToSourceImageIndex(int targetCol, int targetRow)
        {
            Vector2Int coordinateInOriginalImage = TransformTargetImageCoordinatesToOriginalCoordinates(targetCol, targetRow);
            return CalculateOriginalImageCoordinatesToIndex(coordinateInOriginalImage);
        }

        public Vector2Int TransformTargetImageCoordinatesToOriginalCoordinates(int targetCol, int targetRow)
        {
            int originalCol = targetCol - LeftBorder;
            int originalRow = targetRow - UpperBorder;

            originalCol = originalCol >= OriginalWidth ? -1 : originalCol;
            originalRow = originalRow >= OriginalHeight ? -1 : originalRow;

            return new Vector2Int(originalCol, originalRow);
        }

        public int CalculateOriginalImageCoordinatesToIndex(Vector2Int colRowVector)
        {
            return CalculateOriginalPixelIndexFromOriginalImageRowCol(colRowVector.x, colRowVector.y);
        }

        public int CalculateOriginalPixelIndexFromOriginalImageRowCol(int col, int row)
        {
            return CalculateArrayIndexFromColRow(col, row, OriginalWidth, OriginalHeight);
        }

        public int CalculateTargetPixelCoordinateToIndex(int col, int row)
        {
            return CalculateArrayIndexFromColRow(col, row, TargetWidth, TargetHeight);
        }

        private int CalculateArrayIndexFromColRow(int col, int row, int width, int height) {
            int index = -1;

            if(col >-1 && row > -1 && col < width && row < height)
            {
                index = row * width + col;
            }

            return index;
        }

        public int TransformTargetImageIndexToSourceImageIndex(int indexInTargetImage, int col, int row)
        {
            int startingRowOriginalImage = row - UpperBorder;
            int sideBorder = (LeftBorder + RightBorder);
            int pixelsOutOfOriginalImageBounds = startingRowOriginalImage * sideBorder;
            return indexInTargetImage - TargetWidth * UpperBorder - LeftBorder + pixelsOutOfOriginalImageBounds;
        }

        private int CalculateLeftBorder()
        {
            return  CalculateBorder(OriginalWidth,  TargetWidth);
        }

        private int CalculateUpperBorder(){
            return CalculateBorder(OriginalHeight, TargetHeight);
        }

        private int CalculateBorder(int originalImageSize, int targetImageSize)
        {
            return Mathf.Max((targetImageSize - originalImageSize) / 2, 0);
        }
    }
}
