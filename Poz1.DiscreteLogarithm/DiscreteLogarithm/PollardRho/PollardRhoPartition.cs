using System;
using System.Collections.Generic;
using System.Text;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho
{
    public abstract class PollardRhoPartition<T>
    {
        public abstract T GetNextX();
        public abstract int GetNextA();
        public abstract int GetNextB();
    }
}
