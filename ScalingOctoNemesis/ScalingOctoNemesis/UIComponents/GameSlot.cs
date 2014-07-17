using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScalingOctoNemesis.UI;

namespace ScalingOctoNemesis.UIComponents
{
    class GameSlot
    {
        public bool Open        { get; private set; }
        public bool Available   { get; private set; }
        public Vector2 Position { get; set; }

        Button _teamButton;
        Player _p = null;
        SpriteFont _f;
        public GameSlot(SpriteFont sf)
        {
            Open = true;
            Available = true;
            _f = sf;
        }

        public void Populate(Player player)
        {
            if (Available && Open)
            {
                _p = player;
                Available = false;
                _teamButton = new Button(_p.Team.ToString(), "teamButton", new Vector2(20,20), Position + new Vector2(300, 10), new Vector2(5, 5), _f);
                _teamButton.Action = delegate {
                    _p.Team++;
                    if (_p.Team > 4)
                        _p.Team = 0;

                    _teamButton.Value = _p.Team.ToString();
                };
            }
        }

        public void Leave()
        {
            _p = null;
            Available = true;
            _teamButton = null;
        }

        public void Draw(SpriteBatch sb)
        {
            if (Available)
                if (Open)
                    sb.DrawString(_f, "Open", Position + new Vector2(10, 10), Color.Green);
                else
                    sb.DrawString(_f, "Closed", Position + new Vector2(10, 10), Color.Red);
            else
            {
                sb.DrawString(_f, _p.Name, Position + new Vector2(10, 10), Color.White);
                sb.DrawString(_f, _p.Civ, Position + new Vector2(100, 10), Color.Chocolate);
                DrawingTools.DrawRectangle(sb, new Rectangle(200, (int)Position.X + 10, 10, 10), _p.Color);
                _teamButton.Draw(sb);
                //sb.DrawString(_f, _p.Team.ToString(), Position + new Vector2(300, 10), Color.Chocolate);
            }
        }
    }
}
