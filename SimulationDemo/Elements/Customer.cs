using SimulationDemo.Enums;
using SimulationDemo.Logger;
using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public class Customer
    {
        private string _customerId;

        private int _arrivalTime;
        private int _maxToleranceTime;
        private int _scanAndPackingTime;
        private int _makePaymentTime;
        private int _needHelpTime;
        private EventEnum _amountItems;
        private int _startCheckoutTime;

        private IQueue _joinedQueue;
        private CheckoutArea _checkoutArea;
        private bool _isNeedHelpForSelfCheckout;

        public string CustomerId { get => _customerId; }

        public Customer(int arrivalTime)
        {
            _customerId = RandomStrGenerator.GetRandomString();

            _arrivalTime = arrivalTime;
            _amountItems = (EventEnum)DistributionHelper.GetDistribution(EventEnum.BuyingItems).Sample();
            _scanAndPackingTime = Convert.ToInt32(DistributionHelper.GetDistribution(_amountItems).Sample());
            _makePaymentTime = Convert.ToInt32(DistributionHelper.GetDistribution(EventEnum.MakingPayment).Sample());
            _maxToleranceTime = Convert.ToInt32(DistributionHelper.GetDistribution(EventEnum.AngryDeparture).Sample());
            _needHelpTime = Convert.ToInt32(DistributionHelper.GetDistribution(EventEnum.FixingMachineError).Sample());
        }

        public bool IsCheckoutFinished()
        {
            if (this.IsCheckoutStarted() == false)
            {
                return false; 
            }

            int checkoutFinishedTime = _startCheckoutTime + _scanAndPackingTime + _makePaymentTime;
            if (_isNeedHelpForSelfCheckout)
            {
                checkoutFinishedTime += _needHelpTime;
            }

            return Simulation.GlobalTime >= checkoutFinishedTime;
        }

        public bool IsCheckoutStarted()
        {
            return _startCheckoutTime != 0;
        }

        public int NumOfWaitingCustomersAhead()
        {
            return 1 + _joinedQueue.IndexOfCustomerInQueue(this);
        }

        public bool IfShouldAngryDeparture()
        {
            if (this.IsCheckoutStarted())
            {
                return false;
            }
            return Simulation.GlobalTime >= _arrivalTime + _maxToleranceTime;
        }

        public bool IfShouldChangeLine()
        {
            if (this.IsCheckoutStarted())
            {
                return false; // cannot change line anymore if the customer already start checking out.
            }
            Type joinedQueueType = _joinedQueue.GetType();
            int eNumOfCustomersAhead = this.NumOfWaitingCustomersAhead();
            if (joinedQueueType == typeof(SelfCheckoutQueue))
            {
                eNumOfCustomersAhead = eNumOfCustomersAhead / _checkoutArea.NumMachine;
            }

            IQueue quickestQueue = _checkoutArea.QuickestQueue();
            Type quickestQueueType = quickestQueue.GetType();
            int eNumOfCustomers = quickestQueue.NumOfWaitingCustomers();
            if (quickestQueueType == typeof(SelfCheckoutQueue))
            {
                eNumOfCustomers = eNumOfCustomers / _checkoutArea.NumMachine;
            }

            var ifShouldChange = eNumOfCustomersAhead - eNumOfCustomers >= 3;
            if(ifShouldChange)
            {
                SimLogger.Info($"Customer [{_customerId}] finds a quick queue: current has {eNumOfCustomersAhead} ahead in queue {_joinedQueue.QueueId}, while queue {quickestQueue.QueueId} has total {eNumOfCustomers} waiting customers");
            }
            return ifShouldChange; // if current joined queue is not the quickest queue in the check out area, then should change
        }

        // ToDO
        public void ChangeLine()
        {
            if (this.IsCheckoutStarted())
            {
                throw new Exception("Cannot change line anymore if the customer already start checking out.");
            }
            this.LeaveQueue();
            this.JoinQueue(_checkoutArea.QuickestQueue());
        }

        public void NeedHelpForSelfCheckout()
        {
            _isNeedHelpForSelfCheckout = true;
        }

        public void ArriveCheckoutArea(CheckoutArea checkoutArea)
        {
            _checkoutArea = checkoutArea;
        }

        public void DepatureCheckoutArea()
        {
            _checkoutArea = null;
        }

        public void JoinQueue(IQueue queue)
        {
            _joinedQueue = queue;
            _joinedQueue.NewCustomersJoins(this);
            SimLogger.Info($"Customer [{_customerId}] joined the queue {queue.QueueId}");
        }

        public void LeaveQueue()
        {
            if (_joinedQueue == null)
            {
                throw new Exception($"The customer has not join any queue yet");
            }

            _joinedQueue.CustomerLeaves(this);
            SimLogger.Info($"Customer [{_customerId}] leave the queue {_joinedQueue.QueueId}");

            _joinedQueue = null;
        }

        public void StartCheckout()
        {
            SimLogger.Info($"Customer [{_customerId}] start checking out at queue {_joinedQueue.QueueId}");
            _startCheckoutTime = Simulation.GlobalTime;
        }

        public void DepartureAfterCheckout()
        {
            if (this.IsCheckoutFinished() == false)
            {
                throw new Exception($"Cannot departure since checkout is not finished yet");
            }
            // this.LeaveQueue(); // does not need to call leavQueue method as in-service customer is not defined as in-line customer
            SimLogger.Info($"Customer [{_customerId}] departure after checking out at queue {_joinedQueue.QueueId}");
            _joinedQueue = null;
            this.DepatureCheckoutArea();
        }

        public void AngryDeparture()
        {
            if (this.IfShouldAngryDeparture() == false)
            {
                throw new Exception($"Should not angry departure as the customer can still wait longer: currentTime = {Simulation.GlobalTime}, arrivalTime = {_arrivalTime}, maxToleranceTime = {_maxToleranceTime}");
            }
            this.LeaveQueue();
            this.DepatureCheckoutArea();
        }
    }
}
