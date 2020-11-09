using Extreme.Statistics.Distributions;
using SimulationDemo.Random;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Randomness
{
    public class Bernoulli : DistributionBase, IDistribution 
    {
        private double _probability;

        public Bernoulli(double probability)
        {
            _probability = probability;
        }
        public object Sample()
        {
            var bernoulli = new BernoulliDistribution(_probability);
            return bernoulli.Sample(rand);
        }
    }
}
