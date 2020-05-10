using System;
using System.Collections.Generic;

namespace Poz1.DiscreteLogarithm.Model
{
	internal interface IFiniteGroup<T> : IMultiplicativeGroup<T>
	{
		IEnumerable<T> Elements
		{
			get;
		}

		bool IsFinite
		{
			get
			{
				return true;
			}
		}

		int Order
		{
			get;
		}
	}
}