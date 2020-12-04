using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SimulationDemo.Randomness;
using System;
using System.IO;
using System.Threading;

namespace SimulationDemo
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appSettings.json", optional: true);
        }
    }
}
