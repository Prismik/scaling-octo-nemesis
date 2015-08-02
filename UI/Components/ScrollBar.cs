using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI
{
    public class ScrollBar: UIContainer
    {
        bool _pressed = false;

        // TODO Implement steps
        Vector2? _pressPosition = null;

        Button _scroll;
        Button _up;
        Button _down;

        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;

                // Set _up position
                // Set _down position
            }
        }

        public Action ScrollUp   { get; set; }
        public Action ScrollDown { get; set; }
        public int InnerLength   { get; set; }
        public float ScrollPct   { get { return (_scroll.Position.Y - Position.Y) / (Size.Y - _scroll.Size.Y); } }
        public bool Enabled      { get; set; }
        public Tooltip Tooltip   { get; set; }

        public ScrollBar(string id, Vector2 position, Vector2 size, SpriteFont font)
            : base(id, position, size)
        {
            _up = new Button("^", "upBtn", new Vector2(30, 30), Position, font);
            _down = new Button("v", "downBtn", new Vector2(30, 30), Position + new Vector2(0, Size.Y - 30), font);
            InnerLength = 0;
            _scroll = new Button("", "scroll", new Vector2(30, 30), Position + new Vector2(0, 60), font);

            Size -= new Vector2(0, 60);
            Position += new Vector2(0, 30);
        }
         
        public override void Press(object o, MouseEventArgs e)
        {
            if (_scroll.PointInComponent(e.X, e.Y))
            {
                _pressed = true;
                // TODO Move only when Y is at press Y within rectangle
                //_pressPosition = new Vector2(e.X, e.Y) - new Vector2 (_scrollRectangle.X, _scrollRectangle.Y);
            }
        }

        public override void Release(object o, MouseEventArgs e)
        {
            if (_pressed)
            {
                _pressed = false;
                _pressPosition = null;
                move = 0;
            } 
        }

        int move = 0;
        public override void Move(object o, MouseEventArgs e)
        {
            if (_pressed)
                MoveScroller(e.Y);
        }

        private void MoveScroller(int y)
        {
            // TODO Move only when Y is at press Y within rectangle
           // if (_pressPosition.HasValue && _pressPosition.Value.Y == y - _scrollRectangle.Top)
           // {
                int delta = y - move;
                if (delta < 0)
                    ScrollUp();
                else
                    ScrollDown();

                // Maybe think of a better way to do this. It makes it impossible to have a scroller
                // That starts at y = 0
            if (move != 0 && _scroll.Position.Y + delta >= Position.Y && _scroll.Position.Y + delta + _scroll.Size.Y <= Position.Y + Size.Y)
                _scroll.Position += new Vector2(0, delta);

                move = y;
         //   }
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
            
            _scroll.Draw(sb);
        }

        public virtual void DrawBar(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, Position,
                new Vector2(30, Size.Y),
                new Color(Color.Black, 0.5f), LayerDepths.BACK);
        }

        public virtual void DrawButtons(SpriteBatch sb)
        {
            _up.Draw(sb);
            _down.Draw(sb);
        }
    }
}