using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis.UIComponents
{
    class GameSlot
    {
        public Vector2 Position { get; set; }
        Player p = null;
        bool open = true;
        bool available = true;
        SpriteFont f;
        public GameSlot(SpriteFont sf)
        {
            f = sf;
        }

        public void Populate(Player player)
        {
            p = player;
            available = false;
        }

        public void Leave()
        {
            p = null;
            available = true;
        }

        public void Draw(SpriteBatch sb)
        {
            if (available)
                if (open)
                    sb.DrawString(f, "Open", Position + new Vector2(10, 10), Color.Green);
                else
                    sb.DrawString(f, "Closed", Position + new Vector2(10, 10), Color.Red);
            else
            {
                sb.DrawString(f, p.Name, Position + new Vector2(10, 10), Color.White);
                sb.DrawString(f, p.Civ, Position + new Vector2(100, 10), Color.Chocolate);
                DrawingTools.DrawRectangle(sb, new Rectangle(200, (int)Position.X + 10, 10, 10), p.Color);
                sb.DrawString(f, p.Team.ToString(), Position + new Vector2(300, 10), Color.Chocolate);
            }
        }
    }
}
