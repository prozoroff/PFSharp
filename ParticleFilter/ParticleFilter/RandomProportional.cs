using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    public static class RandomProportional
    {
        static Random random = new Random();

        public static double NextDouble(int maxValue)
        {
            return random.NextDouble() * maxValue;
        }
    }
}
