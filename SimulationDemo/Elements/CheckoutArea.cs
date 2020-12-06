using SimulationDemo.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationDemo.Elements
{
    public class CheckoutArea
    {
        private ConcurrentBag<CashierQueue> _cashierQueues;
        private List<SelfCheckoutQueue> _selfCheckoutQueues;
        private int _numMachine;

        private int _totalWaitingTime;
        private double _avgWaitingTime;
        private int _totalAngryLeaveCount;
        private int _totalArrivalCustomerCount;
        private int _totalDepartureCustomerCount;
        private int _totalChangeLineCount;
        private int[] _last10waitingtime;
        private int _idx;

        public int NumMachine { get => _numMachine; }

        public CheckoutArea(int numCashier, int numSelfChechout, int numMachine)
        {
            _cashierQueues = new ConcurrentBag<CashierQueue>();
            _selfCheckoutQueues = new List<SelfCheckoutQueue>();
            _numMachine = numMachine;
            _last10waitingtime = new int[10];
            _idx = 0;


            if (numCashier + numSelfChechout <= 0 && numCashier * numSelfChechout <= 0)
            {
                throw new Exception($"At least one line should be opened: numCashier = {numCashier}, numSelfCheckout = {numSelfChechout}");
            }

            for (int i = 0; i < numCashier; i++)
            {
                _cashierQueues.Add(new CashierQueue());
            }
            for (int i = 0; i < numSelfChechout; i++)
            {
                _selfCheckoutQueues.Add(new SelfCheckoutQueue(_numMachine));
            }
        }

        public void AddOneNewCashier()
        {
            var closedQueue = _cashierQueues.FirstOrDefault(q => q.IsQueueOpened == false);
            if (closedQueue != null)
            {
                closedQueue.OpenQueue();
            }
            else
            {
                _cashierQueues.Add(new CashierQueue());
            }
        }

        public void CloseOneCashier()
        {
            _cashierQueues.FirstOrDefault(q => q.IsQueueOpened)?.CloseQueue();
        }

        public IQueue QuickestQueue(Customer customer)
        {
            IQueue quickestCashier = null;
            foreach (var currentQ in _cashierQueues)
            {
                if (currentQ.IsQueueOpened == false)
                {
                    continue;
                }

                if (quickestCashier == null || 
                    currentQ.IsQueueIdle() || 
                    (!currentQ.IsQueueIdle() && !quickestCashier.IsQueueIdle() && quickestCashier.NumOfWaitingCustomers() > currentQ.NumOfWaitingCustomers()))
                {
                    quickestCashier = currentQ;
                }
            }

            IQueue quickestSelfCheckout = null;
            foreach (var currentQ in _selfCheckoutQueues)
            {
                if (currentQ.IsQueueOpened == false)
                {
                    continue;
                }

                if (quickestSelfCheckout == null || 
                    currentQ.IsQueueIdle() || 
                    (!currentQ.IsQueueIdle() && !quickestSelfCheckout.IsQueueIdle() && quickestSelfCheckout.NumOfWaitingCustomers() >= currentQ.NumOfWaitingCustomers()))
                {
                    quickestSelfCheckout = currentQ;
                }
            }

            // -- only the customers with small or medium amount of items are allowed to use self-checkout area
            if (customer.AmountItems == EventEnum.ScaningLargeAmountItems)
            {
                return quickestCashier;
            }

            if (quickestSelfCheckout.IsQueueIdle())
            {
                return quickestSelfCheckout;
            }

            if (quickestCashier.IsQueueIdle())
            {
                return quickestCashier;
            }

            return quickestCashier.NumOfWaitingCustomers() <= (quickestSelfCheckout.NumOfWaitingCustomers() / (_numMachine)) ? quickestCashier : quickestSelfCheckout;
        }

        public IEnumerable<IQueue> GetAllQueues()
        {
            List<IQueue> allQueues = new List<IQueue>();
            allQueues.AddRange(_cashierQueues);
            allQueues.AddRange(_selfCheckoutQueues);

            return allQueues;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            List<Customer> allCustomers = new List<Customer>();
            foreach(IQueue queue in this.GetAllQueues())
            {
                allCustomers.AddRange(queue.GetAllCustomers());
            }
            return allCustomers;
        }

        public void UpdateStatisticsOnArrival(Customer customer)
        {
            _totalArrivalCustomerCount++;
        }

        public void UpdateStatisticsOnChangeLine(Customer customer)
        {
            _totalChangeLineCount++;
        }

        public void UpdateStatisticsOnDeparture(Customer customer)
        {
            _totalDepartureCustomerCount++;

            int newWaitingTime = 0;
            if (customer.StartCheckoutTime == 0)
            {
                _totalAngryLeaveCount++;
                newWaitingTime = (Simulation.GlobalTime - customer.ArrivalTime);
            }
            else
            {
                newWaitingTime = (customer.StartCheckoutTime - customer.ArrivalTime);
            }

            _totalWaitingTime += newWaitingTime;
            _last10waitingtime[_idx++] = newWaitingTime;
            _idx %= _last10waitingtime.Length;
            _avgWaitingTime = _totalWaitingTime / _totalDepartureCustomerCount;
        }
        public void PrintOut()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------Statistics---------------------------------------------------");
            Console.WriteLine($"Total Arrival: {_totalArrivalCustomerCount}");
            Console.WriteLine($"Total Departure: {_totalDepartureCustomerCount}");
            Console.WriteLine($"Total Remainint Customers: {_totalArrivalCustomerCount - _totalDepartureCustomerCount}");
            Console.WriteLine($"Total Angry Departure: {_totalAngryLeaveCount}");
            Console.WriteLine($"Avg. Waiting Time: {_avgWaitingTime}");
            StringBuilder sb = new StringBuilder();
            foreach(int n in _last10waitingtime)
            {
                sb.Append($"{n}, ");
            }
            Console.WriteLine($"Avg. Waiting Time of Lastest {_last10waitingtime.Length} Customers: [{sb.ToString(0, sb.Length - 2)}]");
            Console.WriteLine();
            Console.WriteLine("-------------------------------Checkout Area------------------------------------------------");
            foreach (var q in _cashierQueues)
            {
                q.PrintOut();
            }

            foreach(var q in _selfCheckoutQueues)
            {
                q.PrintOut();
            }
        }
    }
}
