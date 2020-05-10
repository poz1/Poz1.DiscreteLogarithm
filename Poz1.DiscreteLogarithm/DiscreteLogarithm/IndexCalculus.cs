using Poz1.DiscreteLogarithm.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm
{
	internal class IndexCalculus : IDiscreteLogarithmAlgorithm<int>
	{
		private int groupOrder;

		private int alpha;

		public IndexCalculus()
		{
		}

		private int[] CalculateResult(int[][] rows)
		{
			int[] numArray;
			Console.WriteLine("Final vValie");
			this.PrintMatrix(rows);
			int num = 0;
			int length = (int)rows[0].Length;
			int[] numArray1 = new int[(int)rows.Length];
			int length1 = (int)rows.Length - 1;
			while (true)
			{
				if (length1 >= 0)
				{
					num = rows[length1][length - 1];
					for (int i = length - 2; i > length1 - 1; i--)
					{
						num = num - rows[length1][i] * numArray1[i];
					}
					numArray1[length1] = num / rows[length1][length1];
					if (this.IsValidResult((double)numArray1[length1]))
					{
						length1--;
					}
					else
					{
						numArray = null;
						break;
					}
				}
				else
				{
					numArray = numArray1;
					break;
				}
			}
			return numArray;
		}

		public async Task<int> Compute(int alpha, int groupOrder, int beta, CancellationToken cancellationToken)
		{
			IndexCalculus.<>c__DisplayClass2_0 variable = null;
			this.groupOrder = groupOrder;
			this.alpha = alpha;
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			Task.Run(new Action(variable, () => {
				int num = 5;
				int num1 = 1;
				List<int> firstNPrimes = PrimeNumber.GetFirstNPrimes(num);
				int[][] linearEquations = this.<>4__this.GetLinearEquations(num + num1, firstNPrimes);
				Matrix<int> matrix = new Matrix<int>((int)linearEquations.Length, (int)linearEquations[0].Length, new PrimeField(this.groupOrder + 1));
				for (int i = 0; i < matrix.RowCount; i++)
				{
					for (int j = 0; j < matrix.ColumnCount; j++)
					{
						matrix.Set(i, j, linearEquations[i][j]);
					}
				}
				Console.Write(matrix);
				matrix.ReducedRowEchelonForm();
				Console.Write(matrix);
				this.task.SetResult(9);
			}));
			int task = await taskCompletionSource.get_Task();
			variable = null;
			return task;
		}

		public Task<int> Compute(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		private Pair ext_euclid(long a, long b)
		{
			Pair pair;
			if (a < b)
			{
				Pair pair1 = this.ext_euclid(b, a);
				pair = new Pair(pair1.second, pair1.first);
			}
			else if (b != (long)0)
			{
				long num = a / b;
				long num1 = a - b * num;
				Pair pair2 = this.ext_euclid(b, num1);
				Pair pair3 = new Pair(pair2.second, pair2.first - num * pair2.second);
				pair = pair3;
			}
			else
			{
				pair = new Pair((long)1, (long)0);
			}
			return pair;
		}

		private void gauss(int[][] A, int num_columns)
		{
			int length = (int)A.Length;
			int num = (int)A[0].Length;
			for (int i = 0; i < num_columns; i++)
			{
				for (int j = i; j < num_columns; j++)
				{
					if (A[j][i] != 0)
					{
						int[] a = A[i];
						A[i] = A[j];
						A[j] = a;
					}
				}
				int num1 = (int)this.Inverse((long)A[i][i], (long)this.groupOrder);
				for (int k = i; k < num; k++)
				{
					A[i][k] = A[i][k] * num1 % this.groupOrder;
				}
				for (int l = 0; l < length; l++)
				{
					if (l != i)
					{
						int a1 = A[l][i];
						A[l][i] = 0;
						for (int m = i + 1; m < num; m++)
						{
							A[l][m] = (A[l][m] - a1 * A[i][m] + a1 * this.groupOrder) % this.groupOrder;
						}
					}
				}
			}
		}

		private void gauss(int[][] A)
		{
			this.gauss(A, Math.Min((int)A.Length, (int)A[0].Length));
		}

		private long gcd(long a, long b)
		{
			long num;
			if (a < b)
			{
				long num1 = a;
				a = b;
				b = num1;
			}
			num = (b != (long)0 ? this.gcd(b, a % b) : a);
			return num;
		}

		private int[][] GetLinearEquations(int numberOfEquations, List<int> S)
		{
			int[][] count = new int[numberOfEquations][];
			double[] numArray = new double[numberOfEquations];
			Random random = new Random();
			int[] numArray1 = new int[] { 100, 18, 12, 62, 143, 206 };
			int num = 0;
			while (num < numberOfEquations)
			{
				int num1 = numArray1[num];
				BigInteger bigInteger = BigInteger.Pow(this.alpha, num1) % this.groupOrder + 1;
				try
				{
					try
					{
						List<Factor> factors = Number.GetFactors(bigInteger, S);
						Console.WriteLine(string.Concat("Considering N: ", bigInteger.ToString()));
						foreach (Factor factor in factors)
						{
							Console.WriteLine(factor);
						}
						if (factors.get_Count() != 0)
						{
							count[num] = new int[S.get_Count() + 1];
							count[num][S.get_Count()] = num1;
							int num2 = 0;
							for (int i = 0; i < factors.get_Count(); i++)
							{
								Factor item = factors.get_Item(i);
								while (item.Number != S.get_Item(i + num2))
								{
									num2++;
								}
								count[num][i + num2] = item.Count;
							}
						}
					}
					catch (Exception exception)
					{
						Console.WriteLine(string.Concat("Testing ", bigInteger.ToString(), "failed"));
					}
				}
				finally
				{
					num++;
				}
			}
			return count;
		}

		private int GetModulus(int input)
		{
			int num = input % this.groupOrder;
			if (num < 0)
			{
				num = this.groupOrder + input;
			}
			return num;
		}

		private long Inverse(long num, long modulo)
		{
			long num1;
			num %= modulo;
			long num2 = this.ext_euclid(num, modulo).first;
			num1 = (num2 >= (long)0 ? num2 % modulo : (modulo + num2) % modulo);
			return num1;
		}

		private bool IsValidResult(double result)
		{
			return (double.IsNaN(result) ? false : !double.IsInfinity(result));
		}

		private void PrintMatrix(int[][] mat)
		{
			for (int i = 0; i < (int)mat.Length; i++)
			{
				for (int j = 0; j < (int)mat[i].Length; j++)
				{
					Console.Write(string.Concat(mat[i][j].ToString(), " "));
				}
				Console.Write("\n");
			}
		}

		private void PrintMatrix(double[][] mat)
		{
			for (int i = 0; i < (int)mat.Length; i++)
			{
				for (int j = 0; j < (int)mat[i].Length; j++)
				{
					Console.Write(string.Concat(mat[i][j].ToString(), " "));
				}
				Console.Write("\n");
			}
		}

		private int[] SolveLinearEquations(int[][] matrix)
		{
			int[] numArray;
			int length = (int)matrix[0].Length;
			int num = 0;
			while (true)
			{
				if (num >= (int)matrix.Length - 1)
				{
					numArray = this.CalculateResult(matrix);
					break;
				}
				else if ((matrix[num][num] != 0 ? true : this.Swap(matrix, num, num)))
				{
					Console.WriteLine("First Loop");
					this.PrintMatrix(matrix);
					for (int i = num; i < (int)matrix.Length; i++)
					{
						Console.WriteLine(string.Concat("r = ", num.ToString(), " eq = ", i.ToString()));
						int[] modulus = new int[length];
						for (int j = 0; j < length; j++)
						{
							modulus[j] = matrix[i][j];
							if (matrix[i][num] != 0)
							{
								modulus[j] = this.GetModulus(modulus[j] / matrix[i][num]);
							}
						}
						matrix[i] = modulus;
					}
					Console.WriteLine("Second Loop");
					this.PrintMatrix(matrix);
					for (int k = num + 1; k < (int)matrix.Length; k++)
					{
						int[] modulus1 = new int[length];
						for (int l = 0; l < length; l++)
						{
							modulus1[l] = matrix[k][l];
							if (matrix[k][num] != 0)
							{
								modulus1[l] = this.GetModulus(modulus1[l] - matrix[num][l]);
							}
						}
						matrix[k] = modulus1;
					}
					num++;
				}
				else
				{
					numArray = null;
					break;
				}
			}
			return numArray;
		}

		private bool Swap(int[][] rows, int row, int column)
		{
			bool flag = false;
			for (int i = (int)rows.Length - 1; i > row; i--)
			{
				if (rows[i][row] != 0)
				{
					int[] numArray = new int[(int)rows[0].Length];
					numArray = rows[i];
					rows[i] = rows[column];
					rows[column] = numArray;
					flag = true;
				}
			}
			return flag;
		}
	}
}