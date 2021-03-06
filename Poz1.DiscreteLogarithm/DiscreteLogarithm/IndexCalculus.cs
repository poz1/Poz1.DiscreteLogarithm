using Poz1.DiscreteLogarithm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

				//var wikiTest = new int[,] { { 8,1,6 }, { 3,5,7 }, { 4,9,2} };
				//Gauss(3,4, wikiTest);
				//PrintMatrix(linearEquations);
				//PrintMatrix(wikiTest);

				LinearEquationSolver test = new LinearEquationSolver((ModuloMultiplicativeGroup)group);


				foreach (var asdf in linearEquations)
				{
					test.AddLinearEquation(asdf);
				}

				var result = test.Solve();

				//RREF(linearEquations, t + c, firstNPrimes.Count + 1);
				//RREF(linearEquations, 3, 3);
				//var bhu = new int[][] { new int[]{ 3,5,14 }, new int[] { 7,3,6 } };

				//gauss(linearEquations, pgroup.Order);
				//PrintMatrix(linearEquations);
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

		

		private decimal[][] GetLinearEquations(int numberOfEquations, List<int> S, int alpha)
		{
			var count = new decimal[numberOfEquations][];
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
							count[num] = new decimal[S.Count + 1];
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
		class LinearEquationSolver
		{
			ModuloMultiplicativeGroup group;
			public LinearEquationSolver(ModuloMultiplicativeGroup group)
            {
				this.group = group;
            }

			List<LinearEquation> rows = new List<LinearEquation>();
			decimal[] solution;

			public void AddLinearEquation(decimal result, params decimal[] coefficients)
			{
				rows.Add(new LinearEquation(result, coefficients));
			}

			public void AddLinearEquation(params decimal[] coefficients)
			{
				rows.Add(new LinearEquation(coefficients));
			}

			public IList<decimal> Solve()       //Returns a list of coefficients for the variables in the same order they were entered
			{
				solution = new decimal[rows[0].Coefficients.Count()];

				for (int pivotM = 0; pivotM < rows.Count() - 1; pivotM++)
				{
					int pivotN = rows[pivotM].IndexOfFirstNonZero;

					for (int i = pivotN + 1; i < rows.Count(); i++)
					{
						LinearEquation rowToReduce = rows[i];
						decimal pivotFactor = rowToReduce[pivotN] / -rows[pivotM][pivotN];
						
						//decimal pivotFactor = group.Divide((int)rowToReduce[pivotN], (int)-rows[pivotM][pivotN]);
						rowToReduce.AddCoefficients(rows[pivotM], pivotFactor);
					}
				}

				while (rows.Any(r => r.Result != 0))
				{
					LinearEquation row = rows.FirstOrDefault(r => r.NonZeroCount == 1);
					if (row == null)
					{
						break;
					}

					int solvedIndex = row.IndexOfFirstNonZero;
					decimal newSolution = row.Result / row[solvedIndex];
					//decimal newSolution = group.Divide((int)row.Result , (int)row[solvedIndex]);

					AddToSolution(solvedIndex, newSolution);
				}

				return solution;
			}

			private void AddToSolution(int index, decimal value)
			{
				foreach (LinearEquation row in rows)
				{
					decimal coefficient = row[index];
					row[index] -= coefficient;
					row.Result -= coefficient * value;
				}

				solution[index] = value;
			}

			private class LinearEquation
			{
				public decimal[] Coefficients;
				public decimal Result;

				public LinearEquation(decimal result, params decimal[] coefficients)
				{
					this.Coefficients = coefficients;
					this.Result = result;
				}

				public LinearEquation(params decimal[] coefficients)
				{
					this.Coefficients = coefficients[0..(coefficients.Length -1)];
					this.Result = coefficients[coefficients.Length - 1];
				}

				public decimal this[int i]
				{
					get { return Coefficients[i]; }
					set { Coefficients[i] = value; }
				}

				public void AddCoefficients(LinearEquation pivotEquation, decimal factor)
				{
					for (int i = 0; i < this.Coefficients.Count(); i++)
					{
						this[i] += pivotEquation[i] * factor;
						if (Math.Abs(this[i]) < 0.000000001M)    //Because sometimes rounding errors mean it's not quite zero, and it needs to be
						{
							this[i] = 0;
						}
					}

					this.Result += pivotEquation.Result * factor;
				}

				public int IndexOfFirstNonZero
				{
					get
					{
						for (int i = 0; i < Coefficients.Count(); i++)
						{
							if (this[i] != 0) return i;
						}
						return -1;
					}
				}

				public int NonZeroCount
				{
					get
					{
						int count = 0;
						for (int i = 0; i < Coefficients.Count(); i++)
						{
							if (this[i] != 0) count++;
						}
						return count;
					}
				}
			}
		}
	}
}