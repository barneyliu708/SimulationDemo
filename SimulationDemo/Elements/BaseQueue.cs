﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public abstract class BaseQueue
    {
        protected LinkedList<Customer> _queue;

        // customer will arrive and join at the end of the queue 
        public void NewCustomersJoins(Customer newCustomer)
        {
            _queue.AddLast(newCustomer);
            
        }

        // customer will leave either after finishing the checkout, or leave without buying anything
        public void CustomerLeaves(Customer customer)
        {
            _queue.Remove(customer);
        }

        public int NumOfWaitingCustomers()
        {
            return _queue.Count;
        }
    }
}
