using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis.Util
{
    public static class MathUtils
    {
        // A is the initial position, at time 0
        // B is the target position, to reach at time P
        // t is the current time, in the range 0..P
        public static float InterpolateLinear(float A, float B, float P, float t)
        {
            return MathHelper.Clamp(t, 0, P) * (B - A) / P + A;
        }

        // Damp is in the range 0..1, with a typical value of 0.1 (which means 90% correction in one second)
        // dt is the time delta for this iteration
        // B is the target
        // V will be updated to get closer to B
        public static void StepExponential(ref float v, float b, float damp, float dt)
        {
            v = b + (b - v) * (float)Math.Pow(damp, dt);
        }
    }
}
