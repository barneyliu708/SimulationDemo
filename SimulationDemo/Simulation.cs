using Simulation.Logger;
using SimulationDemo.Elements;
using SimulationDemo.Enums;
using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimulationDemo
{
    public class Simulation
    {
        static Bernoulli arrival = new Bernoulli(0.5);
        static System.Random rand = new System.Random();
        
        private string testfield;
        public string Testfield { get => testfield; set => testfield = value; }

        private static int _globalTime;
        public static int GlobalTime { get => _globalTime; }
        

        private int _maxIteration;
        private CheckoutArea _checkoutArea;

        public Simulation(int numCashier, int numSelfChechout, int numMachine, int maxIteration = int.MaxValue)
        {
            _maxIteration = maxIteration;
            _checkoutArea = new CheckoutArea(numCashier, numSelfChechout, numMachine);
        }

        public void ExecuteTest()
        {
            

            int totalarrival = 0;
            int totaldepartuer = 0;
            for (int i = 0; i < 10000; ++i)
            {
                Console.WriteLine("#### Wating Line Similation ####");
                //var interarrival = string.Empty;
                ////interarrival = Configuration.GetValue<string>("Interarrival");
                //using (StreamReader r = new StreamReader(AppContext.BaseDirectory + "appSettings.json"))
                //{
                //    string json = r.ReadToEnd();
                //    dynamic items = JsonConvert.DeserializeObject<dynamic>(json);
                //    interarrival = (string)items["Interarrival"];
                //}

                totalarrival += (int)arrival.Sample() * 1;
                totaldepartuer += (int)arrival.Sample() * 1;
                Console.WriteLine($"Unit time (iteration): {i}");
                Console.WriteLine($"Interarrival: {Testfield}");
                Console.WriteLine($"Total customer arrived: {totalarrival}");
                Console.WriteLine($"Total customer departure: {(totaldepartuer > 20 ? totaldepartuer - 20 : 0)}");
                //Console.WriteLine($"Casher 1: {new string('*', rand.Next(1, 10))} ");
                //Console.WriteLine($"Casher 2: {new string('*', rand.Next(1, 10))} ");
                //Console.WriteLine($"Casher 3: {new string('*', rand.Next(1, 10))} ");
                //Console.WriteLine($"Casher 4s: {new string('*', rand.Next(1, 10))} ");
                Console.WriteLine($"Casher 1      [Opened]|{new string('*', rand.Next(1, 15))}");
                Console.WriteLine($"Casher 2      [Opened]|{new string('*', rand.Next(1, 15))}");
                Console.WriteLine($"Casher 3      [Opened]|{new string('*', rand.Next(1, 15))}");
                Console.WriteLine($"Casher 4      [Closed]|");
                Console.WriteLine($"Self-Checkout [Opened]|{new string('*', rand.Next(1, 15))}");
                //Console.WriteLine($"Casher 1: {rand.Next(1, 10)}");
                //Console.WriteLine($"Casher 2: {rand.Next(1, 10)}");
                //Console.WriteLine($"Casher 3: {rand.Next(1, 10)}");
                //Console.WriteLine($"Casher 4: {rand.Next(1, 10)}");
                Thread.Sleep(1000);

                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");

                Console.SetCursorPosition(0, 0);
                //Console.WriteLine("Over previous line!!!");
            }
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

                Thread.Sleep(1000);
                int cursorTop = Console.CursorTop;
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < cursorTop; i++)
                {
                    Console.WriteLine(new string(' ', 200));
                }

                Console.SetCursorPosition(0, 0);
                this.PrintOut();
                
            }
        }

        public void PrintOut()
        {
            Console.WriteLine("################################");
            Console.WriteLine("#### Wating Line Similation ####");
            Console.WriteLine("################################");
            Console.WriteLine($"Current Unit time (iteration): {_globalTime}");

            DistributionHelper.PrintOut();
            
            _checkoutArea.PrintOut();
        }
    }
}
