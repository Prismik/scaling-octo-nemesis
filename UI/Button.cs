using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace TTUI
{
	public class Button : UIItem, IDisposable
	{
        bool _pressed = false;
        bool _handleHover = false;
        SpriteFont _font;

        public Action Action    { get; set; }
        public string Value     { get; set; }
        public bool Enabled     { get; set; }
        public Tooltip Tooltip  { get; set; }

        public Button(string value, string id, 
            Vector2 size, Vector2 position, SpriteFont font, bool handleHover = true)
            : base(id, position, size)
        {
            Initialize(font, value, handleHover);
        }
		
        public void Initialize(SpriteFont font, string value, bool handleHover)
        {
            Value = value;
            _font = font;
            _handleHover = handleHover;
            InputSystem.MouseDown += Press;
            InputSystem.MouseUp += Release;
            if (_handleHover)
                InputSystem.MouseMove += Move;
        }

        public void Dispose()
        {
            InputSystem.MouseDown  -= Press;
            InputSystem.MouseUp -= Release;
            if (_handleHover)
                InputSystem.MouseMove -= Move;
        }

		public virtual void Press(object o, MouseEventArgs args)
		{
            if (PointInComponent(args.X, args.Y))
                _pressed = true;
		}

        public virtual void Release(object o, MouseEventArgs args)
        {
            if (_pressed)
            {
                _pressed = false;
                if (PointInComponent(args.X, args.Y))
                    Action();
            }
        }

        public virtual void Move(object o, MouseEventArgs args)
        {
            if (PointInComponent(args.X, args.Y))
                Hover = true;
            else
                Hover = false;
        }

        public override void Update(GameTime timer)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            DrawInner(sb);
            DrawBorder(sb);
            DrawText(sb);
        }

        public virtual void DrawBorder(SpriteBatch sb)
        {
            DrawingTools.DrawEmptyRectangle(sb, Position, Size, Color.LightGray, 0.1f);
        }

        public virtual void DrawInner(SpriteBatch sb)
        {
            Color c = Color.DarkSlateGray;
            if (Hover)
                c = Color.Blue;

            DrawingTools.DrawRectangle(sb, Position, Size, c, 0.2f);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
            sb.DrawString(_font, Value, Position + new Vector2(5, 5), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.01f);
        }
	}
}