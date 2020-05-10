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
	public class PohligHellman : IDiscreteLogarithmAlgorithm<int>
	{
		public PohligHellman()
		{
		}

		public Task<int> Compute(int a, int n, int b, CancellationToken cancellationToken)
		{
			PohligHellman.<>c__DisplayClass0_0 variable = null;
			Task<int> task = null;
			ExhaustiveSearch exhaustiveSearch = new ExhaustiveSearch();
			task = Task.Run<int>(new Func<Task<int>>(variable, async () => {
				List<Factor> primeFactors = PrimeNumber.GetPrimeFactors(this.n);
				foreach (Factor primeFactor in primeFactors)
				{
					if (this.cancellationToken.get_IsCancellationRequested())
					{
						throw new TaskCanceledException(this.task);
					}
					int num = 1;
					List<int> list = new List<int>();
					BigInteger bigInteger = BigInteger.Pow(this.a, (int)((double)this.n / Math.Pow((double)primeFactor.Number, 1))) % this.n + 1;
					for (int i = 1; i <= primeFactor.Count; i++)
					{
						int num1 = (int)((double)this.n / Math.Pow((double)primeFactor.Number, (double)i));
						BigInteger bigInteger1 = BigInteger.Pow(this.b / num, num1) % this.n + 1;
						int item = list.get_Item(list.get_Count() - 1);
						int num2 = (int)Math.Pow((double)primeFactor.Number, (double)(i - 1));
						BigInteger bigInteger2 = BigInteger.Pow(this.a, item * num2);
						num = (int)((num * bigInteger2) % this.n + 1);
						bigInteger1 = new BigInteger();
						bigInteger2 = new BigInteger();
					}
					list = null;
					bigInteger = new BigInteger();
				}
				int num3 = 0;
				primeFactors = null;
				return num3;
			}));
			return task;
		}

		public Task<int> Compute(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}