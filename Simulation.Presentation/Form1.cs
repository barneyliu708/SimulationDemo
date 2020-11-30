﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Simulation.Logger;
using SimulationDemo;

namespace Simulation.Presentation
{
    public partial class Form1 : Form
    {
        private SimulationDemo.Simulation _sim;
        private BackgroundWorker backgroundWorker1;
        private readonly ILogger<Form1> _logger;

        public Form1(ILogger<Form1> logger)
        {
            _logger = logger;
            _logger.LogInformation("logging test");
            SimLogger.Info("Sim Logger Info");

            InitializeComponent();
            InitializeBackgroundWorker();
            _sim = new SimulationDemo.Simulation(2, 1, 5);
            _sim.Testfield = "test1";
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
            _logger.LogInformation("started");
            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _sim.Testfield = "test2";
        }
    }
}
