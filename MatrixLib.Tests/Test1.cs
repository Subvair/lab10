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

    [TestMethod]
    public void Multiply_MatricesWithSameSize_ShouldReturnCorrectProduct()
    {
      double[,] dataA = {
        { 1, 2 },
        { 3, 4 }
      };

      double[,] dataB = {
        { 2, 0 },
        { 1, 2 }
      };

      double[,] expectedData = {
        { 4, 4 },
        { 10, 8 }
      };

      SquareMatrix matrixA = CreateMatrixWithData(2, dataA);
      SquareMatrix matrixB = CreateMatrixWithData(2, dataB);
      SquareMatrix expectedMatrix = CreateMatrixWithData(2, expectedData);

      SquareMatrix resultMatrix = matrixA * matrixB;

      Assert.AreEqual(expectedMatrix, resultMatrix);
    }

    [DataTestMethod]
    [DataRow(1, new double[] { 5 }, 5)]
    [DataRow(2, new double[] { 1, 2, 3, 4 }, -2)]
    public void Determinant_ValidMatrix_ShouldReturnExpectedValue(int size, double[] flatValues, double expectedDeterminant)
    {
      double[,] matrixData = ConvertTo2DArray(flatValues, size);
      SquareMatrix matrix = CreateMatrixWithData(size, matrixData);

      double actualDeterminant = matrix.Determinant();

      Assert.AreEqual(expectedDeterminant, actualDeterminant, 1e-10);
    }
    [TestMethod]
    public void Equals_IdenticalMatrices_ShouldReturnTrue()
    {
      double[,] matrixData = {
        { 1, 2 },
        { 3, 4 }
      };

      SquareMatrix matrixA = CreateMatrixWithData(2, matrixData);
      SquareMatrix matrixB = CreateMatrixWithData(2, matrixData);

      Assert.IsTrue(matrixA.Equals(matrixB));
      Assert.IsTrue(matrixA == matrixB);
    }

    [TestMethod]
    public void CompareTo_CompareMatricesByDeterminant_ShouldWorkCorrectly()
    {
      double[,] dataA = {
        { 1, 2 },
        { 3, 4 }
      };

      double[,] dataB = {
        { 2, 0 },
        { 0, 2 }
      };

      SquareMatrix matrixA = CreateMatrixWithData(2, dataA); // det = -2
      SquareMatrix matrixB = CreateMatrixWithData(2, dataB); // det = 4

      Assert.IsTrue(matrixA < matrixB);
      Assert.IsTrue(matrixB > matrixA);
    }

  }
}
