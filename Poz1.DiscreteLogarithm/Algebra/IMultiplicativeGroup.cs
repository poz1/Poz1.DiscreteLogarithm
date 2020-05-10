using System;

namespace Poz1.DiscreteLogarithm.Model
{
	public interface IMultiplicativeGroup<T>
	{
		T Identity { get; }

		bool IsAbelian { get; }

		T GetInverse(T x);

		T Multiply(T x, T y);

		T Pow(T x, T y);
	}
}