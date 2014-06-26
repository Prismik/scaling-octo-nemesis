using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace ScalingOctoNemesis.UI
{
	public class DropDown : UIItem
	{
        List<Object> _objects = new List<Object>();
        SpriteFont _font;
        public Object Selected { get; }
		Action OnSelect { get; set; }
        public bool Expanded { get; set; }

        Rectangle _expandRectangle;
        public DropDown(string id, Vector2 pos, Vector2 size, Vector2 padding, SpriteFont font)
            : base(id, pos, size, padding)
        {
            _font = font;
            Initialize();
        }

		public DropDown(string id, float x, float y, float width, float height, float paddingX, float paddingY, SpriteFont font)
			: base(id, x, y, width, height, paddingX, paddingY)
		{
            _font = font;
            Initialize();
		}

        private void Initialize()
        {
            _expandRectangle = new Rectangle((int)Position.X + (int)Size.X + (int)Padding.X - 20, (int)Position.Y, 20 + (int)Padding.X, (int)Size.Y + (int)Padding.Y * 2);
        }

        public void OnFocus()
        {
            if (!Focused)
            {
                Focused = true;
            }
        }

        public void OnLostFocus()
        {
            if (Focused)
            {

            }
        }

        private void HandleClicks(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                //if (_expandRectangle.Contains(e.X, e.Y))
                // do stuff
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            DrawBorder(sb);
            DrawBackground(sb);
            DrawSelectedItem(sb);
            DrawExpandButton(sb);
            if (Expanded)
                DrawExpandedList(sb);
        }

        public virtual void DrawBorder(SpriteBatch sb)
        {
            Vector2 topLeft = new Vector2(Position.X, Position.Y);
            Vector2 topRight = new Vector2(Position.X + Size.X + Padding.X * 2, Position.Y);
            Vector2 bottomRight = new Vector2(Position.X + Size.X + Padding.X * 2, Position.Y + Size.Y + Padding.Y * 2);
            Vector2 bottomLeft = new Vector2(Position.X, Position.Y + Size.Y + Padding.Y * 2);
            DrawingTools.DrawLine(sb, topLeft, topRight, Color.LightGray);
            DrawingTools.DrawLine(sb, topRight, bottomRight, Color.LightGray);
            DrawingTools.DrawLine(sb, bottomRight, bottomLeft, Color.LightGray);
            DrawingTools.DrawLine(sb, bottomLeft, topLeft, Color.LightGray);
        }

        public virtual void DrawBackground(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb,
                new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X + (int)Padding.X * 2, (int)Size.Y + (int)Padding.Y * 2), Color.DarkSlateGray);
        }

        public virtual void DrawSelectedItem(SpriteBatch sb)
        {
            sb.DrawString(_font, Selected.ToString(), Position + Padding, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        public virtual void DrawExpandButton(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, _expandRectangle, Color.Coral);
        }

        public virtual void DrawExpandedList(SpriteBatch sb)
        {

        }
	}
}