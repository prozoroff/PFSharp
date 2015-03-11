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

        public IList<FeatureParticle> Particles
        {
            get
            {
                return _particles;
            }
        }

        public void GenerateParticles(int numberOfParticles, IList<IDistribution> distributions)
        {
            if (_particles == null)
                throw new ArgumentNullException("The provided collection must not be null.");

            var nDim = distributions.Count;

            for (int i = 0; i < numberOfParticles; i++)
            {
                var randomParam = new double[nDim];
                for (int dimIdx = 0; dimIdx < nDim; dimIdx++)
                {
                    randomParam[dimIdx] = distributions[dimIdx].Generate();
                }

                var p = FeatureParticle.FromArray(randomParam);
                p.Weight = 1d / numberOfParticles;

                _particles.Add(p);
            }
        }

        public IList<FeatureParticle> Resample(int sampleCount)
        {
            var resampledParticles = new List<FeatureParticle>(sampleCount);

            var filteredParticles = _filterParticles(_particles.Count);
            foreach (var dP in filteredParticles)
            {
                var newP = (FeatureParticle)dP.Clone();
                newP.Weight = 1d / _particles.Count;
                resampledParticles.Add(newP);
            }

            return resampledParticles;
        }

        private IList<FeatureParticle> _filterParticles(int sampleCount)
        {
            double[] cumulativeWeights = new double[_particles.Count];
            
            int cumSumIdx = 0;
            double cumSum = 0;
            foreach (var p in _particles)
            {
                cumSum += p.Weight;
                cumulativeWeights[cumSumIdx++] = cumSum;
            }

            var maxCumWeight = cumulativeWeights[_particles.Count - 1];
            var minCumWeight = cumulativeWeights[0];

            var filteredParticles = new List<FeatureParticle>();

            double initialWeight = 1d / _particles.Count;
            
            for (int i = 0; i < sampleCount; i++)
            {
                var randWeight = minCumWeight + RandomProportional.NextDouble(1) * (maxCumWeight - minCumWeight);
            
                int particleIdx = 0;
                while (cumulativeWeights[particleIdx] < randWeight) 
                {
                    particleIdx++;
                }
            
                var p = _particles[particleIdx];
                filteredParticles.Add(p);
            }

            return filteredParticles;
        }

        public void Predict(float effectiveCountMinRatio)
        {
            List<FeatureParticle> newParticles = null;
            var effectiveCountRatio = (double)_effectiveParticleCount(GetNormalizedWeights(_particles)) / _particles.Count;
            if (effectiveCountRatio > Single.Epsilon && 
                effectiveCountRatio < effectiveCountMinRatio)
            {
                newParticles = Resample(_particles.Count).ToList();
            }
            else
            {
                newParticles = _particles
                               .Select(x => (FeatureParticle)x.Clone())
                               .ToList();
            }

            foreach (var p in newParticles)
            {
                p.Diffuse();
            }
            _particles = new List<FeatureParticle>(newParticles);
        }

        private double _effectiveParticleCount(IEnumerable<double> weights)
        {
            var sumSqr = weights.Sum(x => x * x) + Single.Epsilon;
            return weights.Sum() / sumSqr;
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

        public void Update(FeatureParticle measure)
        {
            foreach (var p in _particles)
            {
                var dX = p.Position.X - measure.Position.X;
                var dY = p.Position.Y - measure.Position.Y;
                p.Weight = 1 / (Math.Sqrt(dX * dX + dY * dY));
            }
        }
    }
}
