using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalingOctoNemesis.Util
{
    /// <summary>
    /// Simple timer with an inner value that records elapsed time.
    /// </summary>
    public class Timer
    {
        bool _paused = false;

        /// <summary>
        /// Current timestamp of the timer.
        /// </summary>
        public double Time { get; private set; }
        public Timer()
        {
            Time = 0;
        }

        public void Update(double elapsed)
        {
            if (!_paused)
                Time += elapsed;
        }

        /// <summary>
        /// Sets the time value to 0.
        /// </summary>
        public void Reset()
        {
            Time = 0;
        }

        /// <summary>
        /// Pauses the timer, making the update function to no longer update the time value.
        /// </summary>
        public void Pause()
        {
            _paused = true;
        }

        /// <summary>
        /// Resumes the timer, making the update function to update the time value again.
        /// </summary>
        public void Resume()
        {
            _paused = false;
        }
    }
}
