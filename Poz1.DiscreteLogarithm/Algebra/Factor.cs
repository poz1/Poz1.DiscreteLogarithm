using System;
using System.Runtime.CompilerServices;

namespace Poz1.DiscreteLogarithm.Model
{
	public class Factor
	{
		public int Count
		{
			get;
			private set;
		}

		public int Number
		{
			get;
		}

		public Factor(int number)
		{
			this.Number = number;
		}

		public void IncreaseCounter()
		{
			this.Count = this.Count + 1;
		}

		public override string ToString()
		{
			string str = this.Number.ToString();
			int count = this.Count;
			return string.Concat(str, "^", count.ToString());
		}
	}
}