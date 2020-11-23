using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public class CashierQueue : BaseQueue
    {
        private Customer _currentInServiceCustomer;

        public CashierQueue()
        {

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

            if (_currentInServiceCustomer != null)
            {
                _currentInServiceCustomer.DepartureAfterCheckout();
                _currentInServiceCustomer = null;
            }

            _currentInServiceCustomer = _queue.First?.Value; // if queue is empty, then _currentInServiceCustomer is null
            if (_queue.Count != 0)
            {
                _queue.RemoveFirst();
            }
        }
    }
}
