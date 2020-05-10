using System;
using System.Collections.Generic;
using System.Numerics;

namespace Poz1.DiscreteLogarithm.Model
{
	public static class Number
	{
		public static List<Factor> GetFactors(BigInteger number, List<int> divisors)
		{
			List<Factor> list = new List<Factor>();
			foreach (int divisor in divisors)
			{
				if ((number % divisor) == (long)0)
				{
					Factor factor = new Factor(divisor);
					while ((number % divisor) == (long)0)
					{
						number /= divisor;
						factor.IncreaseCounter();
					}
					list.Add(factor);
				}
			}
			if (number > (long)1)
			{
				throw new Exception("we need more divisors!");
			}
			return list;
		}
	}
}