using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using SimulationDemo.Enums;
using SimulationDemo.Logger;
using SimulationDemo.Randomness;

namespace Simulation.Presentation
{
    public partial class Form1 : Form
    {
        private SimulationDemo.Simulation _sim;
        private BackgroundWorker backgroundWorker1;

        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            this.numOfCashierTextBox.Text = "10";
            this.numOfSelfCheckoutTextBox.Text = "1";
            this.numOfSelfCheckMachineTextBox.Text = "5";
            this.arrivalRateTextBox.Text = "0.5";
            this.maxIterationTextBox.Text = "MAX";
        }
        private void InitializeBackgroundWorker()
        {
            this.backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
        }

        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the
            // RunWorkerCompleted eventhandler.
            _sim.Execute();
            e.Result = "OK";
        }

        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            int numOfCashier = 0;
            int numOfSelfCheckout = 0;
            int numOfMachines = 0;
            double arrPro = 0;

            if (int.TryParse(this.numOfCashierTextBox.Text, out numOfCashier) &&
                int.TryParse(this.numOfSelfCheckoutTextBox.Text, out numOfSelfCheckout) &&
                int.TryParse(this.numOfSelfCheckMachineTextBox.Text, out numOfMachines) &&
                double.TryParse(this.arrivalRateTextBox.Text, out arrPro))
            {
                int maxIteration;
                if (int.TryParse(this.maxIterationTextBox.Text, out maxIteration) && maxIteration > 0)
                {
                    _sim = new SimulationDemo.Simulation(numCashier: numOfCashier, numSelfChechout: numOfSelfCheckout, numMachine: numOfMachines, maxIteration: maxIteration);
                }
                else
                {
                    _sim = new SimulationDemo.Simulation(numCashier: numOfCashier, numSelfChechout: numOfSelfCheckout, numMachine: numOfMachines);
                }

                IDistribution dist = new Poison(arrPro);
                DistributionHelper.UpdateDistribution(EventEnum.Arrival, dist);

                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
                this.startbutton.Enabled = false;
                this.numOfCashierTextBox.Enabled = false;
                this.numOfSelfCheckoutTextBox.Enabled = false;
                this.numOfSelfCheckMachineTextBox.Enabled = false;

                this.startbutton.Text = "Started";
            }
        }

        private void UpdateArrivalRate_Click(object sender, EventArgs e)
        {
            double arrPro = 0;
            if (double.TryParse(this.arrivalRateTextBox.Text, out arrPro)) 
            {
                IDistribution dist = new Poison(arrPro);
                DistributionHelper.UpdateDistribution(EventEnum.Arrival, dist);
            };
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddOneCashier_Click(object sender, EventArgs e)
        {
            _sim.CheckoutArea.AddOneNewCashier();
        }

        private void CloseOneCashier_Click(object sender, EventArgs e)
        {
            _sim.CheckoutArea.CloseOneCashier();
        }

        private void SpeedUp_Click(object sender, EventArgs e)
        {
            _sim.SpeedUp();
        }

        private void SlowDown_Click(object sender, EventArgs e)
        {
            _sim.SlowDown();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _sim.Stop();
            this.stopButton.Enabled = false;
        }
    }
}
