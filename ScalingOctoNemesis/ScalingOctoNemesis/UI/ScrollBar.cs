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

        public int InnerLength  { get; set; }
        public bool Enabled     { get; set; }
        public Tooltip Tooltip  { get; set; }

        public ScrollBar(string id, float width, float height, 
                            float x, float y, float paddingX, float paddingY,
                            Button up, Button down)
			: base(id, x, y, width, height, paddingX, paddingY)
		{
            _up = up;
            _down = down;
            InnerLength = 0;
		}
		
		public virtual void Press()
		{
            _pressed = true;
		}

        public virtual void Release()
        {
            _pressed = false;
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
            // spritebatch.drawRectangle(width)
        }

        public virtual void DrawBar(SpriteBatch sb)
        {

        }

        public virtual void DrawButtons(SpriteBatch sb)
        {

        }
	}
}