using System;

namespace ParticleFilter
{
    // Summary:
    //     Common interface for probability distributions.
    //
    public interface IDistribution : ICloneable
    {
        double Generate();
    }
}
