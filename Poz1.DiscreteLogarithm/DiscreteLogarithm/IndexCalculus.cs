using Poz1.DiscreteLogarithm.Model;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm
{
	internal class IndexCalculus : DiscreteLogarithmAlgorithm<int>
	{


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

		//public async Task<int> Compute(int alpha, int groupOrder, int beta, CancellationToken cancellationToken)
		//{
		//	IndexCalculus.<> c__DisplayClass2_0 variable = null;
		//	this.groupOrder = groupOrder;
		//	this.alpha = alpha;
		//	TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
		//	Task.Run(new Action(variable, () =>
		//	{
		//		int num = 5;
		//		int num1 = 1;
		//		List<int> firstNPrimes = PrimeNumber.GetFirstNPrimes(num);
		//		int[][] linearEquations = this.<> 4__this.GetLinearEquations(num + num1, firstNPrimes);
		//		Matrix<int> matrix = new Matrix<int>((int)linearEquations.Length, (int)linearEquations[0].Length, new PrimeField(this.groupOrder + 1));
		//		for (int i = 0; i < matrix.RowCount; i++)
		//		{
		//			for (int j = 0; j < matrix.ColumnCount; j++)
		//			{
		//				matrix.Set(i, j, linearEquations[i][j]);
		//			}
		//		}
		//		Console.Write(matrix);
		//		matrix.ReducedRowEchelonForm();
		//		Console.Write(matrix);
		//		this.task.SetResult(9);
		//	}));
		//	int task = await taskCompletionSource.get_Task();
		//	variable = null;
		//	return task;
		//}

		private IFiniteGroup<int> pgroup;

		public override Task<int> Solve(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			TaskCompletionSource<int> task = new TaskCompletionSource<int>();

			Task.Run(() =>
			{
				if (!(group is ICyclicGroup<int>) && !(group is IFiniteGroup<int>))
				{
					task.SetException(new ArgumentException("Group has to be finite and cyclic"));
					return;
				}

				pgroup = (IFiniteGroup<int>)group;
				int t = 5;
				int c = 1;
				var firstNPrimes = PrimeNumber.GetFirstNPrimes(t);
				var linearEquations = GetLinearEquations(t + c, firstNPrimes, alpha);

				//var hh = SolveLinearEquations(linearEquations, pgroup.Order + 1);

				var wikiTest = new int[,] { { 8,1,6 }, { 3,5,7 }, { 4,9,2} };
				//Gauss(3,4, wikiTest);
				PrintMatrix(linearEquations);

				//RREF(linearEquations, t + c, firstNPrimes.Count + 1);
				//RREF(linearEquations, 3, 3);
				//var bhu = new int[][] { new int[]{ 3,5,14 }, new int[] { 7,3,6 } };

				gauss(linearEquations, pgroup.Order);
				PrintMatrix(linearEquations);
				//Matrix<int> matrix = new Matrix<int>((int)linearEquations.Length, (int)linearEquations[0].Length, new PrimeField(this.groupOrder + 1));
				//for (int i = 0; i < matrix.RowCount; i++)
				//{
				//	for (int j = 0; j < matrix.ColumnCount; j++)
				//	{
				//		matrix.Set(i, j, linearEquations[i][j]);
				//	}
				//}
				//Console.Write(matrix);
				//matrix.ReducedRowEchelonForm();
				//Console.Write(matrix);


				task.SetResult(t);
				return;
			});

			return task.Task;
		}

		

		private int[][] GetLinearEquations(int numberOfEquations, List<int> S, int alpha)
		{
			var count = new int[numberOfEquations][];
			double[] numArray = new double[numberOfEquations];
			Random random = new Random();
			int[] numArray1 = new int[] { 100, 18, 12, 62, 143, 206 };
			int num = 0;
			while (num < numberOfEquations)
			{
				int num1 = numArray1[num];
				BigInteger bigInteger = BigInteger.Pow(alpha, num1) % (this.pgroup.Order + 1);
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
						if (factors.Count != 0)
						{
							count[num] = new int[S.Count + 1];
							count[num][ S.Count] = num1;
							int num2 = 0;
							for (int i = 0; i < factors.Count; i++)
							{
								Factor item = factors[i];
								while (item.Number != S[i + num2])
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


		private void gauss(int[][] A, int num_columns, int prime)
		{
			int n = A.Length;
			int m = A[0].Length;

			for (int i = 0; i < num_columns; i++)
			{
				// Finding row with nonzero element at column i, swap this to row i
				for (int k = i; k < num_columns; k++)
				{
					if (A[k][i] != 0)
					{
						int[] t = A[i];
						A[i] = A[k];
						A[k] = t;
					}
				}
				// Normalize the i-th row.
				int inverse2 = (int)inverse((long)A[i][i], prime);
				for (int k = i; k < m; k++) A[i][k] = (A[i][k] * inverse2) % prime;

				// Combine the i-th row with the following rows.
				for (int j = 0; j < n; j++)
				{
					if (j == i) continue;
					int c = A[j][i];
					A[j][i] = 0;
					for (int k = i + 1; k < m; k++)
					{
						A[j][k] = (A[j][k] - c * A[i][k] + c * prime) % prime;
					}
				}
			}
		}

		public void gauss(int[][] A, int prime)
		{
			gauss(A, Math.Min(A.Length, A[0].Length), prime);
		}

		private long gcd(long a, long b)
		{
			if (a < b)
			{
				long temp = a;
				a = b;
				b = temp;
			}
			if (b == 0) return a;
			return gcd(b, a % b);
		}
		private Pair ext_euclid(long a, long b)
		{
			if (a < b)
			{
				Pair r2 = ext_euclid(b, a);
				return new Pair(r2.second, r2.first);
			}
			if (b == 0) return new Pair(1, 0);
			long q = a / b;
			long rem = a - b * q;
			Pair r = ext_euclid(b, rem);
			Pair ret = new Pair(r.second, r.first - q * r.second);
			return ret;
		}

		private long inverse(long num, long modulo)
		{
			num = num % modulo;
			Pair p = ext_euclid(num, modulo);
			long ret = p.first;
			if (ret < 0) return (modulo + ret) % modulo;
			return ret % modulo;
		}

		 private class Pair
		{
			public long first;
			public long second;
			public Pair(long frst, long scnd)
			{
				first = frst;
				second = scnd;
			}
		}
	}
}