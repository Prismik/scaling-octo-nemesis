using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis.Util
{
    /// <summary> 
    /// Handles delay on actions. Enable to call a function after a given time.
    /// </summary>
    public static class Delay
    {
        static List<DelayOps> _ops = new List<DelayOps>();
        static List<DelayOps> _opsDone = new List<DelayOps>();
        /// <summary>
        /// Adds an operation to be called after a delay.
        /// </summary>
        /// <param name="ops">Contains the action, the timer and the treshold after which to call the action.</param>
        public static void AddOps(DelayOps ops)
        {
            _ops.Add(ops);
        }

        /// <summary>
        /// Calls the delay actions when required.
        /// </summary>
        /// <param name="time">The game time.</param>
        public static void Update(GameTime time)
        {
            foreach (DelayOps o in _ops)
            {
                o.timer.Update(time.ElapsedGameTime.TotalMilliseconds);
                if (o.timer.Time >= o.treshold)
                {
                    o.ops();
                    _opsDone.Add(o);
                }
            }

            foreach (DelayOps o in _opsDone)
                _ops.Remove(o);

            _opsDone.Clear();
        }
    }

    public struct DelayOps
    {
        public Action ops;
        public Timer timer;
        public int treshold;

        public DelayOps(Action a, Timer t, int max)
        {
            ops = a;
            timer = t;
            treshold = max;
        }
    }
}
