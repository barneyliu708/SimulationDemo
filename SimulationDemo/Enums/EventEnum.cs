using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Enums
{
    public enum EventEnum
    {
        Arrival,
        BuyingItems,
        ScaningSmallAmountItems,
        ScaningMediumAmountItems,
        ScaningLargeAmountItems,
        MakingPayment,
        MachineError,
        FixingMachineError,
        AngryDeparture
    }
}
