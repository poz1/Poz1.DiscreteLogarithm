using Poz1.DiscreteLogarithm.Model;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho
{
    public class PollardRhoS2<T> : PollardRhoPartition<T>
    {
        public override int GetNextA(IFiniteGroup<T> group, int a)
        {
            return (2 * a) % group.Order;
        }

        public override int GetNextB(IFiniteGroup<T> group, int b)
        {
            return (2 * b) % group.Order;
        }

        public override T GetNextX(IFiniteGroup<T> group, T alpha, T beta, T x)
        {
            return group.Multiply(x, x);
        }
    }
}
