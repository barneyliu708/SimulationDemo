using SimulationDemo.Enums;
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
        private ItemAmountType _amountType;


        public Customer(int arrivalTime)
        {
            _arrivalTime = arrivalTime;
        }
    }
}
