using System;

public class SquareMatrix
{
  private double[,] _data;

  public int Size { get; }

  public SquareMatrix(int size, bool useRandom = false)
  {
    if (size <= 0)
      throw new MatrixException("Размер матрицы должен быть положительным");

    Size = size;
    _data = new double[size, size];

    if (useRandom)
      FillWithRandomValues();
    else
      FillFromConsole();
  }

  private SquareMatrix(int size, double[,] data)
  {
    Size = size;
    _data = data;
  }

  private void FillFromConsole()
  {
    Console.WriteLine($"Введите элементы матрицы {Size}x{Size}:");

    for (int rowIndex = 0; rowIndex < Size; rowIndex++)
    {
      for (int columnIndex = 0; columnIndex < Size; columnIndex++)
      {
        Console.Write($"[{rowIndex + 1}, {columnIndex + 1}]: ");
        string? input = Console.ReadLine();
        _data[rowIndex, columnIndex] = double.Parse(input!);
      }
    }
  }

  private void FillWithRandomValues()
  {
    Random random = new Random();

    for (int rowIndex = 0; rowIndex < Size; rowIndex++)
    {
      for (int columnIndex = 0; columnIndex < Size; columnIndex++)
      {
        _data[rowIndex, columnIndex] = random.NextDouble() * 10;
      }
    }
  }

  public static SquareMatrix operator +(SquareMatrix leftMatrix, SquareMatrix rightMatrix)
  {
    if (leftMatrix.Size != rightMatrix.Size)
      throw new MatrixException("Матрицы должны быть одного размера");

    double[,] resultData = new double[leftMatrix.Size, leftMatrix.Size];

    for (int rowIndex = 0; rowIndex < leftMatrix.Size; rowIndex++)
    {
      for (int columnIndex = 0; columnIndex < leftMatrix.Size; columnIndex++)
      {
        resultData[rowIndex, columnIndex] =
          leftMatrix._data[rowIndex, columnIndex] + rightMatrix._data[rowIndex, columnIndex];
      }
    }

    return new SquareMatrix(leftMatrix.Size, resultData);
  }

  public static SquareMatrix operator *(SquareMatrix leftMatrix, SquareMatrix rightMatrix)
  {
    if (leftMatrix.Size != rightMatrix.Size)
      throw new MatrixException("Матрицы должны быть одного размера");

    double[,] resultData = new double[leftMatrix.Size, leftMatrix.Size];

    for (int rowIndex = 0; rowIndex < leftMatrix.Size; rowIndex++)
    {
      for (int columnIndex = 0; columnIndex < leftMatrix.Size; columnIndex++)
      {
        for (int sharedIndex = 0; sharedIndex < leftMatrix.Size; sharedIndex++)
        {
          resultData[rowIndex, columnIndex] +=
            leftMatrix._data[rowIndex, sharedIndex] * rightMatrix._data[sharedIndex, columnIndex];
        }
      }
    }

    return new SquareMatrix(leftMatrix.Size, resultData);
  }

  public double Determinant()
  {
    if (Size == 1)
      return _data[0, 0];

    if (Size == 2)
      return _data[0, 0] * _data[1, 1] - _data[0, 1] * _data[1, 0];

    double determinant = 0;

    for (int columnIndex = 0; columnIndex < Size; columnIndex++)
    {
      double multiplier = (columnIndex % 2 == 0 ? 1 : -1) * _data[0, columnIndex];
      determinant += multiplier * GetMinor(0, columnIndex).Determinant();
    }

    return determinant;
  }

  private SquareMatrix GetMinor(int excludedRow, int excludedColumn)
  {
    double[,] minorData = new double[Size - 1, Size - 1];

    for (int rowIndex = 0, minorRow = 0; rowIndex < Size; rowIndex++)
    {
      if (rowIndex == excludedRow)
        continue;

      for (int columnIndex = 0, minorCol = 0; columnIndex < Size; columnIndex++)
      {
        if (columnIndex == excludedColumn)
          continue;

        minorData[minorRow, minorCol] = _data[rowIndex, columnIndex];
        minorCol++;
      }

      minorRow++;
    }

    return new SquareMatrix(Size - 1, minorData);
  }

  public static bool operator >(SquareMatrix left, SquareMatrix right) =>
    left.Determinant() > right.Determinant();

  public static bool operator <(SquareMatrix left, SquareMatrix right) =>
    left.Determinant() < right.Determinant();

  public static bool operator >=(SquareMatrix left, SquareMatrix right) =>
    left.Determinant() >= right.Determinant();

  public static bool operator <=(SquareMatrix left, SquareMatrix right) =>
    left.Determinant() <= right.Determinant();

  public static bool operator ==(SquareMatrix left, SquareMatrix right) =>
    left.Equals(right);

  public static bool operator !=(SquareMatrix left, SquareMatrix right) =>
    !left.Equals(right);

  public static explicit operator double(SquareMatrix matrix) =>
    matrix.Determinant();

  public override bool Equals(object? obj)
  {
    if (obj is not SquareMatrix otherMatrix || Size != otherMatrix.Size)
      return false;

    for (int rowIndex = 0; rowIndex < Size; rowIndex++)
    {
      for (int columnIndex = 0; columnIndex < Size; columnIndex++)
      {
        if (_data[rowIndex, columnIndex] != otherMatrix._data[rowIndex, columnIndex])
          return false;
      }
    }

    return true;
  }

  public override int GetHashCode() =>
    _data.GetHashCode();

  public override string ToString()
  {
    string matrixString = string.Empty;

    for (int rowIndex = 0; rowIndex < Size; rowIndex++)
    {
      for (int columnIndex = 0; columnIndex < Size; columnIndex++)
      {
        matrixString += _data[rowIndex, columnIndex].ToString("F2") + " ";
      }

      matrixString += Environment.NewLine;
    }

    return matrixString;
  }

  public int CompareTo(SquareMatrix otherMatrix) =>
    Determinant().CompareTo(otherMatrix.Determinant());

  public SquareMatrix DeepCopy()
  {
    double[,] copiedData = (double[,])_data.Clone();
    return new SquareMatrix(Size, copiedData);
  }
}
