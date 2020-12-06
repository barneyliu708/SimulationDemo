using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationDemo.Elements
{
    public class CashierQueue : BaseQueue, IQueue
    {
        private Customer _currentInServiceCustomer;

        public CashierQueue() : base()
        {
            _waitingqueue = new LinkedList<Customer>();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(_currentInServiceCustomer);
            customers.AddRange(_waitingqueue);

            return customers.Where(c => c != null);
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
            this.UpdateStatisticsOnDeparture(_currentInServiceCustomer);
            _currentInServiceCustomer = null;

            if (_waitingqueue.Count != 0) // if queue is empty, then _currentInServiceCustomer is null 
            {
                _currentInServiceCustomer = _waitingqueue.First?.Value;
                _currentInServiceCustomer.StartCheckout();
                _waitingqueue.RemoveFirst();
            }
        }

        public bool IsQueueIdle()
        {
            return _currentInServiceCustomer == null && _waitingqueue.Count == 0;
        }

        public void PrintOut()
        {
            StringBuilder builder = new StringBuilder();
            foreach(var cur in _waitingqueue)
            {
                builder.Append($"[{cur.CustomerId}]");
            }
            Console.WriteLine($"Cashier[{_queueId}] [{(_currentInServiceCustomer != null ? "busy" : "idle")}] [{(_isQueueOpened ? "opened" : "closed")}] " +
                $"- Avg. Waiting Time: {_avgWaitingTime:00} " +
                $"- Lastest 10 Cust Avg. Waiting Time: {(_last10waitingtime.Count == 0 ? 0 : _last10waitingtime.Sum() / _last10waitingtime.Count):00} " +
                $"- [{(_currentInServiceCustomer != null ? _currentInServiceCustomer.CustomerId : "        ")}] ||{builder}");
        }
    }
}
