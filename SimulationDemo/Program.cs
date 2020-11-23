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
        static System.Random rand = new System.Random();
        static Bernoulli arrival = new Bernoulli(0.5);
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appSettings.json", optional: true);

            Configuration = builder.Build();

            Console.WriteLine("#### Wating Line Similation ####");

            int totalarrival = 0;
            int totaldepartuer = 0;
            for (int i = 0; i < 10000; ++i)
            {
                //var interarrival = string.Empty;
                ////interarrival = Configuration.GetValue<string>("Interarrival");
                //using (StreamReader r = new StreamReader(AppContext.BaseDirectory + "appSettings.json"))
                //{
                //    string json = r.ReadToEnd();
                //    dynamic items = JsonConvert.DeserializeObject<dynamic>(json);
                //    interarrival = (string)items["Interarrival"];
                //}

                totalarrival += (int)arrival.Sample() * 1;
                totaldepartuer += (int)arrival.Sample() * 1;
                Console.WriteLine($"Unit time (iteration): {i}");
                Console.WriteLine($"Interarrival: {2}");
                Console.WriteLine($"Total customer arrived: {totalarrival}");
                Console.WriteLine($"Total customer departure: {(totaldepartuer > 20 ? totaldepartuer - 20 : 0)}");
                //Console.WriteLine($"Casher 1: {new string('*', rand.Next(1, 10))} ");
                //Console.WriteLine($"Casher 2: {new string('*', rand.Next(1, 10))} ");
                //Console.WriteLine($"Casher 3: {new string('*', rand.Next(1, 10))} ");
                //Console.WriteLine($"Casher 4s: {new string('*', rand.Next(1, 10))} ");
                Console.WriteLine($"Casher 1      [Opened]|{new string('*', rand.Next(1, 15))}");
                Console.WriteLine($"Casher 2      [Opened]|{new string('*', rand.Next(1, 15))}");
                Console.WriteLine($"Casher 3      [Opened]|{new string('*', rand.Next(1, 15))}");
                Console.WriteLine($"Casher 4      [Closed]|");
                Console.WriteLine($"Self-Checkout [Opened]|{new string('*', rand.Next(1, 15))}");
                //Console.WriteLine($"Casher 1: {rand.Next(1, 10)}");
                //Console.WriteLine($"Casher 2: {rand.Next(1, 10)}");
                //Console.WriteLine($"Casher 3: {rand.Next(1, 10)}");
                //Console.WriteLine($"Casher 4: {rand.Next(1, 10)}");
                Thread.Sleep(1000);

                Console.SetCursorPosition(0, Console.CursorTop - 9);
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");
                Console.WriteLine($"{new string(' ', 100)}");

                Console.SetCursorPosition(0, Console.CursorTop - 9);
                //Console.WriteLine("Over previous line!!!");

                
            }
        }
    }
}
