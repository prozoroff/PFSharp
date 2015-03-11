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
        void GenerateParticles(int numberOfParticles, Func<double[], TParticle> creator, IList<IDistribution> distributions);
        IList<TParticle> Resample(int sampleCount);
        void Predict(float effectiveCountMinRatio);
        void Update(TParticle measure);

        IList<TParticle> Particles { get; }
    }
}
