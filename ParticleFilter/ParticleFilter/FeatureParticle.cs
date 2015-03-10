using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    public class FeatureParticle : IParticle
    {
        public double Weight { get; set; }
        public PointF Position { get; set; }
        private int _variance = 10;//FIXME
        private RandomProportional random;

        public FeatureParticle()
        {
            random = new RandomProportional(_variance);
        }

        public void Drift()
        {
            //we do not have velocity
        }

        public void Diffuse()
        {
            this.Position = new PointF
            {
                X = this.Position.X + random.Next(),
                Y = this.Position.Y + random.Next(),
            };
        }

        public static FeatureParticle FromArray(double[] arr)
        {
            return new FeatureParticle
            {
                Position = new PointF((float)arr[0], (float)arr[1]),
            };
        }

        object ICloneable.Clone()
        {
            return new FeatureParticle
            {
                Position = this.Position,
                Weight = this.Weight
            };
        }
    }
}
