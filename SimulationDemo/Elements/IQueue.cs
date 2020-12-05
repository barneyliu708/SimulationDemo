using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public interface IQueue
    {
        bool IsQueueOpened { get; }
        string QueueId { get; }
        int NumOfWaitingCustomers();
        void CloseQueue();
        void OpenQueue();
        void NewCustomersJoins(Customer newCustomer);
        void CustomerLeaves(Customer customer);
        bool IsCurrentCustomerFinished();
        void StartCheckoutForNextCustomer();
        IEnumerable<Customer> GetAllCustomers();
        int IndexOfCustomerInQueue(Customer customer);
        bool IsQueueIdle();

    }
}
