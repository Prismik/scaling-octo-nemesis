using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI
{
    /// <summary>
    /// Graphical control element that provides the user a simple way to trigger an event.
    /// </summary>
	public class Button : UIItem, IDisposable
	{
        bool _pressed = false;
        SpriteFont _font;
        float _textSize;

        public Action Action   { get; set; }
        public string Value    { get; set; }
        public bool Enabled    { get; set; }
        public Tooltip Tooltip { get; set; }

        public Button(string value, string id, 
            Vector2 size, Vector2 position, SpriteFont font, bool handleHover = true)
            : base(id, position, size)
        {
            Initialize(font, value, handleHover);
        }
		
        public void Initialize(SpriteFont font, string value, bool handleHover)
        {
            Value = value;
            _textSize = font.MeasureString(value).X;
            _font = font;
        }

        public void Dispose()
        {
            InputSystem.MouseDown  -= Press;
            InputSystem.MouseUp -= Release;
            InputSystem.MouseMove -= Move;
        }

		public override void Press(object o, MouseEventArgs args)
		{
            if (PointInComponent(args.X, args.Y))
                _pressed = true;
		}

        public override void Release(object o, MouseEventArgs args)
        {
            if (_pressed)
            {
                _pressed = false;
                if (PointInComponent(args.X, args.Y))
                    Action();
            }
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

        }

        public virtual void DrawInner(SpriteBatch sb)
        { 
            Color c = FlatColors.MIDNIGHT_BLUE;
            if (Hover)
                c = FlatColors.WET_ASPHALT;

            DrawingTools.DrawRectangle(sb, Position, Size, c, LayerDepths.D3);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
            // TODO Center Y as well
            Vector2 middle = Position + new Vector2((Size.X - _textSize) / 2, 5);
            sb.DrawString(_font, Value, middle, FlatColors.SILVER, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }
	}
}