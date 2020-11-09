using Extreme.Statistics.Distributions;
using SimulationDemo.Random;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Randomness
{
    public class Normal : DistributionBase, IDistribution
    {
        private double _mean;
        private double _sd;

        public Normal(double mean, double sd)
        {
            _mean = mean;
            _sd = sd;
        }
        public object Sample()
        {
            //double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            //double u2 = 1.0 - rand.NextDouble();
            //double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            //double randNormal = _mean + _sd * randStdNormal; //random normal(mean,stdDev^2)

            var normal = new NormalDistribution(_mean, _sd);
            return normal.Sample(rand);
        }
    }
}
