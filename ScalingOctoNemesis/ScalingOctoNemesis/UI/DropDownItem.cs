using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScalingOctoNemesis.UI
{
    public class DropDownItem: UIItem, IDisposable
    {
        public Action Action { get; set; }
        Object _item = null;
        SpriteFont font;
        public bool Hover { get; set; }
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                if (Visible)
                {
                    InputSystem.MouseDown += Press;
                    InputSystem.MouseMove += Move;
                }
                else
                {
                    InputSystem.MouseDown -= Press;
                    InputSystem.MouseMove -= Move;
                }
            }
        }

        public DropDownItem(Object o, SpriteFont f)
            : base("id", Vector2.Zero, Vector2.Zero, Vector2.Zero)
        {
            _item = o;
            Size = f.MeasureString(o.ToString());
            Visible = false;
            font = f;
            Hover = false;
        }

        public void Dispose()
        {
            InputSystem.MouseDown -= Press;
            InputSystem.MouseMove -= Move;
        }

        public virtual void Press(object o, MouseEventArgs args)
        {
            if (PointInComponent(args.X, args.Y))
                Action();
        }

        public virtual void Release(object o, MouseEventArgs args)
        {
            //if (_pressed)
            //{
            //    _pressed = false;
            //    if (PointInComponent(args.X, args.Y))
            //        Action();
           // }
        }

        public virtual void Move(object o, MouseEventArgs args)
        {
            if (PointInComponent(args.X, args.Y))
                Hover = true;
            else
                Hover = false;
        }

        public virtual void DrawText(SpriteBatch sb)
        {
            sb.DrawString(font, _item.ToString(), Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
         //   throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Visible)
            {
                if (Hover)
                    DrawingTools.DrawRectangle(sb, Position, Size, Color.Black);
                
                DrawText(sb);
            }
        }

        public override string ToString()
        {
            return _item.ToString();
        }
    }
}
