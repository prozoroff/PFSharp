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
        void Resample(int sampleCount);
        void Predict(float effectiveCountMinRatio, int sampleCount);
        void Update();

        IList<FeatureParticle> Particles { get; }
    }
}
