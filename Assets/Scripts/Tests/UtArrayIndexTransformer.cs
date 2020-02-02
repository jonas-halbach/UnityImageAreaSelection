using NUnit.Framework;
using UnityEngine.TestTools;
using com.halbach.imageselection.image;
using UnityEngine;


namespace com.halbach.UnityImageAreaSelection.Tests
{
    public class UtArrayIndexTransformer
    {
        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform_1_1From3x3To1x1Array_Returns_0_0()
        {
            //Arrange
            int originalWidth = 1;
            int originalHeight = 1;
            int targetWidth = 3;
            int targetHeight = 3;

            int colInTargetArray = 1;
            int rowInTargetArray = 1;

            int expectedColCoordinate = 0;
            int expectedRowCoordinate = 0;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform_0_0From3x3To1x1Array_Returns_n1_n1()
        {
            //Arrange
            int originalWidth = 1;
            int originalHeight = 1;
            int targetWidth = 3;
            int targetHeight = 3;

            int colInTargetArray = 0;
            int rowInTargetArray = 0;

            int expectedColCoordinate = -1;
            int expectedRowCoordinate = -1;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform_1_1From4x4To2x2_Returns_0_0()
        {
            //Arrange
            int originalWidth = 2;
            int originalHeight = 2;
            int targetWidth = 4;
            int targetHeight = 4;

            int colInTargetArray = 1;
            int rowInTargetArray = 1;

            int expectedColCoordinate = 0;
            int expectedRowCoordinate = 0;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform_2_1From4x4To2x2Array_Return_1_0()
        {
            //Arrange
            int originalWidth = 2;
            int originalHeight = 2;
            int targetWidth = 4;
            int targetHeight = 4;

            int colInTargetArray = 2;
            int rowInTargetArray = 1;

            int expectedColCoordinate = 1;
            int expectedRowCoordinate = 0;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform_1_2From4x4To2x2Array_Returns_0_1()
        {
            //Arrange
            int originalWidth = 2;
            int originalHeight = 2;
            int targetWidth = 4;
            int targetHeight = 4;

            int colInTargetArray = 1;
            int rowInTargetArray = 2;

            int expectedColCoordinate = 0;
            int expectedRowCoordinate = 1;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray  = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform_2_2From4x4To2x2Array_Returns1_1()
        {
            //Arrange
            int originalWidth = 2;
            int originalHeight = 2;
            int targetWidth = 4;
            int targetHeight = 4;

            int colInTargetArray = 2;
            int rowInTargetArray = 2;

            int expectedColCoordinate = 1;
            int expectedRowCoordinate = 1;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray  = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform_0_2IndexFrom4x4To2x2Array_Returns_n1_1()
        {
            //Arrange
            int originalWidth = 2;
            int originalHeight = 2;
            int targetWidth = 4;
            int targetHeight = 4;

            int colInTargetArray = 0;
            int rowInTargetArray = 2;

            int expectedColCoordinate = -1;
            int expectedRowCoordinate = 1;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray  = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform_1_1From3x4To1x2Array_Returns_0_0()
        {
            //Arrange
            int originalWidth = 1;
            int originalHeight = 2;
            int targetWidth = 3;
            int targetHeight = 4;

            int colInTargetArray = 1;
            int rowInTargetArray = 1;

            int expectedColCoordinate = 0;
            int expectedRowCoordinate = 0;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void TransformTargetImageCoordinatesToOriginalCoordinates_Transform1_2_From3x4To1x2Array_Returns_0_1()
        {
            //Arrange
            int originalWidth = 1;
            int originalHeight = 2;
            int targetWidth = 3;
            int targetHeight = 4;

            int colInTargetArray = 1;
            int rowInTargetArray = 2;

            int expectedColCoordinate = 0;
            int expectedRowCoordinate = 1;

            Vector2Int expectedOriginalCoordinate = new Vector2Int(expectedColCoordinate, expectedRowCoordinate);

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            Vector2Int coordinatesInOriginalArray = arrayIndexTransformer.TransformTargetImageCoordinatesToOriginalCoordinates(colInTargetArray, rowInTargetArray);
        
            //Assert
            Assert.AreEqual(expectedOriginalCoordinate, coordinatesInOriginalArray, "Transformed index of target array in source array");
        }

        [Test]
        public void CalculateTargetPixelCoordinateToIndex_Row1Col1_Returns4()
        {
            //Arrange
            int originalWidth = 1;
            int originalHeight = 1;
            int targetWidth = 3;
            int targetHeight = 3;

            int row = 1;
            int col = 1;

            int expectedIndex = 4;

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            int indexInTargetArray = arrayIndexTransformer.CalculateTargetPixelCoordinateToIndex(row, col);

            //Assert
            Assert.AreEqual(expectedIndex, indexInTargetArray, "Index in target Array");
        }

        [Test]
        public void CalculateOriginalPixelIndexFromOriginalImageRowCol_n1_1_Returnsn1() {
            //Arrange
            int originalWidth = 2;
            int originalHeight = 2;
            int targetWidth = 4;
            int targetHeight = 4;

            int col = -1;
            int row = 1;

            int expectedIndex = -1;

            ArrayIndexTransformer arrayIndexTransformer = new ArrayIndexTransformer(originalWidth, originalHeight, targetWidth, targetHeight);

            //Action
            int indexInTargetArray = arrayIndexTransformer.CalculateOriginalPixelIndexFromOriginalImageRowCol(col, row);

            //Assert
            Assert.AreEqual(expectedIndex, indexInTargetArray, "Index in target Array");
        }
    }
}
