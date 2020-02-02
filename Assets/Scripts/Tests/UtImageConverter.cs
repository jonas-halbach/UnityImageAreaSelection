using NUnit.Framework;
using com.halbach.imageselection.image;
using UnityEngine;

namespace com.halbach.UnityImageAreaSelection.Tests
{
    public class UtImageConverter
    {
        [Test]
        public void ResizeImage_ResizeFrom1x1To3x3_AllPixelsShallBeWhiteButMiddleShallHaveColorOfInputArray()
        {
            //Arrange
            Color[] inputImage = new Color[1];
            inputImage[0] = Color.black;

            ImageConverter imageCoverter = new ImageConverter(1, 1, 3, 3);
            imageCoverter.FillingColor = Color.white;
    
            //Action
            Color[] outputImage = imageCoverter.ResizeImage(inputImage);

            //Assert
            Assert.AreEqual(Color.white, outputImage[0], "Test", null);
            Assert.AreEqual(Color.white, outputImage[1], "Test", null);
            Assert.AreEqual(Color.white, outputImage[2], "Test", null);
            Assert.AreEqual(Color.white, outputImage[3], "Test", null);
            Assert.AreEqual(Color.black, outputImage[4], "Test", null);
            Assert.AreEqual(Color.white, outputImage[5], "Test", null);
            Assert.AreEqual(Color.white, outputImage[6], "Test", null);
            Assert.AreEqual(Color.white, outputImage[7], "Test", null);
            Assert.AreEqual(Color.white, outputImage[8], "Test", null);
        }

        [Test]
        public void ResizeImage_ResizeFrom2x2To4x4_BorderShallBeWhiteButMiddleShallHaveColorOfInputArray()
        {
            //Arrange
            Color[] inputImage = new Color[4];
            inputImage[0] = Color.green;
            inputImage[1] = Color.yellow;
            inputImage[2] = Color.red;
            inputImage[3] = Color.blue;

            ImageConverter imageCoverter = new ImageConverter(2, 2, 4, 4);
            imageCoverter.FillingColor = Color.white;

            // Action
            Color[] outputImage = imageCoverter.ResizeImage(inputImage);

            // Assert : row 1
            Assert.AreEqual(Color.white, outputImage[0], "Test", null);
            Assert.AreEqual(Color.white, outputImage[1], "Test", null);
            Assert.AreEqual(Color.white, outputImage[2], "Test", null);
            Assert.AreEqual(Color.white, outputImage[3], "Test", null);

            // Assert : row 2
            Assert.AreEqual(Color.white, outputImage[4], "Test", null);
            Assert.AreEqual(Color.green, outputImage[5], "Test", null);
            Assert.AreEqual(Color.yellow, outputImage[6], "Test", null);
            Assert.AreEqual(Color.white, outputImage[7], "Test", null);

            // Assert : row 3
            Assert.AreEqual(Color.white,outputImage[8],  "Test", null);
            Assert.AreEqual(Color.red, outputImage[9], "Test", null);
            Assert.AreEqual( Color.blue,outputImage[10], "Test", null);
            Assert.AreEqual( Color.white, outputImage[11], "Test", null);
            
            // Assert : row 4
            Assert.AreEqual(Color.white, outputImage[12], "Test", null);
            Assert.AreEqual(Color.white, outputImage[13], "Test", null);
            Assert.AreEqual(Color.white, outputImage[14], "Test", null);
            Assert.AreEqual(Color.white, outputImage[15], "Test", null);
        }
    }
}
