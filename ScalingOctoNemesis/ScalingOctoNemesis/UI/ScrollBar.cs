using System;
using Microsoft.Xna.Framework;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI
{
	public class ScrollBar : UIItem
	{
		Action _action;
        bool _pressed = false;

        public bool Enabled     { get; set; }
        public Tooltip Tooltip  { get; set; }

        public ScrollBar(Action action, string value, string id, 
            float width, float height, float x, float y, float paddingX, float paddingY)
			: base(id, x, y, width, height, paddingX, paddingY)
		{
			_action = action;
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
            DrawScroller();
            DrawBar();
        }

        public virtual void DrawScroller()
        {
            // spritebatch.drawRectangle(width)
        }

        public virtual void DrawBar()
        {

        }
	}
}