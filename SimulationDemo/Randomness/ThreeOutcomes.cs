using Extreme.Statistics.Distributions;
using SimulationDemo.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Randomness
{
    public class ThreeOutcomes : DistributionBase, IDistribution
    {
        private int _accumulateOutcome1;
        private int _accumulateOutcome2;

        public ThreeOutcomes(int pOutcome1, int pOutcome2)
        {
            if (pOutcome1 + pOutcome2 > 100)
            {
                throw new Exception($"Wrong setting: probability of outcome 1 = {pOutcome1}, probability of outcome 2 = {pOutcome2}");
            }

            _accumulateOutcome1 = pOutcome1;
            _accumulateOutcome2 = pOutcome1 + pOutcome2;
        }

        public void PrintOut()
        {
            Console.WriteLine($"Three outcomes distribution with {EventEnum.ScaningSmallAmountItems}'s probability = {_accumulateOutcome1}; {EventEnum.ScaningMediumAmountItems}'s probability = {_accumulateOutcome2 - _accumulateOutcome1}; {EventEnum.ScaningLargeAmountItems}'s probability = {1 - _accumulateOutcome2};");
        }

        public override string ToString()
        {
            return $"Three outcomes: ({EventEnum.ScaningSmallAmountItems} = {_accumulateOutcome1}%; {EventEnum.ScaningMediumAmountItems} = {_accumulateOutcome2 - _accumulateOutcome1}%; {EventEnum.ScaningLargeAmountItems} = {100 - _accumulateOutcome2}%;";
        }

        public ValueType Sample()
        {
            var uniformDistribution = new DiscreteUniformDistribution(100); //[0, maxValue)
            var randValue = uniformDistribution.Sample(rand) + 1; 
            
            if (randValue >= 0 && randValue <= _accumulateOutcome1)
            {
                return EventEnum.ScaningSmallAmountItems;
            }
            else if (randValue > _accumulateOutcome1 && randValue <= _accumulateOutcome2)
            {
                return EventEnum.ScaningMediumAmountItems;
            }
            return EventEnum.ScaningLargeAmountItems;
        }
    }
}
