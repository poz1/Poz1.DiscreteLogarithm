using Poz1.DiscreteLogarithm.Model;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm
{
	public class ExhaustiveSearch : IDiscreteLogarithmAlgorithm<int>
	{
		public ExhaustiveSearch()
		{
		}

		public Task<int> Compute(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			ExhaustiveSearch.<>c__DisplayClass0_0 variable = null;
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			Task.Run(new Action(variable, () => {
				if ((this.@group is ICyclicGroup<int> ? false : !(this.@group is IFiniteGroup<int>)))
				{
					this.task.SetException(new ArgumentException("Group has to be finite and cyclic"));
				}
				int identity = this.@group.Identity;
				int num = 0;
				while (identity != this.beta)
				{
					identity = this.@group.Multiply(identity, this.alpha);
					num++;
				}
				this.task.SetResult(num);
			}));
			return taskCompletionSource.get_Task();
		}
	}
}