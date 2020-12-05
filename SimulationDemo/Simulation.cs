﻿using SimulationDemo.Elements;
using SimulationDemo.Enums;
using SimulationDemo.Logger;
using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimulationDemo
{
    public class Simulation
    {
        private static int _globalTime;
        public static int GlobalTime { get => _globalTime; }
        public CheckoutArea CheckoutArea { get => _checkoutArea; }

        private int _maxIteration;
        private CheckoutArea _checkoutArea;

        private int _sleepmillesecond;

        public Simulation(int numCashier, int numSelfChechout, int numMachine, int maxIteration = int.MaxValue)
        {
            _sleepmillesecond = 500;
            _maxIteration = maxIteration;
            _checkoutArea = new CheckoutArea(numCashier, numSelfChechout, numMachine);
        }

        public void SpeedUp()
        {
            if (_sleepmillesecond >= 100)
            {
                _sleepmillesecond -= 100;
            }
        }

        public void SlowDown()
        {
            _sleepmillesecond += 100;
        }

        public void Execute()
        {
            SimLogger.Info("Execution started");
            for (_globalTime = 1; _globalTime < _maxIteration; ++_globalTime)
            {
                // the arrival of new customers
                if ((int)DistributionHelper.GetDistribution(EventEnum.Arrival).Sample() == 1) // check if new customer arrivals
                {
                    Customer newCustomer = new Customer(_globalTime);
                    newCustomer.ArriveCheckoutArea(_checkoutArea);

                    IQueue quickestQueue = _checkoutArea.QuickestQueue(newCustomer);
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
                    if (customer.IfShouldChangeLine(out IQueue newqueue))
                    {
                        customer.ChangeLine(newqueue);
                    }

                    if (customer.IfShouldAngryDeparture())
                    {
                        customer.AngryDeparture();
                    }
                }

                Thread.Sleep(_sleepmillesecond);
                //int cursorTop = Console.CursorTop;
                //Console.SetCursorPosition(0, 0);
                //for (int i = 0; i < cursorTop; i++)
                //{
                //    Console.WriteLine(new string(' ', 200));
                //}

                //Console.SetCursorPosition(0, 0);

                this.PrintOut();
                
            }
        }

        public void PrintOut()
        {
            Console.Clear();
            Console.WriteLine("################################");
            Console.WriteLine("#### Wating Line Similation ####");
            Console.WriteLine("################################");
            Console.WriteLine($"Current Unit time (iteration): {_globalTime}");

            DistributionHelper.PrintOut();
            
            _checkoutArea.PrintOut();
        }
    }
}
