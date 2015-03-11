using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    /// <summary>
    /// Static class to get random double value 
    /// </summary>
    public static class RandomProportional
    {
        static Random random = new Random();

        /// <summary>
        /// Getting random double value from 0 to MaxValue
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static double NextDouble(int maxValue)
        {
            return random.NextDouble() * maxValue;
        }

        /// <summary>
        /// Getting random double value from MinValue to MaxValue
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static double NextDouble(int minValue, int maxValue)
        {
            var randomValue = random.NextDouble();
            return (maxValue - minValue) / 2 - randomValue * (maxValue - minValue);
        }
    }
}
