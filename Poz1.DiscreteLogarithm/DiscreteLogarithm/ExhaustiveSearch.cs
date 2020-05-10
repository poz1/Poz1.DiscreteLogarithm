using Poz1.DiscreteLogarithm.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm
{
	public class ExhaustiveSearch : DiscreteLogarithmAlgorithm<int>
	{
		public override Task<int> Solve(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			TaskCompletionSource<int> task = new TaskCompletionSource<int>();

			Task.Run(() => 
			{
				if (!(group is ICyclicGroup<int>) && !(group is IFiniteGroup<int>))
				{
					task.SetException(new ArgumentException("Group has to be finite and cyclic"));
				}

				int identity  = group.Identity;
				int num = 0;

				while (identity != beta)
				{
					identity = group.Multiply(identity, alpha);
					num++;
				}
				
				task.SetResult(num);
			});

			return task.Task;
		}
	}
}