using Poz1.DiscreteLogarithm.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm
{
	public class BabyStepGiantStep : DiscreteLogarithmAlgorithm<int>
	{
		public override Task<int> Solve(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			TaskCompletionSource<int> task = new TaskCompletionSource<int>();

			Task.Run(() => 
			{
				if (!(group is ICyclicGroup<int>) && !(group is IFiniteGroup<int>))
					task.SetException(new ArgumentException("Group has to be finite and cyclic"));
				

				int m = (int)Math.Ceiling(Math.Sqrt((group as IFiniteGroup<int>).Order));
				var table = new Dictionary<int, int>();

				int alphaJ = group.Identity;
				for (int j = 0; j < m; j++)
				{
					if (cancellationToken.IsCancellationRequested)
						task.SetCanceled();
					
					table.Add(alphaJ, j);
					alphaJ = group.Multiply(alphaJ, alpha);
				}

				int inv = group.GetInverse(alpha);
				int am = group.Identity;

				for (int j = 0; j < m; j++)
				{
					am = group.Multiply(am, inv);
				}

				int gamma = beta;

				for(int i = 0; i < m; i++)
				{
					if (cancellationToken.IsCancellationRequested)
						task.SetCanceled();

					if (table.ContainsKey(gamma))
						task.SetResult(group.Multiply(i, m) + table[gamma]);

					gamma = group.Multiply(am, gamma);
				}
			});

			return task.Task;
		}
	}
}