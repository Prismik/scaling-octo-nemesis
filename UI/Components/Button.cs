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
        public float TextSize { get; private set; }

        /// <summary>
        /// Gets or sets the action triggered when the button is pressed.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Gets or sets the value shown within the button.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this button is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }

        public Texture2D Image { get; set; }

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
            TextSize = font.MeasureString(value).X;
            _font = font;
            Action = delegate { };
        }

        public override void Press(object o, MouseEventArgs e)
        {
            base.Press(o, e);
            if (PointInComponent(e.X, e.Y))
                _pressed = true;
        }

        public override void Release(object o, MouseEventArgs e)
        {
            if (_pressed)
            {
                if (Hover)
                    State = ComponentState.HOVER;
                else
                    State = State != ComponentState.DISABLED ? ComponentState.ENABLED : ComponentState.DISABLED;
                
                _pressed = false;
                if (PointInComponent(e.X, e.Y))
                    Action();
                
            }
        }
            
        public override void Update(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// Draws the Button with it's default look and feel. Override if you want to draw your own button.
        /// </summary>
        /// <param name="sb">The spritebatch.</param>
        public override void Draw(SpriteBatch sb)
        {
            DrawButton(sb);
            DrawText(sb);
        }

        private void DrawButton(SpriteBatch sb)
        { 
            if (Image != null)
            {
                sb.Draw(Image, Position, Color.White);
            }
            else
            {
                Color c = FlatColors.MIDNIGHT_BLUE;
                if (State == ComponentState.HOVER)
                    c = FlatColors.WET_ASPHALT;

                DrawingTools.DrawRectangle(sb, Position, Size, c, LayerDepths.D3);
            }
        }

        private void DrawText(SpriteBatch sb)
        {
            // TODO Center Y as well
            Vector2 middle = Position + new Vector2((Size.X - TextSize) / 2, 5);
            sb.DrawString(_font, Value, middle, FlatColors.SILVER, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }
    }
}