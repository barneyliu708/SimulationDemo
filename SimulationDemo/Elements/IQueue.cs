﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public interface IQueue
    {
        int NumOfWaitingCustomers();
        void NewCustomersJoins(Customer newCustomer);
        void CustomerLeaves(Customer customer);
        bool IsCurrentCustomerFinished();
        void StartCheckoutForNextCustomer();
        IEnumerable<Customer> GetAllCustomers();
        int IndexOfCustomerInQueue(Customer customer);
    }
}
