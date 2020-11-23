using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public class CashierQueue : BaseQueue, IQueue
    {
        private Customer _currentInServiceCustomer;

        public CashierQueue()
        {

        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(_currentInServiceCustomer);
            customers.AddRange(_waitingqueue);

            return customers;
        }

        public bool IsCurrentCustomerFinished()
        {
            if (_currentInServiceCustomer == null)
            {
                return true; // no customer is in service, so return true
            }

            return _currentInServiceCustomer.IsCheckoutFinished();
        }

        public void StartCheckoutForNextCustomer()
        {
            if (this.IsCurrentCustomerFinished() == false)
            {
                throw new Exception($"Current customer's checkout has not been finished yet, cannot start checking out for next one");
            }

            _currentInServiceCustomer?.DepartureAfterCheckout();

            if (_waitingqueue.Count != 0) // if queue is empty, then _currentInServiceCustomer is null 
            {
                _currentInServiceCustomer = _waitingqueue.First?.Value; 
                _waitingqueue.RemoveFirst();
            }
        }
    }
}
