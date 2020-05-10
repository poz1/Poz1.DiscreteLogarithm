using Poz1.DiscreteLogarithm.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho
{
    public abstract class PollardRhoPartition<T>
    {
        public abstract T GetNextX(IFiniteGroup<T> group, T alpha, T beta, T x);
        public abstract int GetNextA(IFiniteGroup<T> group, int a);
        public abstract int GetNextB(IFiniteGroup<T> group, int b);
        public PollardRhoTriad<T> GetNextTriad(IFiniteGroup<T> group, T alpha, T beta, T x, int a, int b)
        {
            return new PollardRhoTriad<T>(GetNextX(group, alpha, beta, x), GetNextA(group, a), GetNextB(group, b));
        }
    }
}
