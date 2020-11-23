using SimulationDemo.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Randomness
{
    public static class DistributionHelper
    {
        public static Dictionary<EventEnum, IDistribution> _distribution = new Dictionary<EventEnum, IDistribution>()
        {
            { EventEnum.Arrival, new Bernoulli(0.25) },
            { EventEnum.BuyingItems, new ThreeOutcomes(30, 40) },
            { EventEnum.ScaningSmallAmountItems, new Normal(2, 1) },
            { EventEnum.ScaningMediumAmountItems, new Normal(4, 1) },
            { EventEnum.ScaningLargeAmountItems, new Normal(6, 1) },
            { EventEnum.MakingPayment, new Normal(1, 1) },
            { EventEnum.MachineError, new Bernoulli(0.05)},
            { EventEnum.FixingMachineError, new Normal(1, 1) },
            { EventEnum.AngryDeparture, new Normal(60, 1) }
        };
    }
}
