using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    public class FeatureParticleFilter : IParticleFilter<FeatureParticle>
    {
        List<FeatureParticle> _particles;

        public FeatureParticleFilter()
        {
            _particles = new List<FeatureParticle>();
        }

        public void GenerateParticles(List<FeatureParticle> collection, int numberOfParticles, Func<double[], FeatureParticle> creator, IList<IDistribution> distributions)
        {
            if (collection == null)
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

                collection.Add(p);
            }
        }

        void IParticleFilter<FeatureParticle>.Resample()
        {
            throw new NotImplementedException();
        }

        void IParticleFilter<FeatureParticle>.Predict()
        {
            throw new NotImplementedException();
        }

        void IParticleFilter<FeatureParticle>.Update()
        {
            throw new NotImplementedException();
        }
    }
}
