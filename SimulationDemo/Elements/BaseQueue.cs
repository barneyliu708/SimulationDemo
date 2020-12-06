using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public abstract class BaseQueue
    {
        public BaseQueue()
        {
            _queueId = RandomStrGenerator.GetRandomString();
            _isQueueOpened = true;
            _last10waitingtime = new LinkedList<int>();
        }

        protected string _queueId;
        protected bool _isQueueOpened;
        protected LinkedList<Customer> _waitingqueue;

        protected int _totalWaitingTime;
        protected double _avgWaitingTime;
        protected int _totalDepartureCustomerCount;
        protected LinkedList<int> _last10waitingtime;

        public string QueueId { get => _queueId; }
        public bool IsQueueOpened { get => _isQueueOpened; }

        public void CloseQueue()
        {
            _isQueueOpened = false;
        }

        public void OpenQueue()
        {
            _isQueueOpened = true;
        }

        // customer will arrive and join at the end of the queue 
        public void NewCustomersJoins(Customer newCustomer)
        {
            _waitingqueue.AddLast(newCustomer);
            
        }

        // leave without buying anything
        public void CustomerLeavesWaitingQueue(Customer customer)
        {
            _waitingqueue.Remove(customer);
            this.UpdateStatisticsOnDeparture(customer);
        }

        public int NumOfWaitingCustomers()
        {
            return _waitingqueue.Count;
        }

        public int IndexOfCustomerInQueue(Customer customer)
        {
            if (_waitingqueue.Find(customer) == null)
            {
                throw new Exception("Customer does not belows to this queue");
            }
            int index = 0;
            LinkedListNode<Customer> current = _waitingqueue.First;
            while(current != null && current.Value != customer)
            {
                current = current.Next;
                index++;
            }
            return index;
        }

        public void UpdateStatisticsOnDeparture(Customer customer)
        {
            if (customer == null)
            {
                return;
            }

            _totalDepartureCustomerCount++;

            int newWaitingTime = 0;
            if (customer.StartCheckoutTime == 0)
            {
                newWaitingTime = (Simulation.GlobalTime - customer.ArrivalTime);
            }
            else
            {
                newWaitingTime = (customer.StartCheckoutTime - customer.ArrivalTime);
            }

            _totalWaitingTime += newWaitingTime;
            _last10waitingtime.AddLast(newWaitingTime);
            if (_last10waitingtime.Count > 10)
            {
                _last10waitingtime.RemoveFirst();
            }
            _avgWaitingTime = _totalWaitingTime / _totalDepartureCustomerCount;
        }
    }
}
