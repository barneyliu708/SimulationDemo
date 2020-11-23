using SimulationDemo.Enums;
using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public class SelfCheckoutQueue : BaseQueue, IQueue
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

        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.AddRange(_currentInServiceCustomers);
            customers.AddRange(_waitingqueue);

            return customers;
        }

        public bool IsCurrentCustomerFinished()
        {
            for(int i = 0; i < _numOfMachines; i++)
            {
                if (_currentInServiceCustomers[i] == null || _currentInServiceCustomers[i].IsCheckoutFinished()) //there is a self-checkout machine available
                {
                    return true; 
                }
            }
            return false;
        }

        public void StartCheckoutForNextCustomer()
        {
            if (this.IsCurrentCustomerFinished() == false)
            {
                throw new Exception($"No self checkout machine will be available, cannot start checking out for next one");
            }

            for (int i = 0; i < _numOfMachines; i++)
            {
                if (_currentInServiceCustomers[i] == null || _currentInServiceCustomers[i].IsCheckoutFinished()) //there is a self-checkout machine available
                {
                    _currentInServiceCustomers[i]?.DepartureAfterCheckout();
                }
                if (_waitingqueue.Count != 0)  // if queue is empty, then _currentInServiceCustomer is null 
                {
                    _currentInServiceCustomers[i] = _waitingqueue.First.Value;
                    if ((int)DistributionHelper.GetDistribution(EventEnum.MachineError).Sample() == 1) // machine error occurs
                    {
                        _currentInServiceCustomers[i].NeedHelpForSelfCheckout();
                    }
                    _waitingqueue.RemoveFirst();
                }
            }
        }
    }
}
