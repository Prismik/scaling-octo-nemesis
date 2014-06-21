using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalingOctoNemesis.Util
{
    public class Timer
    {
        bool _paused = false;
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

        public void Reset()
        {
            Time = 0;
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }
    }
}
