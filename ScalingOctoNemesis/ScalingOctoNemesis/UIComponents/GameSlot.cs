using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScalingOctoNemesis.UI;

namespace ScalingOctoNemesis.UIComponents
{
    class GameSlot : UIContainer
    {
        public bool Open        { get; private set; }
        public bool Available   { get; private set; }

        ColorButton _colorButton;
        Button _teamButton;
        DropDown _closeChoice;
        DropDown _civChoice;
        Player _p = null;
        SpriteFont _f;

        int index = 0;
        public static Color[] Colors = new Color[8] { Color.Blue, Color.Red, Color.Yellow, Color.Brown, Color.Orange, Color.Green, Color.Gray, Color.Teal };
        public GameSlot(SpriteFont sf, Vector2 pos, Vector2 size)
            : base("id", pos, size)
        {
            _closeChoice = new DropDown("CloseDD", pos, new Vector2(80, 25), sf);
            _closeChoice.AddItem("Open");
            _closeChoice.AddItem("Closed");
            _civChoice = new DropDown("dropdown", pos, new Vector2(130, 25), sf);
            for (int i = 0; i != 4; ++i)
                _civChoice.AddItem("Object " + i.ToString());

            Open = true;
            Available = true;
            _f = sf;
        }

        public void Populate(Player player)
        {
            if (Available && Open)
            {
                _p = player;
                index = Colors.ToList<Color>().FindIndex(c => c == _p.Color);
                Available = false;
               // _closeChoice.Position =
                _civChoice.Position = Position + new Vector2(250, 10);
                _teamButton = new Button(_p.Team.ToString(), "teamButton", new Vector2(20,20), Position + new Vector2(450, 10), _f);
                _teamButton.Action = delegate {
                    _p.Team++;
                    if (_p.Team > 4)
                        _p.Team = 0;

                    _teamButton.Value = _p.Team.ToString();
                };
                _colorButton = new ColorButton("", "colorButton", new Vector2(20, 20), Position + new Vector2(550, 10), _f, false);
                _colorButton.Color = _p.Color;
                _colorButton.Action = delegate {
                    index++;
                    if (index > 7)
                        index = 0;

                    _colorButton.Color = _p.Color = Colors[index];
                };
            }
        }

        public void Leave()
        {
            _p = null;
            Available = true;
            _teamButton = null;
        }

        public override void Update(GameTime elapsedTime)
        {
            //base.Update(elapsedTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Available)
                if (Open)
                    sb.DrawString(_f, "Open", Position + new Vector2(10, 10), Color.Green, 0, Vector2.Zero, 1, SpriteEffects.None, LayerDepths.D1);
                else
                    sb.DrawString(_f, "Closed", Position + new Vector2(10, 10), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, LayerDepths.D1);
            else
            {
                sb.DrawString(_f, _p.Name, Position + new Vector2(10, 10), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, LayerDepths.D1);
                _civChoice.Draw(sb);
                // if (Player != self)
                //sb.DrawString(_f, _p.Civ, Position + new Vector2(100, 10), Color.Chocolate);
                _colorButton.Draw(sb);
                _teamButton.Draw(sb);
                // if (Player != self)
                // sb.DrawString(_f, _p.team.toString(), Position + new Vector(200, 10), Color.White);
            }
        }
    }
}
