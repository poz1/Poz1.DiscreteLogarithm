using System;

namespace Poz1.DiscreteLogarithm.Model
{
	internal interface ICyclicGroup<T> : IMultiplicativeGroup<T>
	{
		bool IsCyclic
		{
			get
			{
				return true;
			}
		}

		T GetGenerator(T b);
	}
}