using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Randomness
{
    public class RandomStrGenerator
    {
        public static string GetRandomString(int stringLength = 8)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }
    }
}
