using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleFilter
{
    /// <summary>
    /// Common particle interface
    /// </summary>
    public interface IParticle : ICloneable
    {
        /// <summary>
        /// Particle's weight.
        /// </summary>
        double Weight { get; set; }

        /// <summary>
        /// Applies model transition without noise to a particle's state.
        /// </summary>
        void Drift();

        /// <summary>
        /// Applies noise to a particle's state.
        /// </summary>
        void Diffuse();
    }

    /// <summary>
    /// Particle interface defining common members for all particle instances.
    /// </summary>
    /// <typeparam name="TState">State type.</typeparam>
    public interface IParticle<TState> : IParticle
    {
        /// <summary>
        /// Gets or sets the particle's state.
        /// </summary>
        TState State { get; set; }
    }
}
