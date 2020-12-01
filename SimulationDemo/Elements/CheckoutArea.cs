using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationDemo.Elements
{
    public class CheckoutArea
    {
        private List<CashierQueue> _cashierQueues;
        private List<SelfCheckoutQueue> _selfCheckoutQueues;
        private int _numMachine;

        public int NumMachine { get => _numMachine; }

        public CheckoutArea(int numCashier, int numSelfChechout, int numMachine)
        {
            _cashierQueues = new List<CashierQueue>();
            _selfCheckoutQueues = new List<SelfCheckoutQueue>();
            _numMachine = numMachine;


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

        public IQueue QuickestQueue()
        {
            IQueue quickestCashier = null;
            foreach (var currentQ in _cashierQueues)
            {
                if (quickestCashier == null || currentQ.IfQueueIdle() || quickestCashier.NumOfWaitingCustomers() >= currentQ.NumOfWaitingCustomers())
                {
                    quickestCashier = currentQ;
                }
            }

            IQueue quickestSelfCheckout = null;
            foreach (var currentQ in _selfCheckoutQueues)
            {
                if (quickestSelfCheckout == null || currentQ.IfQueueIdle() || quickestSelfCheckout.NumOfWaitingCustomers() >= currentQ.NumOfWaitingCustomers())
                {
                    quickestSelfCheckout = currentQ;
                }
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

        public void PrintOut()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            foreach(var q in _cashierQueues)
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
