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
            var resampledParticles = new List<FeatureParticle>(sampleCount);

            var drawnParticles = Draw(_particles.Count);
            foreach (var dP in drawnParticles)
            {
                var newP = (FeatureParticle)dP.Clone();
                newP.Weight = 1d / _particles.Count;
                resampledParticles.Add(newP);
            }
        }

        IList<FeatureParticle> Draw(int sampleCount)
        {
            /*************** calculate cumulative weights ****************/
            double[] cumulativeWeights = new double[_particles.Count];

            int cumSumIdx = 0;
            double cumSum = 0;
            foreach (var p in _particles)
            {
                cumSum += p.Weight;
                cumulativeWeights[cumSumIdx++] = cumSum;
            }
            /*************** calculate cumulative weights ****************/

            /*************** re-sample particles ****************/
            var maxCumWeight = cumulativeWeights[_particles.Count - 1];
            var minCumWeight = cumulativeWeights[0];

            var drawnParticles = new List<FeatureParticle>();
            double initialWeight = 1d / _particles.Count;

            Random rand = new Random();

            for (int i = 0; i < sampleCount; i++)
            {
                var randWeight = minCumWeight + rand.NextDouble() * (maxCumWeight - minCumWeight);

                int particleIdx = 0;
                while (cumulativeWeights[particleIdx] < randWeight) //find particle's index
                {
                    particleIdx++;
                }

                var p = _particles[particleIdx];
                drawnParticles.Add(p);
            }
            /*************** re-sample particles ****************/

            return drawnParticles;
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
            _particles = new List<FeatureParticle>(newParticles);
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
