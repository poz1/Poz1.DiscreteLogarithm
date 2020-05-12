using Poz1.DiscreteLogarithm.Algebra;
using Poz1.DiscreteLogarithm.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm
{
	public class PohligHellman : DiscreteLogarithmAlgorithm<int>
	{
		private readonly DiscreteLogarithmAlgorithm<int> discreteLogarithmAlgorithm;
		public PohligHellman(DiscreteLogarithmAlgorithm<int> discreteLogarithmAlgorithm)
		{
			this.discreteLogarithmAlgorithm = discreteLogarithmAlgorithm;
		}

		public PohligHellman() : this(new ExhaustiveSearchAlgorithm())
		{ }

		public override Task<int> Solve(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			TaskCompletionSource<int> task = new TaskCompletionSource<int>();

			Task.Run(async () =>
			{
				if (!(group is ICyclicGroup<int>) && !(group is IFiniteGroup<int>))
				{
					task.SetException(new ArgumentException("Group has to be finite and cyclic"));
					return;
				}

				var finiteGroup = (IFiniteGroup<int>)group;

				List<Factor> primeFactors = PrimeNumber.GetPrimeFactors(finiteGroup.Order);
				List<Congruence<int>> congruences = new List<Congruence<int>>();
				var solutionModulo = 1;

				foreach (Factor factor in primeFactors)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						task.SetCanceled();
						return;
					}

					var q = factor.Number;
					var e = factor.Count;

					var gamma = 1;
					var l = 0;
					var x = 0;

					var alphaS = group.Pow(alpha, finiteGroup.Order / q);

					for(int i = 0; i < e; i++)
					{
						gamma *= group.Pow(alpha, l * group.Pow(q, i - 1));
						var betaS = group.Pow( beta * group.GetInverse(gamma), finiteGroup.Order/ group.Pow(q, i +1));

						l = await discreteLogarithmAlgorithm.Solve(group, alphaS, betaS, cancellationToken);
						x += l * group.Pow(q, i);
					}

					var congruence = new Congruence<int>(x, (int)Math.Pow(factor.Number , factor.Count));
					solutionModulo *= congruence.Modulus;
					congruences.Add(congruence);
				}

				var result = SolveCongruences( congruences, solutionModulo);
				task.SetResult(result);
			});

			return task.Task;
		}

		//Gauss's Algorithm (2.121)
		private int SolveCongruences(List<Congruence<int>> congruences, int solutionModulo)
		{
			var res = 0;
			foreach(var congruence in congruences)
			{
				var congruenceGroup = new ModuloMultiplicativeGroup(congruence.Modulus);

				var n = solutionModulo / congruence.Modulus;
				var m = congruenceGroup.GetInverse(n);
				res += (congruence.Value * n * m) % solutionModulo;
			}

			return res;
		}
	}
}