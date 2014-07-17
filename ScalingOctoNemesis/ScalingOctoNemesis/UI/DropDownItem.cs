using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScalingOctoNemesis.UI
{
    class DropDownItem
    {
        public Action Click { get; set; }
        Object _item = null;
        public bool Visible { get; set; }
        public Vector2 Position { get; set; }
        SpriteFont font;
        public DropDownItem(Object o, SpriteFont f)
        {
            _item = o;
            Visible = false;
            font = f;
        }

        public virtual void DrawText(SpriteBatch sb)
        {
            sb.DrawString(font, _item.ToString(), Position, Color.White);
        }

        public void Draw(SpriteBatch sb)
        {
            DrawText(sb);
        }

        public override string ToString()
        {
            return _item.ToString();
        }
    }
}
