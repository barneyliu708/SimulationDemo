using SimulationDemo.Enums;
using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationDemo.Elements
{
    public class SelfCheckoutQueue : BaseQueue, IQueue
    {
        private int _numOfMachines;
        private Customer[] _currentInServiceCustomers;

        public SelfCheckoutQueue(int numOfMachines) : base()
        {
            _waitingqueue = new LinkedList<Customer>();

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

            return customers.Where(c => c != null);
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
                    _currentInServiceCustomers[i] = null;

                    if (_waitingqueue.Count != 0)  // if queue is empty, then _currentInServiceCustomer is null 
                    {
                        _currentInServiceCustomers[i] = _waitingqueue.First.Value;
                        _currentInServiceCustomers[i].StartCheckout();

                        if ((int)DistributionHelper.GetDistribution(EventEnum.MachineError).Sample() == 1) // machine error occurs
                        {
                            _currentInServiceCustomers[i].NeedHelpForSelfCheckout();
                        }
                        _waitingqueue.RemoveFirst();
                    }
                }
            }
        }

        public void PrintOut()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var cur in _waitingqueue)
            {
                builder.Append($"[{cur.CustomerId}]");
            }
            Console.WriteLine($"Self-Checkout[{_queueId}] [{(_waitingqueue.Count != 0 ? "busy" : "idle")}] |{builder.ToString()}");

            for (int i = 0; i < _numOfMachines; i++)
            {
                Console.WriteLine($" - Machine {i+1}: [{(_currentInServiceCustomers[i] != null ? _currentInServiceCustomers[i].CustomerId : "        ")}]");
            }
        }

        public bool IsQueueIdle()
        {
            return _currentInServiceCustomers.Any(x => x == null) && _waitingqueue.Count == 0;
        }
    }
}
