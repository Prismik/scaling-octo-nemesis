using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace ScalingOctoNemesis.UI
{
	public class DropDown : UIItem
	{
        public List<Object> Objects { get; set; }
        List<DropDownItem> _items = new List<DropDownItem>();
        SpriteFont _font;
        public Object Selected { get; set; }
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
            Objects = new List<Object>();
            _expandRectangle = new Rectangle((int)Position.X + (int)Size.X + (int)Padding.X - 20, (int)Position.Y, 20 + (int)Padding.X, (int)Size.Y + (int)Padding.Y * 2);
            Expanded = false;

            InputSystem.MouseDown += Press;
        }

        public void Dispose()
        {
            InputSystem.MouseDown -= Press;
        }

        public void AddItem(Object o)
        {
            DropDownItem i = new DropDownItem(o, _font);
            i.Click = delegate { SetSelected(i); };
            _items.Add(i);
        }

        public void SetSelected(DropDownItem i)
        {
            Selected = i;
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

        private void Press(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                if (PointInComponent(e.X, e.Y))
                    Expanded = true;
                else
                    Expanded = false;
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
            DrawingTools.DrawEmptyRectangle(sb, Position, Size + Padding * 2, Color.LightGray);
        }

        public virtual void DrawBackground(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, Position, Size + Padding * 2, Color.DarkSlateGray);
        }

        public virtual void DrawSelectedItem(SpriteBatch sb)
        {
            if (Selected != null)
                sb.DrawString(_font, Selected.ToString(), Position + Padding, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        public virtual void DrawExpandButton(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, _expandRectangle, Color.Coral);
        }

        public virtual void DrawExpandedList(SpriteBatch sb)
        {
            for (int i = 0; i != Objects.Count; ++i)
                sb.DrawString(_font, Objects[i].ToString(), Position + new Vector2(Padding.X, Size.Y + Padding.Y + i * 22), Color.White); 
        }
	}
}