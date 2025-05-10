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
    
  }
}
