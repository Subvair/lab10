using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace MatrixCalculatorTests
{
  [TestClass]
  public class SquareMatrixTests
  {
    [TestMethod]
    public void Constructor_ValidSize_ShouldSetMatrixSize()
    {
      const int expectedSize = 3;

      SquareMatrix matrix = CreateMatrixWithData(expectedSize, new double[,] {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 9 }
      });

      Assert.AreEqual(expectedSize, matrix.Size);
    }

     [TestMethod]
    [ExpectedException(typeof(MatrixException))]
    public void Constructor_NonPositiveSize_ShouldThrowMatrixException()
    {
      _ = new SquareMatrix(0);
    }

    [TestMethod]
    public void Add_MatricesWithSameSize_ShouldReturnCorrectSum()
    {
      double[,] dataA = {
        { 1, 2 },
        { 3, 4 }
      };

      double[,] dataB = {
        { 5, 6 },
        { 7, 8 }
      };

      double[,] expectedData = {
        { 6, 8 },
        { 10, 12 }
      };

      SquareMatrix matrixA = CreateMatrixWithData(2, dataA);
      SquareMatrix matrixB = CreateMatrixWithData(2, dataB);
      SquareMatrix expectedMatrix = CreateMatrixWithData(2, expectedData);

      SquareMatrix resultMatrix = matrixA + matrixB;

      Assert.AreEqual(expectedMatrix, resultMatrix);
    }


  }
}
