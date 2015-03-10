using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParticleFilter;

namespace ParticleFilterDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            IParticleFilter<FeatureParticle> filter = new FeatureParticleFilter();
            filter.GenerateParticles( new List<FeatureParticle>(), 1000,  //particles' count
                                           FeatureParticle.FromArray, //convert arr => position (create from array)
                                           new List<IDistribution>()
                                           { 
                                               new UniformDistribution(320),
                                               new UniformDistribution(240)
                                           });
        }
    }
}
