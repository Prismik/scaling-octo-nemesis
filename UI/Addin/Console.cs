﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TTUI.Addin
{
    /// <summary>
    /// Adds a basic console to the game.
    /// </summary>
    public class Console
    {
        private int _height;

        public bool Enabled { get; private set; }
        InputField _input;
        public Console(SpriteFont font, int height = 120)
        {
            _height = height;
            _input = new InputField("", font, "console", 99, 
                new Vector2(0, _height), 
                new Vector2((int)Util.Resolution.VirtualViewport.Length(), font.MeasureString("A").Y));
            _input.Visible = false;
            _input.AddKeyHandler(delegate(object o, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter)
                {
                    string val = _input.Clear();
                }
            });
            InputSystem.KeyDown += HandleKeys;
        }

        public void Toggle()
        {
            Enabled = !Enabled;
            if (Enabled)
            {
                _input.Visible = true;
                _input.OnFocus();
                _input.Clear();
            }
            else
            {
                _input.Visible = false;
                _input.OnLostFocus();
                _input.Clear();
            }
        }

        public void HandleKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyModifiers == Modifiers.Control && e.KeyCode == Keys.I)
                Toggle();
        }

        public void Update(GameTime time)
        {
            if (Enabled)
            {
                _input.Update(time);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (Enabled)
            {
                DrawingTools.DrawRectangle(sb, 
                    new Rectangle(0, 0, (int)Util.Resolution.VirtualViewport.Length(), _height), 
                    new Color(0, 0, 0, 0.75f),
                    LayerDepths.POST_FRONT);
                _input.Draw(sb);
            } 
        }
    }
}

