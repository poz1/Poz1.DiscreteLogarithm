//using System;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;

//namespace Poz1.DiscreteLogarithm.Model
//{
//	public class Matrix<E>
//	{
//		public int ColumnCount
//		{
//			get
//			{
//				return (Values == null ? 0 : Values.GetLength(1));
//			}
//		}

//		public Field<E> Field
//		{
//			get;
//		}

//		public int RowCount
//		{
//			get
//			{
//				return (Values == null ? 0 : Values.GetLength(0));
//			}
//		}

//		public E[,] Values
//		{
//			get;
//			private set;
//		}

//		public Matrix(int rows, int cols, Field<E> field)
//		{
//			if ((rows <= 0 ? true : cols <= 0))
//			{
//				throw new ArgumentOutOfRangeException("Invalid number of rows or columns");
//			}
//			Field = field;
//			Values = new E[rows, cols];
//		}

//		public void AddRows(int srcRow, int destRow, E factor)
//		{
//			if ((srcRow < 0 || srcRow >= RowCount || destRow < 0 ? true : destRow >= RowCount))
//			{
//				throw new ArgumentOutOfRangeException("Row index out of bounds");
//			}
//			int num = 0;
//			int columnCount = ColumnCount;
//			while (num < columnCount)
//			{
//				Values[destRow, num] = Field.Add(Values[destRow, num], Field.Multiply(Values[srcRow, num], factor));
//				num++;
//			}
//		}

//		public E DeterminantAndRef()
//		{
//			int rowCount = RowCount;
//			int columnCount = ColumnCount;
//			if (rowCount != columnCount)
//			{
//				throw new InvalidOperationException("Matrix dimensions are not square");
//			}
//			E one = Field.One;
//			int num = 0;
//			for (int i = 0; i < columnCount; i++)
//			{
//				int num1 = num;
//				while (true)
//				{
//					if ((num1 >= rowCount ? true : !Field.Equals(Values[num1, i], Field.Zero)))
//					{
//						break;
//					}
//					num1++;
//				}
//				if (num1 < rowCount)
//				{
//					if (num != num1)
//					{
//						SwapRows(num, num1);
//						one = Field.Negate(one);
//					}
//					num1 = num;
//					num++;
//					E values = Values[num1, i];
//					MultiplyRow(num1, Field.Reciprocal(values));
//					one = Field.Multiply(values, one);
//					for (int j = num1 + 1; j < rowCount; j++)
//					{
//						AddRows(num1, j, Field.Negate(Values[j, i]));
//					}
//				}
//				one = Field.Multiply(Values[i, i], one);
//			}
//			return one;
//		}

//		public E Get(int row, int col)
//		{
//			if ((row < 0 || row >= RowCount || col < 0 ? true : col >= ColumnCount))
//			{
//				throw new ArgumentOutOfRangeException("Row or column index out of bounds");
//			}
//			return Values[row, col];
//		}

//		public E[] GetColumn(int columnNumber)
//		{
//			Matrix<E>.<> c__DisplayClass16_0 variable = null;
//			E[] array = Enumerable.ToArray<E>(Enumerable.Select<int, E>(Enumerable.Range(0, RowCount), new Func<int, E>(variable, (int x) => <> 4__Values[x, columnNumber])));
//			return array;
//		}

//		public E[] GetRow(int rowNumber)
//		{
//			Matrix<E>.<> c__DisplayClass17_0 variable = null;
//			E[] array = Enumerable.ToArray<E>(Enumerable.Select<int, E>(Enumerable.Range(0, ColumnCount), new Func<int, E>(variable, (int x) => <> 4__Values[rowNumber, x])));
//			return array;
//		}

//		public void Invert()
//		{
//			int rowCount = RowCount;
//			int columnCount = ColumnCount;
//			if (rowCount != columnCount)
//			{
//				throw new InvalidOperationException("Matrix dimensions are not square");
//			}
//			Matrix<E> matrix = new Matrix<E>(rowCount, columnCount * 2, Field);
//			for (int i = 0; i < rowCount; i++)
//			{
//				for (int j = 0; j < columnCount; j++)
//				{
//					matrix.Set(i, j, Values[i, j]);
//					matrix.Set(i, j + columnCount, (i == j ? Field.One : Field.Zero));
//				}
//			}
//			matrix.ReducedRowEchelonForm();
//			for (int k = 0; k < rowCount; k++)
//			{
//				for (int l = 0; l < columnCount; l++)
//				{
//					if (!Field.Equals(matrix.Values[k, l], (k == l ? Field.One : Field.Zero)))
//					{
//						throw new InvalidOperationException("Matrix is not invertible");
//					}
//				}
//			}
//			for (int m = 0; m < rowCount; m++)
//			{
//				for (int n = 0; n < columnCount; n++)
//				{
//					Set(m, n, matrix.Values[m, n + columnCount]);
//				}
//			}
//		}

//		public Matrix<E> Multiply(Matrix<E> other)
//		{
//			if (ColumnCount != other.RowCount)
//			{
//				throw new ArgumentOutOfRangeException("Incompatible matrix sizes for multiplication");
//			}
//			int rowCount = RowCount;
//			int columnCount = other.ColumnCount;
//			int num = ColumnCount;
//			Matrix<E> matrix = new Matrix<E>(rowCount, columnCount, Field);
//			for (int i = 0; i < rowCount; i++)
//			{
//				for (int j = 0; j < columnCount; j++)
//				{
//					E zero = Field.Zero;
//					for (int k = 0; k < num; k++)
//					{
//						zero = Field.Add(Field.Multiply(Values[i, k], other.Values[k, j]), zero);
//					}
//					matrix.Set(i, j, zero);
//				}
//			}
//			return matrix;
//		}

//		public void MultiplyRow(int row, E factor)
//		{
//			if ((row < 0 ? true : row >= RowCount))
//			{
//				throw new ArgumentOutOfRangeException("Row index out of bounds");
//			}
//			int num = 0;
//			int columnCount = ColumnCount;
//			while (num < columnCount)
//			{
//				Values[row, num] = Field.Multiply(Values[row, num], factor);
//				num++;
//			}
//		}

//		public void ReducedRowEchelonForm()
//		{
//			int rowCount = RowCount;
//			int columnCount = ColumnCount;
//			int num = 0;
//			int num1 = 0;
//			while (true)
//			{
//				if ((num1 >= columnCount ? true : num >= rowCount))
//				{
//					break;
//				}
//				int num2 = num;
//				while (true)
//				{
//					if ((num2 >= rowCount ? true : !Field.Equals(Values[num2, num1], Field.Zero)))
//					{
//						break;
//					}
//					num2++;
//				}
//				if (num2 != rowCount)
//				{
//					SwapRows(num, num2);
//					num2 = num;
//					num++;
//					MultiplyRow(num2, Field.Reciprocal(Values[num2, num1]));
//					for (int i = num2 + 1; i < rowCount; i++)
//					{
//						AddRows(num2, i, Field.Negate(Values[i, num1]));
//					}
//				}
//				num1++;
//			}
//			Console.WriteLine("\n ---- \n");
//			Console.WriteLine(ToString());
//			for (int j = num - 1; j >= 0; j--)
//			{
//				int num3 = 0;
//				while (true)
//				{
//					if ((num3 >= columnCount ? true : !Field.Equals(Values[j, num3], Field.Zero)))
//					{
//						break;
//					}
//					num3++;
//				}
//				if (num3 != columnCount)
//				{
//					for (int k = j - 1; k >= 0; k--)
//					{
//						AddRows(j, k, Field.Negate(Values[k, num3]));
//					}
//				}
//			}
//		}

//		public void Set(int row, int col, E val)
//		{
//			if ((row < 0 || row >= RowCount || col < 0 ? true : col >= ColumnCount))
//			{
//				throw new ArgumentOutOfRangeException("Row or column index out of bounds");
//			}
//			Values[row, col] = val;
//		}

//		public void SetColumn(int columnNumber, E[] newColumn)
//		{
//			for (int i = 0; i < RowCount; i++)
//			{
//				Values[i, columnNumber] = newColumn[i];
//			}
//		}

//		public void SetRow(int rowNumber, E[] newRow)
//		{
//			for (int i = 0; i < ColumnCount; i++)
//			{
//				Values[rowNumber, i] = newRow[i];
//			}
//		}

//		public void SwapRows(int row0, int row1)
//		{
//			if ((row0 < 0 || row0 >= RowCount || row1 < 0 ? true : row1 >= RowCount))
//			{
//				throw new ArgumentOutOfRangeException("Row or column index out of bounds");
//			}
//			E[] row = GetRow(row0);
//			SetRow(row0, GetRow(row1));
//			SetRow(row1, row);
//		}


//		public Matrix<E> Transpose()
//		{
//			int rowCount = RowCount;
//			int columnCount = ColumnCount;
//			Matrix<E> matrix = new Matrix<E>(columnCount, rowCount, Field);
//			for (int i = 0; i < rowCount; i++)
//			{
//				for (int j = 0; j < columnCount; j++)
//				{
//					matrix.Values[j, i] = Values[i, j];
//				}
//			}
//			return matrix;
//		}


//		public override string ToString()
//		{
//			StringBuilder stringBuilder = new StringBuilder();
//			for (int i = 0; i < RowCount; i++)
//			{
//				if (i > 0)
//				{
//					stringBuilder.Append("\n");
//				}
//				stringBuilder.Append("[");
//				for (int j = 0; j < ColumnCount; j++)
//				{
//					if (j > 0)
//					{
//						stringBuilder.Append(" ");
//					}
//					stringBuilder.Append(Values[i, j]);
//				}
//				stringBuilder.Append("]");
//			}
//			return stringBuilder.ToString();
//		}
//	}
//}