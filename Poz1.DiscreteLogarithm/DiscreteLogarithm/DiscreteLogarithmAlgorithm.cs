using Poz1.DiscreteLogarithm.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm
{
	public abstract class DiscreteLogarithmAlgorithm<T>
	{
		Task<T> Compute(IMultiplicativeGroup<T> group, T alpha, T beta, CancellationToken cancellationToken);
	}
}