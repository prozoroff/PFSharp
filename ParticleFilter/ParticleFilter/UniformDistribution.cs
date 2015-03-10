using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    public class UniformDistribution : IDistribution
    {
        private RandomProportional _random;
        private int _maxValue;

        public UniformDistribution(int maxValue)
        {
            _maxValue = maxValue;
            _random = new RandomProportional(_maxValue);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public double Generate()
        {
            return _random.GenerateNext();
        }
    }
    
}
