using System;
using System.Collections.Generic;

namespace Poz1.DiscreteLogarithm.Model
{
	public static class PrimeNumber
	{
		public static List<int> GetFirstNPrimes(int n)
		{
			List<int> list = new List<int>(n);
			int num = 2;
			while (list.Count < n)
			{
				bool flag = true;
				int num1 = 2;
				while (num1 <= num / 2)
				{
					if (num % num1 != 0)
					{
						num1++;
					}
					else
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(num);
				}
				num++;
			}
			return list;
		}

		public static List<Factor> GetPrimeFactors(int number)
		{
			List<Factor> list = new List<Factor>();
			for (int i = 2; Math.Pow((double)i, 2) <= (double)number; i++)
			{
				if (number % i == 0)
				{
					Factor factor = new Factor(i);
					while (number % i == 0)
					{
						number /= i;
						factor.IncreaseCounter();
					}
					list.Add(factor);
				}
			}
			if (number > 1)
			{
				Factor factor1 = new Factor(number);
				factor1.IncreaseCounter();
				list.Add(factor1);
			}
			return list;
		}

		public static bool IsPrime(int n)
		{
			bool flag;
			if (n != 1)
			{
				int num = 2;
				while (num * num <= n)
				{
					if (n % num != 0)
					{
						num++;
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}
	}
}