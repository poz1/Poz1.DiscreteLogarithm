using Poz1.DiscreteLogarithm.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm
{
	public class BabyStepGiantStep : IDiscreteLogarithmAlgorithm<int>
	{
		public BabyStepGiantStep()
		{
		}

		public Task<int> Compute(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			BabyStepGiantStep.<>c__DisplayClass0_0 variable = null;
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			Task.Run(new Action(variable, () => {
				if ((this.@group is ICyclicGroup<int> ? false : !(this.@group is IFiniteGroup<int>)))
				{
					this.task.SetException(new ArgumentException("Group has to be finite and cyclic"));
				}
				int num = (int)Math.Ceiling(Math.Sqrt((double)(this.@group as IFiniteGroup<int>).Order));
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				int identity = this.@group.Identity;
				for (int i = 0; i < num; i++)
				{
					if (this.cancellationToken.get_IsCancellationRequested())
					{
						this.task.SetCanceled();
					}
					dictionary.Add(identity, i);
					identity = this.@group.Multiply(identity, this.alpha);
				}
				int inverse = this.@group.GetInverse(this.alpha);
				int identity1 = this.@group.Identity;
				for (int j = 0; j < num; j++)
				{
					identity1 = this.@group.Multiply(identity1, inverse);
				}
				int num1 = this.beta;
				int item = 0;
				int num2 = 0;
				while (num2 < num - 1)
				{
					if (this.cancellationToken.get_IsCancellationRequested())
					{
						this.task.SetCanceled();
					}
					if (!dictionary.ContainsKey(num1))
					{
						num1 = this.@group.Multiply(identity1, num1);
						num2++;
					}
					else
					{
						item = num2 * num + dictionary.get_Item(num1);
						break;
					}
				}
				this.task.SetResult(item);
			}));
			return taskCompletionSource.get_Task();
		}
	}
}