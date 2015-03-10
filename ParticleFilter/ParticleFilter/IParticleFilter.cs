using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    /// <summary>
    /// Common particle filter interface
    /// </summary>
    public interface IParticleFilter<TParticle>
    {
        void GenerateParticles(List<TParticle> collection, int numberOfParticles, Func<double[], TParticle> creator, IList<IDistribution> distributions);
        void Resample();
        void Predict();
        void Update();
    }
}
