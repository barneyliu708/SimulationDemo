using SimulationDemo.Elements;
using SimulationDemo.Enums;
using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo
{
    public class Simulation
    {
        private static int _globalTime;
        public static int GlobalTime { get => _globalTime; }

        
        private int _maxIteration;
        private CheckoutArea _checkoutArea;

        public Simulation(int numCashier, int numSelfChechout, int numMachine, int maxIteration = int.MaxValue)
        {
            _maxIteration = maxIteration;
            _checkoutArea = new CheckoutArea(numCashier, numSelfChechout, numMachine);
        }

        public void Execute()
        {
            for (_globalTime = 0; _globalTime < _maxIteration; ++_globalTime)
            {
                // the arrival of new customers
                if ((int)DistributionHelper.GetDistribution(EventEnum.Arrival).Sample() == 1) // check if new customer arrivals
                {
                    Customer newCustomer = new Customer(_globalTime);
                    newCustomer.ArriveCheckoutArea(_checkoutArea);

                    IQueue quickestQueue = _checkoutArea.QuickestQueue();
                    newCustomer.JoinQueue(quickestQueue);
                }

                // the departure of customers after checking out -- loop through each queue
                IEnumerable<IQueue> queues = _checkoutArea.GetAllQueues();
                foreach(IQueue queue in queues)
                {
                    if (queue.IsCurrentCustomerFinished())
                    {
                        queue.StartCheckoutForNextCustomer();
                    }
                }

                // changing line of customers & angry departure of custoemrs -- loop through each customer
                IEnumerable<Customer> customers = _checkoutArea.GetAllCustomers();
                foreach(Customer customer in customers)
                {
                    if (customer.IfShouldChangeLine())
                    {
                        customer.ChangeLine();
                    }

                    if (customer.IfShouldAngryDeparture())
                    {
                        customer.AngryDeparture();
                    }
                }
            }
        }
    }
}
