using Poz1.DiscreteLogarithm.Model;

namespace Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho
{
    class PollardRhoS3<T> : PollardRhoPartition<T>
    {
        public override int GetNextA(IFiniteGroup<T> group, int a)
        {
            return (a + 1) % group.Order;
        }

        public override int GetNextB(IFiniteGroup<T> group, int b)
        {
            return b;
        }

        public override T GetNextX(IFiniteGroup<T> group, T alpha, T beta, T x)
        {
            return group.Multiply(alpha, x);
        }
    }
}
