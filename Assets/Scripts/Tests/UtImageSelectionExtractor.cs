using NUnit.Framework;
using UnityEngine;
using com.halbach.imageselection.image;

namespace com.halbach.UnityImageAreaSelection.Tests
{
    public class UtImageSelectionExtractor {

        [Test]
        public void ExtractSection_NoChanges_FullImageExpected() {
            //Arrange
            Color[] sourceImage = CreateSoureImage();
            Color[] expectedTargetImage = CreateSoureImage();

            ImageSelectionExtractor imageSelectionExtractor = new ImageSelectionExtractor(sourceImage, 3, 3);
            
            Rect selectionRect = new Rect(0, 0, 3, 3);

            //Action
            Color[] extractedImage = imageSelectionExtractor.ExtractSection(selectionRect);

            //Assert
            CompareArrays(extractedImage, expectedTargetImage);
        }

        [Test]
        public void ExtractSecion_MiddleSection1x1_ColorArrayLength1Red()
        {
             //Arrange
            Color[] sourceImage = CreateSoureImage();
            Color[] expectedTargetImage = new Color[1];
            expectedTargetImage[0] = Color.red;

            ImageSelectionExtractor imageSelectionExtractor = new ImageSelectionExtractor(sourceImage, 3, 3);
            
            Rect selectionRect = new Rect(1, 1, 1, 1);

            //Action
            Color[] extractedImage = imageSelectionExtractor.ExtractSection(selectionRect);

            //Assert
            CompareArrays(extractedImage, expectedTargetImage);
        }

        private void CompareArrays(Color[] extractedColors, Color[] expectedColors) {
            Assert.AreEqual(extractedColors.Length, expectedColors.Length, "Length of extracted array");

            for(int i = 0; i < expectedColors.Length; i++) {
                Assert.AreEqual(extractedColors[i], expectedColors[i], "Color array is diffrent at index position " + i);
            }
        }

        private Color[] CreateSoureImage()
        {
            Color[] sourceImage = new Color[9];

            sourceImage[0] = Color.black;
            sourceImage[1] = Color.grey;
            sourceImage[2] = Color.white;
            sourceImage[3] = Color.magenta;
            sourceImage[4] = Color.red; 
            sourceImage[5] = Color.yellow;
            sourceImage[6] = Color.green;
            sourceImage[7] = Color.blue;
            sourceImage[8] = Color.cyan;

            return sourceImage;
        }
    }
}