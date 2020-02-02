using UnityEngine;

namespace com.halbach.imageselection.image {
    public class ImageSelectionExtractor {

        Color[] SoureImage {get;set;}
        int SourceWidth {get; set;}
        int SourceHeight {get; set;}

        public ImageSelectionExtractor(Color[] sourceImage, int sourceWidth, int sourceHeight)
        {
            SoureImage = sourceImage;
            SourceWidth = sourceWidth;
            SourceHeight = sourceHeight;
        }

        public Color[] ExtractSection(Rect selectionRect)
        {
            Color[] extractedSection = new Color[(int)(selectionRect.width * selectionRect.height)];

            int indexInExtractedSection = 0;
            ArrayIndexTransformer indexTransformer = new ArrayIndexTransformer(SourceWidth, SourceHeight, (int)selectionRect.width, (int)selectionRect.height);

            int startCol = (int) (selectionRect.x);
            int endCol = (int) (startCol + selectionRect.width);
            
            int startRow = (int) (selectionRect.y);
            int endRow = (int) (startRow + selectionRect.height);

            for(int row = startRow; row < endRow; row++) {
                for (int col = startCol; col < endCol; col++) {
                    int indexInOriginalImage = indexTransformer.CalculateOriginalPixelIndexFromOriginalImageRowCol(col, row);
                    Color pixelColor = Color.clear;
                    if(indexInOriginalImage >= 0)
                    {
                        pixelColor = SoureImage[indexInOriginalImage];
                    }
                    
                    extractedSection[indexInExtractedSection] = pixelColor;
                    indexInExtractedSection++;
                }
            }
            return extractedSection;
        }
    }
}
