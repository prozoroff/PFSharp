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
        /// <summary>
        /// Generating initial particles
        /// </summary>
        /// <param name="numberOfParticles">max particles number</param>
        /// <param name="distributions">methods to distribute initial particles</param>
        void GenerateParticles(int numberOfParticles, IList<IDistribution> distributions);

        /// <summary>
        /// Resample partiles using their wights
        /// </summary>
        /// <param name="sampleCount">number of particles for resampling</param>
        /// <returns></returns>
        IList<TParticle> Resample(int sampleCount);

        /// <summary>
        /// Prediction state of particle filter
        /// </summary>
        /// <param name="effectiveCountMinRatio">threshold for resampling</param>
        void Predict(float effectiveCountMinRatio);

        /// <summary>
        /// Updating of particle filter using received measure
        /// </summary>
        /// <param name="measure">measure, cap</param>
        void Update(TParticle measure);


        /// <summary>
        /// Property to get particles
        /// </summary>
        IList<TParticle> Particles { get; }
    }
}
