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

            //var group = new ModuloMultiplicativeGroup(251);
            //var res = algo.Solve(group, 71, 210, new System.Threading.CancellationToken());

            //var group = new ModuloMultiplicativeGroup(229);
            //var res = algo.Solve(group, 6, 13, new System.Threading.CancellationToken());

            //var group = new ModuloMultiplicativeGroup(1019);
            //var res = algo.Solve(group, 5, 2, new System.Threading.CancellationToken());

            var algo = new ExhaustiveSearchAlgorithm();
            var group = new ModuloMultiplicativeGroup(113);
            var res = algo.Solve(group, 3, 57, new System.Threading.CancellationToken());

            //var algo = new BabyStepGiantStepAlgorithm();
            //var group = new ModuloMultiplicativeGroup(113);
            //var res = algo.Solve(group, 3, 57, new System.Threading.CancellationToken());

            //var algo = new PollardRhoAlgorithm(191);
            //var group = new ModuloMultiplicativeGroup(383);
            //var res = algo.Solve(group, 2, 228, new System.Threading.CancellationToken());

            //var algo = new PohligHellman();
            //var group = new ModuloMultiplicativeGroup(251);
            //var res = algo.Solve(group, 71, 210, new System.Threading.CancellationToken());

            Console.WriteLine("result: " + res.Result);
            Console.ReadLine();
        }
    }
}
