using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public class SelfCheckoutQueue : BaseQueue
    {
        private int _countOfMachines;
        private Customer[] _checkingoutCustomers;

        public SelfCheckoutQueue(int countOfMachines)
        {
            _countOfMachines = countOfMachines;
            _checkingoutCustomers = new Customer[_countOfMachines];
        }

        // customer will arrive and join at the end of the queue 
        public void NewCustomersArrives(Customer newCustomer)
        {
            _queue.AddLast(newCustomer);
        }

        // customer will leave either after finishing the checkout, or leave without buying anything
        public void CustomerLeaves(Customer customer)
        {
            _queue.Remove(customer);
        }
    }
}
