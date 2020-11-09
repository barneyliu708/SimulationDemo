using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Random
{
    public interface IDistribution
    {
        object Sample();
    }
}
