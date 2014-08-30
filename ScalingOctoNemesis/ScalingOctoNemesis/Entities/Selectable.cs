using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis.Entities
{
    public abstract class Selectable : Clickable
    {
        public Vector2 Position { get; set; }
        public bool Selected { get; private set; }
        public abstract void Click(MouseButton button);

        public virtual void Select()
        {
            Selected = true;
        }

        public virtual void Release()
        {
            Selected = false;
        }

        public abstract void Update(GameTime time);
    }
}
