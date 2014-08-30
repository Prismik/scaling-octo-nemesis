using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScalingOctoNemesis.UIComponents
{
    class TopBar : UIComponent
    {
        public TopBar(Vector2 pos, Vector2 size)
            : base("TopBar", pos, size, Vector2.Zero)
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, Position, Size, Color.DarkGray, LayerDepths.FRONT);
        }
    }
}
