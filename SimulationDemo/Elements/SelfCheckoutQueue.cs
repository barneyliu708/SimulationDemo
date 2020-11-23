using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public class SelfCheckoutQueue : BaseQueue
    {
        private int _numOfMachines;
        private Customer[] _currentInServiceCustomers;

        public SelfCheckoutQueue(int numOfMachines)
        {
            if (numOfMachines <= 0)
            {
                throw new Exception($"The number of self checkout machine should be greater than zero: numOfMachines = {numOfMachines}");
            }

            _numOfMachines = numOfMachines;
            _currentInServiceCustomers = new Customer[_numOfMachines];
        }

        public int NumOfMachines { get => _numOfMachines; }
    }
}
