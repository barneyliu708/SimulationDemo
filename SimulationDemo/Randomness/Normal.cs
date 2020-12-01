using Extreme.Statistics.Distributions;
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

        public void PrintOut()
        {
            Console.WriteLine($"Normal distribution with mane = {_mean}, standard deviation = {_sd}");
        }

        public override string ToString()
        {
            return $"Normal({_mean}, {_sd})";
        }

        public ValueType Sample()
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
