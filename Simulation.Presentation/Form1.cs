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
        private readonly ILogger<Form1> _logger;

        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            _sim = new SimulationDemo.Simulation(numCashier: 5, numSelfChechout: 1, numMachine: 5);
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
            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync();
        }

        private void UpdateArrivalRate_Click(object sender, EventArgs e)
        {
            double arrPro = double.Parse(this.textBox1.Text);
            IDistribution dist = new Bernoulli(arrPro);
            DistributionHelper.UpdateDistribution(EventEnum.Arrival, dist);
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
    }
}
