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
        private int _needHelpTime;
        private EventEnum _amountItems;
        private int _startCheckoutTime;

        private IQueue _joinedQueue;
        private CheckoutArea _checkoutArea;
        private bool _isNeedHelpForSelfCheckout;

        public Customer(int arrivalTime)
        {
            _arrivalTime = arrivalTime;
            _amountItems = (EventEnum)DistributionHelper.GetDistribution(EventEnum.BuyingItems).Sample();
            _scanAndPackingTime = (int)DistributionHelper.GetDistribution(_amountItems).Sample();
            _makePaymentTime = (int)DistributionHelper.GetDistribution(EventEnum.MakingPayment).Sample();
            _maxToleranceTime = (int)DistributionHelper.GetDistribution(EventEnum.AngryDeparture).Sample();
            _needHelpTime = (int)DistributionHelper.GetDistribution(EventEnum.FixingMachineError).Sample();
        }

        public bool ShouldAngryDeparture()
        {
            return Simulation.GlobalTime >= _arrivalTime + _maxToleranceTime;
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

        public bool ShouldChangeLine()
        {
            if (this.IsCheckoutStarted())
            {
                return false; // cannot change line anymore if the customer already start checking out.
            }
            return _checkoutArea.QuickestQueue() != _joinedQueue; // if current joined queue is not the quickest queue in the check out area, then should change
        }

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
            if (this.IsCheckoutFinished() == false)
            {
                throw new Exception($"Cannot departure since checkout is not finished yet");
            }
            this.LeaveQueue();
            this.DepatureCheckoutArea();
        }

        public void AngryDeparture()
        {
            if (this.ShouldAngryDeparture() == false)
            {
                throw new Exception($"Should not angry departure as the customer can still wait longer: currentTime = {Simulation.GlobalTime}, arrivalTime = {_arrivalTime}, maxToleranceTime = {_maxToleranceTime}");
            }
            this.LeaveQueue();
            this.DepatureCheckoutArea();
        }
    }
}
