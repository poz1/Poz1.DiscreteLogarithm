using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Poz1.DiscreteLogarithm.Model
{
	internal class ModuloMultiplicativeGroup : IFiniteGroup<int>, IMultiplicativeGroup<int>, ICyclicGroup<int>
	{
		private readonly List<int> elements = new List<int>();

		private int order = 0;

		private bool lazyLoaded = false;

		public IEnumerable<int> Elements
		{
			get
			{
				if (!lazyLoaded)
				{
					ComputeElements();
				}
				return elements.ToImmutableList();
			}
		}

		public int Identity { get { return 1; } }

		public bool IsAbelian { get { return true; } }

		public bool IsCyclic { get { throw new NotImplementedException(); } }

		public int Modulus { get; }

		public int Order
		{
			get
			{
				if (!lazyLoaded)
				{
					ComputeElements();
				}
				return order;
			}
		}

		public ModuloMultiplicativeGroup(int modulus)
		{
			//Must be prime or it's not a group
			Modulus = modulus;
		}

		private void ComputeElements()
		{
			for (int i = 1; i <= Modulus; i++)
			{
				if (EuclideanGCD(i, Modulus) == 1)
				{
					elements.Add(i);
					order++;
				}
			}
			lazyLoaded = true;
		}

		public int EuclideanGCD(int x, int y)
		{ 
			if ((x < 0) || (y < 0))
				throw new ArgumentOutOfRangeException("x and y cannot be negative");

			int a;
			int b;

			if (x >= y)
			{
				a = x;
				b = y;
			}
			else
			{
				a = y;
				b = x;
			}

			while (b != 0)
			{
				int temp = a % b;
				a = b;
				b = temp;
			}

			return a;
		}

		public int GetGenerator(int b)
		{
			throw new NotImplementedException();
		}

		public int GetGenerators()
		{
			if (!IsCyclic)
			{
				throw new Exception("Group has to be cyclic");
			}
			return 0;
		}

		public int GetInverse(int x)
		{
			if (!lazyLoaded)
				ComputeElements();

			while (x > Modulus)
				x -= Modulus;

			for (int i = 0; i < elements.Count; i++)
			{
				if (Multiply(elements[i], x) == Identity)
				{
					return elements[i];
				}
			}

			return 0;
			//throw new Exception("Modulus must be prime or it's not a group");
		}

		public int Multiply(int x, int y)
		{
			return (x * y) % Modulus;
		}

		public int Pow(int x, int y)
		{
			var result = Identity;
			for(int i = 0; i < y; i++)
			{
				result = Multiply(result, x);
			}
			return result;
		}
	}
}