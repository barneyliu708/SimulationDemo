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

        public CheckoutArea(int numCashier, int numSelfChechout, int numMachine)
        {
            _cashierQueues = new List<CashierQueue>();
            _selfCheckoutQueues = new List<SelfCheckoutQueue>();
            
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
                _selfCheckoutQueues.Add(new SelfCheckoutQueue(numMachine));
            }
        }

        public BaseQueue QuickestQueue()
        {
            BaseQueue ans = null;
            foreach(var queue in _cashierQueues)
            {
                if (ans == null || ans.NumOfWaitingCustomers() >= queue.NumOfWaitingCustomers())
                {
                    ans = queue;
                }
            }
            foreach (var queue in _selfCheckoutQueues)
            {
                if (ans == null || ans.NumOfWaitingCustomers() >= (queue.NumOfWaitingCustomers() / queue.NumOfMachines))
                {
                    ans = queue;
                }
            }

            return ans;
        } 
    }
}
