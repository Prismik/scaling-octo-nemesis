using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI
{
    public class ScrollBar : UIItem, IDisposable
	{
        bool _pressed = false;

        Rectangle _scrollRectangle;
        Button _up;
        Button _down;

        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                _scrollRectangle = new Rectangle((int)Position.X + (int)Size.X / 2, (int)Position.Y, 12, 64);

                // Set _up position
                // Set _down position
            }
        }

        public Action ScrollUp   { get; set; }
        public Action ScrollDown { get; set; }
        public int InnerLength   { get; set; }
        public float ScrollPct   { get { return (_scrollRectangle.Y - Position.Y) / (Size.Y - _scrollRectangle.Height); } }
        public bool Enabled      { get; set; }
        public Tooltip Tooltip   { get; set; }

        public ScrollBar(string id, Vector2 position, Vector2 size, Button up, Button down)
            : base(id, position, size)
        {
            _up = up;
            _down = down;
            InnerLength = 0;
            _scrollRectangle = new Rectangle((int)Position.X + (int)Size.X / 2, (int)Position.Y + (int)Size.Y - 64, 12, 64);
            Initialize();
        }

        private void Initialize()
        {
            InputSystem.MouseDown += Press;
            InputSystem.MouseUp += Release;
            InputSystem.MouseMove += Move;
        }

        public void Dispose()
        {
            InputSystem.MouseDown -= Press;
            InputSystem.MouseUp -= Release;
            InputSystem.MouseMove -= Move;
        }

        public virtual void Press(object o, MouseEventArgs args)
        {
            if (_scrollRectangle.Contains(args.Location))
                _pressed = true;
        }

        public virtual void Release(object o, MouseEventArgs args)
        {
            if (_pressed)
            {
                _pressed = false;
                move = 0;
            } 
        }

        int move = 0;
        public virtual void Move(object o, MouseEventArgs args)
        {
            if (_scrollRectangle.Contains(args.Location))
                Hover = true;
            else
                Hover = false;

            if (_pressed)
                    MoveScroller(args.Y);
        }

        private void MoveScroller(int y)
        {
            int delta = y - move;
            if (delta < 0)
                ScrollUp();
            else
                ScrollDown();

            // Maybe think of a better way to do this. It makes it impossible to have a scroller
            // That starts at y = 0
            if (move != 0 && _scrollRectangle.Y + delta >= Position.Y && _scrollRectangle.Y + delta + _scrollRectangle.Height <= Position.Y + Size.Y)
                _scrollRectangle.Offset(0, delta);

            move = y;

        }

        public override void Update(GameTime timer)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            DrawBar(sb);
            DrawScroller(sb);
            DrawButtons(sb);
        }

        public virtual void DrawScroller(SpriteBatch sb)
        {
            Color c = Hover ? Color.Coral : Color.BlueViolet;
            if (Hover)
                c = _pressed ? Color.BurlyWood : Color.Coral;
            DrawingTools.DrawRectangle(sb, _scrollRectangle, c, LayerDepths.FRONT);
        }

        public virtual void DrawBar(SpriteBatch sb)
        {
            DrawingTools.DrawLine(sb, new Vector2(Position.X + Size.X / 2, Position.Y),
                                      new Vector2(Position.X + Size.X / 2, Position.Y + Size.Y),
                                      Color.Black, LayerDepths.D1);
        }

        public virtual void DrawButtons(SpriteBatch sb)
        {
            _up.Draw(sb);
            _down.Draw(sb);
        }
	}
}