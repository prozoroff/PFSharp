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

        public FeatureParticle()
        {

        }

        public void Drift()
        {
            //we do not have velocity
        }

        public void Diffuse()
        {
            this.Position = new PointF
            {
                X = this.Position.X + (float)RandomProportional.NextDouble(_variance),
                Y = this.Position.Y + (float)RandomProportional.NextDouble(_variance)
            };
        }

        public static FeatureParticle FromArray(double[] arr)
        {
            return new FeatureParticle
            {
                Position = new PointF((float)arr[0], (float)arr[1]),
            };
        }

        public object Clone()
        {
            return new FeatureParticle
            {
                Position = this.Position,
                Weight = this.Weight
            };
        }
    }
}
