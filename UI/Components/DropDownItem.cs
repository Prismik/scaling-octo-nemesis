using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI
{
    /// <summary>
    /// Graphical control element within a DropDown.
    /// </summary>
    public class DropDownItem: UIItem
    {
        SpriteFont _font;

        /// <summary>
        /// Gets the value held by this item.
        /// </summary>
        public Object Value { get; private set; }

        public Action Action { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this item is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                if (value != Visible)
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
        }

        public DropDownItem(Object o, SpriteFont f, Vector2 size)
            : base(o.ToString(), Vector2.Zero, Vector2.Zero)
        {
            Value = o;
            Visible = false;
            Size = size;
            _font = f;
            Hover = false;
        }

        public override void Press(object o, MouseEventArgs e)
        {
            if (PointInComponent(e.X, e.Y))
                Action();
        }

        public override void Release(object o, MouseEventArgs e)
        {
            //if (_pressed)
            //{
            //    _pressed = false;
            //    if (PointInComponent(args.X, args.Y))
            //        Action();
           // }
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
