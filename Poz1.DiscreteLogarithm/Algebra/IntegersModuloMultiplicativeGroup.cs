using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Poz1.DiscreteLogarithm.Model
{
	internal class IntegersModuloMultiplicativeGroup : IFiniteGroup<int>, IMultiplicativeGroup<int>, ICyclicGroup<int>
	{
		private readonly List<int> elements = new List<int>();

		private int order = 0;

		private bool lazyLoaded = false;

		public IEnumerable<int> Elements
		{
			get
			{
				if (!this.lazyLoaded)
				{
					this.ComputeElements();
				}
				return elements.ToImmutableList<int>();
			}
		}

		public int Identity
		{
			get
			{
				return 1;
			}
		}

		public bool IsAbelian
		{
			get
			{
				return true;
			}
		}

		public bool IsCyclic
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int Modulus
		{
			get;
		}

		public int Order
		{
			get
			{
				if (!this.lazyLoaded)
				{
					this.ComputeElements();
				}
				return this.order;
			}
		}

		public IntegersModuloMultiplicativeGroup(int modulus)
		{
			//MUst be prime or it's not a group
			this.Modulus = modulus;
		}

		private void ComputeElements()
		{
			for (int i = 1; i <= this.Modulus; i++)
			{
				if (this.EuclideanGCD(i, this.Modulus) == 1)
				{
					this.elements.Add(i);
					this.order++;
				}
			}
			this.lazyLoaded = true;
		}

		private int EuclideanGCD(int x, int y)
		{
			int num;
			int num1;
			if ((x < 0 ? true : y < 0))
			{
				throw new ArgumentOutOfRangeException("x and y cannot be negative");
			}
			if (x >= y)
			{
				num = x;
				num1 = y;
			}
			else
			{
				num = y;
				num1 = x;
			}
			while (num1 != 0)
			{
				int num2 = num % num1;
				num = num1;
				num1 = num2;
			}
			return num;
		}

		public int GetGenerator(int b)
		{
			throw new NotImplementedException();
		}

		public int GetGenerators()
		{
			if (!this.IsCyclic)
			{
				throw new Exception("Group has to be cyclic");
			}
			return 0;
		}

		public int GetInverse(int x)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				if (Multiply(elements[i], x) == Identity)
				{
					return elements[i];
				}
			}
			throw new Exception("boh");
			//var num = x;
			//int modulus = this.Modulus;
			//if (num == 0)
			//{
			//	throw new ArithmeticException("Division by zero");
			//}
			//int num1 = 0;
			//int num2 = 1;
			//while (num != 0)
			//{
			//	int num3 = modulus % num;
			//	int num4 = num1 - modulus / num * num2;
			//	modulus = num;
			//	num = num3;
			//	num1 = num2;
			//	num2 = num4;
			//}
			//if (modulus != 1)
			//{
			//	throw new ArgumentOutOfRangeException("Field modulus is not prime");
			//}
			//int modulus1 = (int)(((long)num1 + (long)this.Modulus) % (long)this.Modulus);
			//return modulus1;
		}

		public int Multiply(int x, int y)
		{
			return (x * y) % Modulus;
		}
	}
}