using Extreme.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Randomness
{
    public class Poison : DistributionBase, IDistribution
    {
        private double _lambda;

        public Poison(double lambda)
        {
            _lambda = lambda;
        }

        public ValueType Sample()
        {
            var poisonDist = new PoissonDistribution(_lambda);
            return poisonDist.Sample(rand);
        }

        public void PrintOut()
        {
            Console.WriteLine($"Bernoulli distribution with probability = {_lambda}");
        }

        public override string ToString()
        {
            return $"Poisson({_lambda})";
        }
    }
}
