using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.DiscreteLogarithm.Algebra
{
    public class Congruence<T>
    {   
        public T Value { get; }
        public T Modulus { get; }

        public Congruence(T value, T modulus)
        {
            Value = value;
            Modulus = modulus;
        }
    }
}
