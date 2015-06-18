using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI
{
    public class DropDownItem: UIItem, IDisposable
    {
        public Object Value { get; private set; }
        SpriteFont _font;

        public Action Action    { get; set; }
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
            : base(o.ToString(), Vector2.Zero, Vector2.Zero)
        {
            Value = o;
            Size = f.MeasureString(o.ToString());
            Visible = false;
            _font = f;
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
            sb.DrawString(_font, Value.ToString(), Position, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, LayerDepths.FRONT);
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
                    DrawingTools.DrawRectangle(sb, Position, Size, Color.Black, LayerDepths.POST_FRONT);
                else
                    DrawingTools.DrawRectangle(sb, Position, Size, Color.DarkSlateGray, LayerDepths.POST_FRONT);
                
                DrawText(sb);
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
