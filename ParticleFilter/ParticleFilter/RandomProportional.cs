using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    public class RandomProportional : Random
    {
        private int _maxValue;

        public RandomProportional(int maxValue)
        {
            _maxValue = maxValue;
        }

        // The Sample method generates a distribution proportional to the value  
        // of the random numbers, in the range [0.0, 1.0]. 
        protected override double Sample()
        {
            return Math.Sqrt(base.Sample());
        }

        public double GenerateNext()
        {
            return (Sample() * _maxValue);
        }
    }
}
