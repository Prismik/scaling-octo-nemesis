﻿using System;
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

        Button _teamButton;
        DropDown _closeChoice;
        DropDown _civChoice;
        Player _p = null;
        SpriteFont _f;
        public GameSlot(SpriteFont sf, Vector2 pos, Vector2 size, Vector2 padding)
            : base("id", pos, size, padding)
        {
            _closeChoice = new DropDown("CloseDD", pos, new Vector2(80, 25), Vector2.Zero, sf);
            _closeChoice.AddItem("Open");
            _closeChoice.AddItem("Closed");
            _civChoice = new DropDown("dropdown", pos, new Vector2(130, 25), Vector2.Zero, sf);
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
                Available = false;
               // _closeChoice.Position =
                _civChoice.Position = Position + new Vector2(100, 10); 
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

        public override void Update(GameTime elapsedTime)
        {
            //base.Update(elapsedTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Available)
                if (Open)
                    sb.DrawString(_f, "Open", Position + new Vector2(10, 10), Color.Green, 0, Vector2.Zero, 1, SpriteEffects.None, 0.01f);
                else
                    sb.DrawString(_f, "Closed", Position + new Vector2(10, 10), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.01f);
            else
            {
                sb.DrawString(_f, _p.Name, Position + new Vector2(10, 10), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.01f);
                _civChoice.Draw(sb);
                // if (Player != self)
                //sb.DrawString(_f, _p.Civ, Position + new Vector2(100, 10), Color.Chocolate);
                DrawingTools.DrawRectangle(sb, new Rectangle(250, (int)Position.Y + 10, 20, 20), _p.Color, 0.01f);
                _teamButton.Draw(sb);
                // if (Player != self)
                // sb.DrawString(_f, _p.team.toString(), Position + new Vector(200, 10), Color.White);
            }
        }
    }
}
