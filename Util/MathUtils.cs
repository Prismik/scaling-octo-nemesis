using System;
using Microsoft.Xna.Framework;

namespace TTUI.Util
{
    /// <summary>
    /// Provides some utility functions for math operations.
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Returns an interpolated linear.
        /// </summary>
        /// <returns>The linear.</returns>
        /// <param name="A">The initial position, at time 0</param>
        /// <param name="B">The target position, to reach at time P</param>
        /// <param name="P">The upper bound for t</param>
        /// <param name="t">the current time, in the range 0..P</param>
        public static float InterpolateLinear(float A, float B, float P, float t)
        {
            return MathHelper.Clamp(t, 0, P) * (B - A) / P + A;
        }

        /// <summary>
        /// Step in an exponential.
        /// </summary>
        /// <param name="v">Will be updated to get closer to B</param>
        /// <param name="b">The target</param>
        /// <param name="damp">In the range 0..1, with a typical value of 0.1 (which means 90% correction in one second)</param>
        /// <param name="dt">The time delta for this iteration</param>
        public static void StepExponential(ref float v, float b, float damp, float dt)
        {
            v = b + (b - v) * (float)Math.Pow(damp, dt);
        }
    }
}
