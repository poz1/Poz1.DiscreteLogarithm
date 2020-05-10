using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho
{
    public class PollardRhoTriad<T>
    {
        public T X { get; }

        public int A { get; }

        public int B { get; }

        public PollardRhoTriad(T x, int a, int b)
        {
            X = x;
            A = a;
            B = b;
        }
    }
}
