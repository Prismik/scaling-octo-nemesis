﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalingOctoNemesis.UI
{
    public class Tooltip : UIItem
    {
        public Tooltip(string id, float x, float y, float width, float height)
            : base(id, x, y, width, height)
        { 
        
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            throw new NotImplementedException();
        }
    }
}
