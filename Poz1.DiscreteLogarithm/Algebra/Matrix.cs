using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Poz1.DiscreteLogarithm.Model
{
	public class Matrix<E>
	{
		public int ColumnCount
		{
			get
			{
				return (this.Values == null ? 0 : this.Values.GetLength(1));
			}
		}

		public Field<E> Field
		{
			get;
		}

		public int RowCount
		{
			get
			{
				return (this.Values == null ? 0 : this.Values.GetLength(0));
			}
		}

		public E[,] Values
		{
			get;
			private set;
		}

		public Matrix(int rows, int cols, Field<E> field)
		{
			if ((rows <= 0 ? true : cols <= 0))
			{
				throw new ArgumentOutOfRangeException("Invalid number of rows or columns");
			}
			this.<Field>k__BackingField = field;
			this.Values = new E[rows, cols];
		}

		public void AddRows(int srcRow, int destRow, E factor)
		{
			if ((srcRow < 0 || srcRow >= this.RowCount || destRow < 0 ? true : destRow >= this.RowCount))
			{
				throw new ArgumentOutOfRangeException("Row index out of bounds");
			}
			int num = 0;
			int columnCount = this.ColumnCount;
			while (num < columnCount)
			{
				this.Values[destRow, num] = this.Field.Add(this.Values[destRow, num], this.Field.Multiply(this.Values[srcRow, num], factor));
				num++;
			}
		}

		public E DeterminantAndRef()
		{
			int rowCount = this.RowCount;
			int columnCount = this.ColumnCount;
			if (rowCount != columnCount)
			{
				throw new InvalidOperationException("Matrix dimensions are not square");
			}
			E one = this.Field.One;
			int num = 0;
			for (int i = 0; i < columnCount; i++)
			{
				int num1 = num;
				while (true)
				{
					if ((num1 >= rowCount ? true : !this.Field.Equals(this.Values[num1, i], this.Field.Zero)))
					{
						break;
					}
					num1++;
				}
				if (num1 < rowCount)
				{
					if (num != num1)
					{
						this.SwapRows(num, num1);
						one = this.Field.Negate(one);
					}
					num1 = num;
					num++;
					E values = this.Values[num1, i];
					this.MultiplyRow(num1, this.Field.Reciprocal(values));
					one = this.Field.Multiply(values, one);
					for (int j = num1 + 1; j < rowCount; j++)
					{
						this.AddRows(num1, j, this.Field.Negate(this.Values[j, i]));
					}
				}
				one = this.Field.Multiply(this.Values[i, i], one);
			}
			return one;
		}

		public E Get(int row, int col)
		{
			if ((row < 0 || row >= this.RowCount || col < 0 ? true : col >= this.ColumnCount))
			{
				throw new ArgumentOutOfRangeException("Row or column index out of bounds");
			}
			return this.Values[row, col];
		}

		public E[] GetColumn(int columnNumber)
		{
			Matrix<E>.<>c__DisplayClass16_0 variable = null;
			E[] array = Enumerable.ToArray<E>(Enumerable.Select<int, E>(Enumerable.Range(0, this.RowCount), new Func<int, E>(variable, (int x) => this.<>4__this.Values[x, this.columnNumber])));
			return array;
		}

		public E[] GetRow(int rowNumber)
		{
			Matrix<E>.<>c__DisplayClass17_0 variable = null;
			E[] array = Enumerable.ToArray<E>(Enumerable.Select<int, E>(Enumerable.Range(0, this.ColumnCount), new Func<int, E>(variable, (int x) => this.<>4__this.Values[this.rowNumber, x])));
			return array;
		}

		public void Invert()
		{
			int rowCount = this.RowCount;
			int columnCount = this.ColumnCount;
			if (rowCount != columnCount)
			{
				throw new InvalidOperationException("Matrix dimensions are not square");
			}
			Matrix<E> matrix = new Matrix<E>(rowCount, columnCount * 2, this.Field);
			for (int i = 0; i < rowCount; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					matrix.Set(i, j, this.Values[i, j]);
					matrix.Set(i, j + columnCount, (i == j ? this.Field.One : this.Field.Zero));
				}
			}
			matrix.ReducedRowEchelonForm();
			for (int k = 0; k < rowCount; k++)
			{
				for (int l = 0; l < columnCount; l++)
				{
					if (!this.Field.Equals(matrix.Values[k, l], (k == l ? this.Field.One : this.Field.Zero)))
					{
						throw new InvalidOperationException("Matrix is not invertible");
					}
				}
			}
			for (int m = 0; m < rowCount; m++)
			{
				for (int n = 0; n < columnCount; n++)
				{
					this.Set(m, n, matrix.Values[m, n + columnCount]);
				}
			}
		}

		public Matrix<E> Multiply(Matrix<E> other)
		{
			if (this.ColumnCount != other.RowCount)
			{
				throw new ArgumentOutOfRangeException("Incompatible matrix sizes for multiplication");
			}
			int rowCount = this.RowCount;
			int columnCount = other.ColumnCount;
			int num = this.ColumnCount;
			Matrix<E> matrix = new Matrix<E>(rowCount, columnCount, this.Field);
			for (int i = 0; i < rowCount; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					E zero = this.Field.Zero;
					for (int k = 0; k < num; k++)
					{
						zero = this.Field.Add(this.Field.Multiply(this.Values[i, k], other.Values[k, j]), zero);
					}
					matrix.Set(i, j, zero);
				}
			}
			return matrix;
		}

		public void MultiplyRow(int row, E factor)
		{
			if ((row < 0 ? true : row >= this.RowCount))
			{
				throw new ArgumentOutOfRangeException("Row index out of bounds");
			}
			int num = 0;
			int columnCount = this.ColumnCount;
			while (num < columnCount)
			{
				this.Values[row, num] = this.Field.Multiply(this.Values[row, num], factor);
				num++;
			}
		}

		public void ReducedRowEchelonForm()
		{
			int rowCount = this.RowCount;
			int columnCount = this.ColumnCount;
			int num = 0;
			int num1 = 0;
			while (true)
			{
				if ((num1 >= columnCount ? true : num >= rowCount))
				{
					break;
				}
				int num2 = num;
				while (true)
				{
					if ((num2 >= rowCount ? true : !this.Field.Equals(this.Values[num2, num1], this.Field.Zero)))
					{
						break;
					}
					num2++;
				}
				if (num2 != rowCount)
				{
					this.SwapRows(num, num2);
					num2 = num;
					num++;
					this.MultiplyRow(num2, this.Field.Reciprocal(this.Values[num2, num1]));
					for (int i = num2 + 1; i < rowCount; i++)
					{
						this.AddRows(num2, i, this.Field.Negate(this.Values[i, num1]));
					}
				}
				num1++;
			}
			Console.WriteLine("\n ---- \n");
			Console.WriteLine(this.ToString());
			for (int j = num - 1; j >= 0; j--)
			{
				int num3 = 0;
				while (true)
				{
					if ((num3 >= columnCount ? true : !this.Field.Equals(this.Values[j, num3], this.Field.Zero)))
					{
						break;
					}
					num3++;
				}
				if (num3 != columnCount)
				{
					for (int k = j - 1; k >= 0; k--)
					{
						this.AddRows(j, k, this.Field.Negate(this.Values[k, num3]));
					}
				}
			}
		}

		public void Set(int row, int col, E val)
		{
			if ((row < 0 || row >= this.RowCount || col < 0 ? true : col >= this.ColumnCount))
			{
				throw new ArgumentOutOfRangeException("Row or column index out of bounds");
			}
			this.Values[row, col] = val;
		}

		public void SetColumn(int columnNumber, E[] newColumn)
		{
			for (int i = 0; i < this.RowCount; i++)
			{
				this.Values[i, columnNumber] = newColumn[i];
			}
		}

		public void SetRow(int rowNumber, E[] newRow)
		{
			for (int i = 0; i < this.ColumnCount; i++)
			{
				this.Values[rowNumber, i] = newRow[i];
			}
		}

		public void SwapRows(int row0, int row1)
		{
			if ((row0 < 0 || row0 >= this.RowCount || row1 < 0 ? true : row1 >= this.RowCount))
			{
				throw new ArgumentOutOfRangeException("Row or column index out of bounds");
			}
			E[] row = this.GetRow(row0);
			this.SetRow(row0, this.GetRow(row1));
			this.SetRow(row1, row);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.RowCount; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append("\n");
				}
				stringBuilder.Append("[");
				for (int j = 0; j < this.ColumnCount; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(this.Values[i, j]);
				}
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		public Matrix<E> Transpose()
		{
			int rowCount = this.RowCount;
			int columnCount = this.ColumnCount;
			Matrix<E> matrix = new Matrix<E>(columnCount, rowCount, this.Field);
			for (int i = 0; i < rowCount; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					matrix.Values[j, i] = this.Values[i, j];
				}
			}
			return matrix;
		}
	}
}