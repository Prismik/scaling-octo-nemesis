using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TTUI.Util
{
    /// <summary> 
    /// Handles delay on actions.
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
                    if (!o.Canceled)
                        o.ops();
                    
                    _opsDone.Add(o);
                }
            }

            foreach (DelayOps o in _opsDone)
                _ops.Remove(o);

            _opsDone.Clear();
        }
    }

    /// <summary>
    /// An operation to be called after a certain treshold.
    /// </summary>
    public class DelayOps: Cancelable
    {
        /// <summary>
        /// Action called after the treshold is reached.
        /// </summary>
        public Action ops;

        /// <summary>
        /// Timer to start the countdown.
        /// </summary>
        public Timer timer;

        /// <summary>
        /// Total time in milliseconds before the action is called.
        /// </summary>
        public int treshold;

        public bool Canceled { get; private set; }

        public DelayOps(Action a, Timer t, int max)
        {
            ops = a;
            timer = t;
            treshold = max;
            Canceled = false;
        }

        public void Cancel()
        {
            Canceled = true;
        }
    }
}
