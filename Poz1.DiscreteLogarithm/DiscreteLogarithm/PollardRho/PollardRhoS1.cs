using Poz1.DiscreteLogarithm.Model;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho
{
    public class PollardRhoS1<T> : PollardRhoPartition<T>
    { 
        public override int GetNextA(IFiniteGroup<T> group, int a)
        {
            return a;
        }
        public override int GetNextB(IFiniteGroup<T> group, int b)
        {
            return (b + 1) % group.Order;
        }

        public override T GetNextX(IFiniteGroup<T> group, T alpha, T beta, T x)
        {
            return group.Multiply(beta, x);
        }
    }
}
