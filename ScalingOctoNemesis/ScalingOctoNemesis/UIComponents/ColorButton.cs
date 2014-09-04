using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis.UIComponents
{
    class ColorButton : Button
    {
        public Color Color { get; set; }
        public ColorButton(string value, string id, 
            float width,    float height,   // size
            float x,        float y,        // position
            SpriteFont font, bool handleHover = true) 
			: base(value, id, width, height, x, y, font, handleHover)
		{
            base.Initialize(font, value, handleHover);
            Color = Color.Black;
		}

        public ColorButton(string value, string id, 
            Vector2 size, Vector2 pos, SpriteFont font, bool handleHover = true)
            : base(value, id, size, pos, font, handleHover)
        {
            base.Initialize(font, value, handleHover);
            Color = Color.Black;
        }

        public override void DrawInner(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, Position, Size, Color, 0.2f);
        }
    }
}
