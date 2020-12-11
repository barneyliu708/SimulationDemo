using SimulationDemo.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Randomness
{
    public static class DistributionHelper
    {
        private static object lockobj = new object();
        private static Dictionary<EventEnum, IDistribution> _distribution = new Dictionary<EventEnum, IDistribution>()
        {
            { EventEnum.Arrival, new Poison(3.5) },
            { EventEnum.BuyingItems, new ThreeOutcomes(30, 40) },
            { EventEnum.ScaningSmallAmountItems, new Normal(1, 1) },
            { EventEnum.ScaningMediumAmountItems, new Normal(4, 1) },
            { EventEnum.ScaningLargeAmountItems, new Normal(7, 1) },
            { EventEnum.MakingPayment, new Normal(1, 0.5) },
            { EventEnum.MachineError, new Bernoulli(0.05)},
            { EventEnum.FixingMachineError, new Normal(5, 1) },
            { EventEnum.AngryDeparture, new Normal(30, 1) }
        };

        public static void UpdateDistribution(EventEnum eventType, IDistribution distribution)
        {
            lock (lockobj)
            {
                _distribution[eventType] = distribution;
            }
        }

        public static IDistribution GetDistribution(EventEnum eventType)
        {
            lock(lockobj)
            {
                return _distribution[eventType];
            }
        }

        public static void PrintOut()
        {
            Console.WriteLine("------------------------------Randomness Settings--------------------------------------------");
            foreach (var kv in _distribution)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value.ToString()}");
            }
        }
    }
}
