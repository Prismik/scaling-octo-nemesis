using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TTUI.Skins
{
    public class Skin
    {
        public Skin()
        {
            State = SkinStates.ENABLED;
        }

        public SkinStates State { get; internal set; }

        public virtual void Update(GameTime elapsedTime)
        {

        }

        public virtual void Draw(SpriteBatch sb)
        {

        }
    }
}

