using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimulationDemo.Logger;
using System;
using System.Windows.Forms;

namespace Simulation.Presentation
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddSingleton<Form1>();
               })
               .ConfigureLogging(logBuilder =>
               {
                   logBuilder.SetMinimumLevel(LogLevel.Trace);
                   logBuilder.AddLog4Net("log4net.config");

               });
            var host = builder.Build();

            using(var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var logger = services.GetRequiredService<ILogger<Form1>>();
                    SimLogger.Logger = logger;
                    
                    //var form1 = services.GetRequiredService<Form1>();
                    Application.Run(new Form1());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            //Application.Run(new Form1());
        }
    }
}
