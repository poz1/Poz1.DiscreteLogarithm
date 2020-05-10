using Poz1.DiscreteLogarithm.DiscreteLogarithm;
using Poz1.DiscreteLogarithm.DiscreteLogarithm.PollardRho;
using Poz1.DiscreteLogarithm.Model;
using System;

namespace Poz1.DiscreteLogarithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var algo = new PollardRhoAlgorithm();

            //var group = new IntegersModuloMultiplicativeGroup(251);
            //var res = algo.Solve(group, 71, 210, new System.Threading.CancellationToken());

            //var group = new IntegersModuloMultiplicativeGroup(113);
            //var res = algo.Solve(group, 3, 57, new System.Threading.CancellationToken());

            //var group = new IntegersModuloMultiplicativeGroup(229);
            //var res = algo.Solve(group, 6, 13, new System.Threading.CancellationToken());

            //var group = new IntegersModuloMultiplicativeGroup(383);
            //var res = algo.Solve(group, 2, 228, new System.Threading.CancellationToken());

            //var group = new IntegersModuloMultiplicativeGroup(1019);
            //var res = algo.Solve(group, 5, 2, new System.Threading.CancellationToken());

            //var group = new IntegersModuloMultiplicativeGroup(1019);
            //var res = algo.Solve(group, 5, 2, new System.Threading.CancellationToken());

            Console.WriteLine("result: " + res.Result);
            Console.ReadLine();
        }
    }
}
