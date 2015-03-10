using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    public class FeatureParticleFilter : IParticleFilter<FeatureParticle>
    {
        IList<FeatureParticle> _particles;

        public FeatureParticleFilter()
        {
            _particles = new List<FeatureParticle>();
        }

        public void GenerateParticles(int numberOfParticles, Func<double[], FeatureParticle> creator, IList<IDistribution> distributions)
        {
            if (_particles == null)
                throw new ArgumentNullException("The provided collection must not be null.");

            var nDim = distributions.Count;

            /**************** make particles *****************/
            for (int i = 0; i < numberOfParticles; i++)
            {
                var randomParam = new double[nDim];
                for (int dimIdx = 0; dimIdx < nDim; dimIdx++)
                {
                    randomParam[dimIdx] = distributions[dimIdx].Generate();
                }

                var p = creator(randomParam);
                p.Weight = 1d / numberOfParticles;

                _particles.Add(p);
            }
        }

        public void Resample(int sampleCount)
        {
            throw new NotImplementedException();
        }

        public void Predict(float effectiveCountMinRatio, int sampleCount)
        {
            List<FeatureParticle> newParticles = null;
            var effectiveCountRatio = (double)effectiveParticleCount(GetNormalizedWeights(_particles)) / _particles.Count;
            if (effectiveCountRatio > Single.Epsilon && //do not resample if all particle weights are zero
                effectiveCountRatio < effectiveCountMinRatio)
            {
                Resample(sampleCount);
            }
            else
            {
                newParticles = _particles
                               .Select(x => (FeatureParticle)x.Clone())
                               .ToList();
            }

            foreach (var p in newParticles)
            {
                p.Drift();
                p.Diffuse();
            }
        }

        private double effectiveParticleCount(IEnumerable<double> weights)
        {
            var sumSqr = weights.Sum(x => x * x) + Single.Epsilon;
            return /*1 if weights are normalized*/ weights.Sum() / sumSqr;
        }

        public IList<double> GetNormalizedWeights(IEnumerable<IParticle> particles)
        {
            List<double> normalizedWeights = new List<double>();

            var weightSum = particles.Sum(x => x.Weight) + Single.Epsilon;

            foreach (var p in particles)
            {
                var normalizedWeight = p.Weight / weightSum;
                normalizedWeights.Add(normalizedWeight);
            }

            return normalizedWeights;
        }

        void IParticleFilter<FeatureParticle>.Update()
        {
            throw new NotImplementedException();
        }
    }
}
