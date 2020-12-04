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
            _currentInServiceCustomer = null;

            if (_waitingqueue.Count != 0) // if queue is empty, then _currentInServiceCustomer is null 
            {
                _currentInServiceCustomer = _waitingqueue.First?.Value;
                _currentInServiceCustomer.StartCheckout();
                _waitingqueue.RemoveFirst();
            }
        }

        public void PrintOut()
        {
            StringBuilder builder = new StringBuilder();
            foreach(var cur in _waitingqueue)
            {
                builder.Append($"[{cur.CustomerId}]");
            }
            Console.WriteLine($"Cashier[{_queueId}] [{(_currentInServiceCustomer != null ? "busy" : "idle")}] [{(_currentInServiceCustomer != null ? _currentInServiceCustomer.CustomerId : "        ")}] |{builder.ToString()}");
            //Console.WriteLine($"Cashier[{_queueId}] [{(_currentInServiceCustomer != null ? "busy" : "idle")}] [{(_currentInServiceCustomer != null ? _currentInServiceCustomer.CustomerId : "        ")}] |{new string('*', _waitingqueue.Count)}");
        }

        public bool IsQueueIdle()
        {
            return _currentInServiceCustomer == null && _waitingqueue.Count == 0;
        }

        //public override string ToString()
        //{
        //    return $"Cashier[{_queueId}] [{(_waitingqueue.Count != 0 ? "busy" : "idle")}] |{new string('*', _waitingqueue.Count)}";
        //}
    }
}
