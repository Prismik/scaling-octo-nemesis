using System;
using Microsoft.Xna.Framework;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI
{
	public class ScrollBar : UIItem
	{
        bool _pressed = false;
        Button _up;
        Button _down;

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                _expandRectangle = new Rectangle((int)Position.X + (int)Size.X / 2, (int)Position.Y, 12, 64);

                // Set _up position
                // Set _down position
            }
        }

        public int InnerLength  { get; set; }
        public bool Enabled     { get; set; }
        public Tooltip Tooltip  { get; set; }

        Rectangle _scrollRectangle;
        public ScrollBar(string id, float width, float height, 
                            float x, float y, float paddingX, float paddingY,
                            Button up, Button down)
			: base(id, x, y, width, height, paddingX, paddingY)
		{
            _up = up;
            _down = down;
            InnerLength = 0;
            _expandRectangle = new Rectangle((int)Position.X + (int)Size.X / 2, (int)Position.Y, 12, 64);
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
            {
                Hover = true;
                if (e.Button == MouseButton.Left)
                {
                    // Calculate move ammount
                    // Convert to Scroller Move ammount
                    // If the scroller can move in the desired direction
                    // Then move it
                    // Otherwise, stay in place
                    MoveScroller();
                }
            }
            else
                Hover = false;
        }

        private void MoveScroller()
        {

        }

        public override void Update(GameTime timer)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            DrawBar();
            DrawScroller();
            DrawButtons();
        }

        public virtual void DrawScroller(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, _scrollRectangle, Color.Coral, LayerDepths.FRONT);
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