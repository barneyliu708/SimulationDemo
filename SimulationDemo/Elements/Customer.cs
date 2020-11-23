using SimulationDemo.Enums;
using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public class Customer
    {
        private int _arrivalTime;
        private int _maxToleranceTime;
        private int _scanAndPackingTime;
        private int _makePaymentTime;
        private EventEnum _amountItems;


        public Customer(int arrivalTime)
        {
            _arrivalTime = arrivalTime;
            _amountItems = (EventEnum)DistributionHelper.GetDistribution(EventEnum.BuyingItems).Sample();
            _scanAndPackingTime = (int)DistributionHelper.GetDistribution(_amountItems).Sample();
            _makePaymentTime = (int)DistributionHelper.GetDistribution(EventEnum.MakingPayment).Sample();
            _maxToleranceTime = (int)DistributionHelper.GetDistribution(EventEnum.AngryDeparture).Sample();
        }


    }
}
