﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationDemo.Randomness
{
    public interface IDistribution
    {
        object Sample();
    }
}
