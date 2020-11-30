using Microsoft.Extensions.Logging;
using System;

namespace Simulation.Logger
{
    public static class SimLogger
    {
        public static ILogger Logger;

        public static void Info(string info)
        {
            Logger.LogInformation(info);
        }
    }
}
