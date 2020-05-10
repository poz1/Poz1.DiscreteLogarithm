using Poz1.DiscreteLogarithm.Model;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho
{
	public class PollardRhoAlgorithm : DiscreteLogarithmAlgorithm<int>
	{
		private readonly Func<int, PollardRhoPartitionID> partitionFunction;
		public PollardRhoAlgorithm() : this(x => (PollardRhoPartitionID)(x % 3)) { }
		public PollardRhoAlgorithm(Func<int, PollardRhoPartitionID> partitionFunction)
		{
			this.partitionFunction = partitionFunction;
		}

	

		public override Task<int> Solve(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
		{
			TaskCompletionSource<int> task = new TaskCompletionSource<int>();

			Task.Run(() =>
			{
				if (!(group is ICyclicGroup<int>) && !(group is IFiniteGroup<int>))
					task.SetException(new ArgumentException("Group has to be finite and cyclic"));

				var x = 0;
				var a = 0;
				var b = 0;


			});

			return task.Task;
		}
	}

	//private class PollardTriad
	//{
	//	public int A
	//	{
	//		get;
	//		set;
	//	}

	//	public int B
	//	{
	//		get;
	//		set;
	//	}

	//	public int X
	//	{
	//		get;
	//		set;
	//	}

	//	public PollardTriad()
	//	{
	//	}
	//}
	//private int n;

	//private IMultiplicativeGroup<int> group;

	// Task<int> Compute(int alpha, int n, int beta, CancellationToken cancellationToken)
	////{
	////	PollardRho.<>c__DisplayClass2_0 variable = null;
	////	Task<int> task = null;
	////	task = Task.Run<int>(new Func<int>(variable, () => {
	////		this.<>4__this.n = this.n;
	////		int num = 0;
	////		PollardRho.PollardTriad pollardTriad = new PollardRho.PollardTriad()
	////		{
	////			X = 1,
	////			A = 0,
	////			B = 0
	////		};
	////		PollardRho.PollardTriad nextTriad = new PollardRho.PollardTriad()
	////		{
	////			X = 1,
	////			A = 0,
	////			B = 0
	////		};
	////		for (int i = 1; i < this.n; i++)
	////		{
	////			if (this.cancellationToken.get_IsCancellationRequested())
	////			{
	////				throw new TaskCanceledException(this.task);
	////			}
	////			pollardTriad = this.<>4__this.GetNextTriad(pollardTriad);
	////			nextTriad = this.<>4__this.GetNextTriad(nextTriad);
	////			nextTriad = this.<>4__this.GetNextTriad(nextTriad);
	////			if (pollardTriad.X == nextTriad.X)
	////			{
	////				int b = (pollardTriad.B - nextTriad.B) % this.n;
	////				if (b == 0)
	////				{
	////					throw new Exception("Algorithm Failure!");
	////				}
	////				num = (int)(Math.Pow((double)b, -1) * (double)(nextTriad.A - pollardTriad.A)) % this.n;
	////			}
	////		}
	////		return num;
	////	}));
	////	return task;
	////}

	////public Task<int> Compute(IMultiplicativeGroup<int> group, int alpha, int beta, CancellationToken cancellationToken)
	////{
	////	PollardRho.<>c__DisplayClass3_0 variable = null;
	////	TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
	////	IFiniteGroup<int> finiteGroup = group as IFiniteGroup<int>;
	////	this.@group = group;
	////	this.n = finiteGroup.Order;
	////	Task.Run(new Action(variable, () => {
	////		int num = 0;
	////		PollardRho.PollardTriad pollardTriad = new PollardRho.PollardTriad()
	////		{
	////			X = 1,
	////			A = 0,
	////			B = 0
	////		};
	////		PollardRho.PollardTriad nextTriad = new PollardRho.PollardTriad()
	////		{
	////			X = 1,
	////			A = 0,
	////			B = 0
	////		};
	////		for (int i = 1; i < this.<>4__this.n; i++)
	////		{
	////			if (this.cancellationToken.get_IsCancellationRequested())
	////			{
	////				this.task.SetCanceled();
	////			}
	////			pollardTriad = this.<>4__this.GetNextTriad(pollardTriad);
	////			nextTriad = this.<>4__this.GetNextTriad(nextTriad);
	////			nextTriad = this.<>4__this.GetNextTriad(nextTriad);
	////			if (pollardTriad.X == nextTriad.X)
	////			{
	////				int b = (pollardTriad.B - nextTriad.B) % this.<>4__this.n;
	////				if (b == 0)
	////				{
	////					throw new Exception("Algorithm Failure!");
	////				}
	////				num = (int)(Math.Pow((double)b, -1) * (double)(nextTriad.A - pollardTriad.A)) % this.<>4__this.n;
	////			}
	////		}
	////		this.task.SetResult(num);
	////	}));
	////	return taskCompletionSource.get_Task();
	////}

	//private PollardRhoAlgorithm.PollardTriad GetNextTriad(PollardRhoAlgorithm.PollardTriad triad)
	//{
	//	PollardRhoAlgorithm.PollardTriad pollardTriad;
	//	switch (triad.X % 3)
	//	{
	//		case 0:
	//			{
	//				pollardTriad = new PollardRhoAlgorithm.PollardTriad()
	//				{
	//					X = (int)Math.Pow((double)triad.X, 2),
	//					A = this.@group.Multiply(triad.A, 2),
	//					B = this.@group.Multiply(triad.B, 2)
	//				};
	//				break;
	//			}
	//		case 1:
	//			{
	//				pollardTriad = new PollardRhoAlgorithm.PollardTriad()
	//				{
	//					X = (int)Math.Pow((double)triad.X, 2),
	//					A = this.@group.Multiply(triad.A, 2),
	//					B = this.@group.Multiply(triad.B, 2)
	//				};
	//				break;
	//			}
	//		case 2:
	//			{
	//				pollardTriad = new PollardRhoAlgorithm.PollardTriad()
	//				{
	//					X = (int)Math.Pow((double)triad.X, 2),
	//					A = this.@group.Multiply(triad.A, 2),
	//					B = this.@group.Multiply(triad.B, 2)
	//				};
	//				break;
	//			}
	//		default:
	//			{
	//				pollardTriad = null;
	//				break;
	//			}
	//	}
	//	return pollardTriad;
	//}

}