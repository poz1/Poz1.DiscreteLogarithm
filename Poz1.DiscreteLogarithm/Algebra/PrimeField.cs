using System;
using System.Runtime.CompilerServices;

namespace Poz1.DiscreteLogarithm.Model
{
	public class PrimeField : Field<int>
	{
		public int Modulus
		{
			get;
		}

		public override int One
		{
			get
			{
				return 1;
			}
		}

		public override int Zero
		{
			get
			{
				return 0;
			}
		}

		public PrimeField(int mod)
		{
			if (!PrimeNumber.IsPrime(mod))
			{
				throw new ArgumentOutOfRangeException("Modulus must be prime");
			}
			this.Modulus = mod;
		}

		public override int Add(int x, int y)
		{
			int num = (int)(((long)this.Check(x) + (long)this.Check(y)) % (long)this.Modulus);
			return num;
		}

		private int Check(int x)
		{
			if ((x < 0 ? true : x >= this.Modulus))
			{
				throw new ArgumentOutOfRangeException(string.Concat("Not an element of this field: ", x.ToString()));
			}
			return x;
		}

		public override bool Equals(int x, int y)
		{
			bool flag = this.Check(x) == this.Check(y);
			return flag;
		}

		public override int Multiply(int x, int y)
		{
			int num = (int)((long)this.Check(x) * (long)this.Check(y) % (long)this.Modulus);
			return num;
		}

		public override int Negate(int x)
		{
			int modulus = (this.Modulus - this.Check(x)) % this.Modulus;
			return modulus;
		}

		public override int Reciprocal(int num)
		{
			int modulus = this.Modulus;
			if (num == 0)
			{
				throw new ArithmeticException("Division by zero");
			}
			int num1 = 0;
			int num2 = 1;
			while (num != 0)
			{
				int num3 = modulus % num;
				int num4 = num1 - modulus / num * num2;
				modulus = num;
				num = num3;
				num1 = num2;
				num2 = num4;
			}
			if (modulus != 1)
			{
				throw new ArgumentOutOfRangeException("Field modulus is not prime");
			}
			int modulus1 = (int)(((long)num1 + (long)this.Modulus) % (long)this.Modulus);
			return modulus1;
		}
	}
}