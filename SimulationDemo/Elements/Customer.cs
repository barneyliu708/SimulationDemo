using SimulationDemo.Enums;
using SimulationDemo.Randomness;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Elements
{
    public class Customer
    {
        private int _arrivalTime;
        private int _maxToleranceTime;
        private int _scanAndPackingTime;
        private int _makePaymentTime;
        private EventEnum _amountItems;
        private int _startCheckoutTime;

        private BaseQueue _joinedQueue;
        private CheckoutArea _checkoutArea;

        public Customer(int arrivalTime)
        {
            _arrivalTime = arrivalTime;
            _amountItems = (EventEnum)DistributionHelper.GetDistribution(EventEnum.BuyingItems).Sample();
            _scanAndPackingTime = (int)DistributionHelper.GetDistribution(_amountItems).Sample();
            _makePaymentTime = (int)DistributionHelper.GetDistribution(EventEnum.MakingPayment).Sample();
            _maxToleranceTime = (int)DistributionHelper.GetDistribution(EventEnum.AngryDeparture).Sample();
        }

        public bool ShouldAngryDeparture()
        {
            return Simulation.GlobalTime >= _arrivalTime + _maxToleranceTime;
        }

        public bool IsCheckoutFinished()
        {
            return Simulation.GlobalTime >= _startCheckoutTime + _scanAndPackingTime + _makePaymentTime;
        }

        public bool IsCheckoutStarted()
        {
            return _startCheckoutTime != 0;
        }

        public bool ShouldChangeLine()
        {
            if (this.IsCheckoutStarted())
            {
                return false; // cannot change line anymore if the customer already start checking out.
            }
            return _checkoutArea.QuickestQueue() != _joinedQueue; // if current joined queue is not the quickest queue in the check out area, then should change
        }

        public void ArriveCheckoutArea(CheckoutArea checkoutArea)
        {
            _checkoutArea = checkoutArea;
        }

        public void DepatureCheckoutArea()
        {
            _checkoutArea = null;
        }

        public void JoinQueue(BaseQueue queue)
        {
            _joinedQueue = queue;
            _joinedQueue.NewCustomersJoins(this);
        }

        public void LeaveQueue()
        {
            if (_joinedQueue == null)
            {
                throw new Exception($"The customer has not join any queue yet");
            }
            _joinedQueue.CustomerLeaves(this);
            _joinedQueue = null;
        }

        public void StartCheckout()
        {
            _startCheckoutTime = Simulation.GlobalTime;
        }

        public void DepartureAfterCheckout()
        {
            this.LeaveQueue();
            this.DepatureCheckoutArea();
        }

        public void AngryDeparture()
        {
            this.LeaveQueue();
            this.DepatureCheckoutArea();
        }
    }
}
