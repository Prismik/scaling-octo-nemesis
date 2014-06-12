using System;
using Microsoft.Xna.Framework;
using System.Timers;
namespace ScalingOctoNemesis.UI
{
	public class Button : UIItem
	{
		Action _action;
        bool _pressed = false;

        public bool Enabled     { get; set; }
        public Tooltip Tooltip  { get; set; }
        
		public Button(Action action, string value, string id, 
            float width, float height, float x, float y)
			: base(id, x, y)
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

        public void Update(GameTime timer)
        {

        }

        public void Draw()
        {
            DrawBorder();
            if (!_pressed)
            {
                DrawInner();
                Tooltip.Draw();
            }
            else
                DrawInnerPressed();
        }

        public virtual void DrawBorder()
        {
            // spritebatch.drawRectangle(width)
        }

        public virtual void DrawInner()
        {

        }

        public virtual void DrawText()
        {

        }

        public virtual void DrawInnerPressed()
        {

        }
	}
}