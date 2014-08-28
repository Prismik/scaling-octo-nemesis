using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Util;
using ScalingOctoNemesis.Util;

namespace ScalingOctoNemesis.UI.Menus
{
    class ColoredMenuNode : MenuNode
    {
        string _text;
        SpriteFont _font; 
        
        public Color Color { get; set; }
        public ColoredMenuNode(string id, string text, Action action, Vector2 position, SpriteFont font)
            :base(id, action, position)
        {
            _text = text;
            Color = Color.White;
            _font = font;
            Size = _font.MeasureString(_text);
            Visible = true;
        }

        internal override void OnToggleActive(bool value)
        {
            Color = (value == true ? Color.Red : Color.White);
        }

        public override Vector2 GetSize()
        {
            return Vector2.Multiply(_font.MeasureString(_text), Scale);
        }

        public override void LeaveContext(float val, int pos)
        {
            float x = Position.X;
            MathUtils.StepExponential(ref x, 0f - GetSize().X, 0.000001f, 1f);
            Position -= new Vector2(Math.Max(Math.Abs(val) - pos, 0), 0);
            //Math.Max(x, 0), Position.Y);
                //
        }


        public override void EnterContext(float val, int pos)
        {
            Position += new Vector2(Math.Abs(val), 0);
        }

        public override bool EnterContextDone()
        {
            return true;
        }

        public override bool LeaveContextDone()
        {
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                float a = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 5) / 2 + 0.5f;
                float r = Color.R;
                float g = Color.G;
                float b = Color.B;

                Color = new Color(a, g, b, a);
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Visible)
                sb.DrawString(Menu.Font, _text, Position, Color, 0, Vector2.One, Scale, SpriteEffects.None, LayerDepths.FRONT);
        }
    }
}
