using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI
{
    /// <summary>
    /// Graphical control element that provides the user a simple way to trigger an event.
    /// </summary>
    public class Button : UIItem
    {
        bool _pressed = false;
        SpriteFont _font;
        float _textSize;

        /// <summary>
        /// Gets or sets the action triggered when the button is pressed.
        /// </summary>
        public Action Action   { get; set; }

        /// <summary>
        /// Gets or sets the value shown within the button.
        /// </summary>
        public string Value    { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this button is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
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

        public override void Press(object o, MouseEventArgs e)
        {
            if (PointInComponent(e.X, e.Y))
                _pressed = true;
        }

        public override void Release(object o, MouseEventArgs e)
        {
            if (_pressed)
            {
                _pressed = false;
                if (PointInComponent(e.X, e.Y))
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