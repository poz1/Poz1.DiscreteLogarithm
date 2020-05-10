using System;

namespace Poz1.DiscreteLogarithm.Model
{
	public abstract class Field<T>
	{
		public abstract T One
		{
			get;
		}

		public abstract T Zero
		{
			get;
		}

		protected Field()
		{
		}

		public abstract T Add(T x, T y);

		public T Divide(T x, T y)
		{
			return this.Multiply(x, this.Reciprocal(y));
		}

		public abstract bool Equals(T x, T y);

		public abstract T Multiply(T x, T y);

		public abstract T Negate(T x);

		public abstract T Reciprocal(T x);

		public T Subtract(T x, T y)
		{
			return this.Add(x, this.Negate(y));
		}
	}
}