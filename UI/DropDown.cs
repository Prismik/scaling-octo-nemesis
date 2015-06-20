using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI
{
	public class DropDown : UIItem
	{
        List<DropDownItem> _items = new List<DropDownItem>();
        SpriteFont _font;
        Rectangle _expandRectangle;
        DropDownItem _selectedDropDown = null;
		Action OnSelect { get; set; }


        public Object Selected { get { return _selectedDropDown.Value; } }

        public bool Expanded   { get; set; }
        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                _expandRectangle = new Rectangle((int)Position.X + (int)Size.X - 20, (int)Position.Y, 
                                                    20, (int)Size.Y );
                for (int i = 0; i != _items.Count; ++i)
                    _items[i].Position = Position + new Vector2(5, Size.Y + 5 + i * 30);
            }
        }

        public DropDown(string id, Vector2 pos, Vector2 size, SpriteFont font)
            : base(id, pos, size)
        {
            _font = font;
            Initialize();
        }
            
        private void Initialize()
        {
            _expandRectangle = new Rectangle((int)Position.X + (int)Size.X - 20, (int)Position.Y, 
                                                20, (int)Size.Y);
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
            i.Position =  Position + new Vector2(5, Size.Y + 5 + _items.Count * 30);
            i.Action = delegate { SetSelected(i); };
            _items.Add(i);
        }

        public object FindItem(String id)
        {
            return _items.Where(x => x.Id == id).First();
        }

        public void SetSelected(DropDownItem i)
        {
            _selectedDropDown = i;
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
                {
                    Expanded = true;
                    foreach (DropDownItem i in _items)
                        i.Visible = true;
                }
                else
                {
                    Expanded = false;
                    foreach (DropDownItem i in _items)
                        i.Visible = false;
                }
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
            DrawingTools.DrawEmptyRectangle(sb, Position, Size, Color.LightGray, LayerDepths.D2);
        }

        public virtual void DrawBackground(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, Position, Size, Color.DarkSlateGray, LayerDepths.D4);
        }

        public virtual void DrawSelectedItem(SpriteBatch sb)
        {
            if (_selectedDropDown != null)
                sb.DrawString(_font, _selectedDropDown.ToString(), Position + new Vector2(5, 5), Color.White, 0, 
                                Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }

        public virtual void DrawExpandButton(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, _expandRectangle, Color.Coral, LayerDepths.D3);
        }

        public virtual void DrawExpandedList(SpriteBatch sb)
        {
            for (int i = 0; i != _items.Count; ++i)
                _items[i].Draw(sb);
                //sb.DrawString(_font, Objects[i].ToString(), 
                //Position + new Vector2(Padding.X, Size.Y + Padding.Y + i * 22), Color.White); 
        }
	}
}