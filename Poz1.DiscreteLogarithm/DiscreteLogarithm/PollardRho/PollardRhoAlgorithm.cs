using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Poz1.DiscreteLogarithm.Model;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho
{
	public class PollardRhoAlgorithm : DiscreteLogarithmAlgorithm<int>
	{
		private readonly Func<int, PollardRhoPartition<int>> partitionFunction;

		public PollardRhoAlgorithm() : this(x => 
		{
			var s1 = new PollardRhoS1<int>();
			var s2 = new PollardRhoS2<int>();
			var s3 = new PollardRhoS3<int>();

			return (x % 3) switch
			{
				1 => s1,
				2 => s3,
				_ => s2,
			};
		}) { }
		
		public PollardRhoAlgorithm(Func<int, PollardRhoPartition<int>> partitionFunction)
		{
			this.partitionFunction = partitionFunction;
		}

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

				var finiteGroup = (IFiniteGroup<int>)group;
				var table = new Dictionary<int, PollardRhoTriad<int>>();
				var triad = new PollardRhoTriad<int>(1,0,0);

				for(int i = 1; ;i++)
				{
					var partition = partitionFunction(triad.X);

					triad = partition.GetNextTriad(finiteGroup, alpha, beta, triad.X, triad.A, triad.B);
					Console.WriteLine("i: " + i + " X: " + triad.X + " A: " + triad.A + " B: " + triad.B);

					table.Add(i, triad);

					if (i % 2 != 0)
						continue;

					var halfIndex = i / 2;
					if (halfIndex > 0 && table[halfIndex].X == triad.X)
					{
						var r = Modulus(table[halfIndex].B - triad.B, finiteGroup.Order);
						if (r == 0)
						{
							task.SetException(new Exception("Terminate the algorithm with failure"));
							return;
						}
						else
						{
							var x = Modulus(group.Multiply(group.GetInverse(r), triad.A - table[halfIndex].A), finiteGroup.Order);
							task.SetResult(x);
							return;
						}
					}
				}
			});

			return task.Task;
		}

		private int Modulus(int n, int mod)
		{
			if (n < 0)
				n = mod + n;

			return n % mod;
		}
	}
}