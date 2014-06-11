using System;
using Microsoft.Xna.Framework;
using System.Timers;
namespace ScalingOctoNemesis.UI
{
	public class Button : GameItem
	{
		Action _action;
        bool _pressed = false;

        public bool Enabled { get; set; }
        public Tooltip Tooltip { get; set; }
		public Button(Action action, InputState input, string value, string id, float width, float height)
			: base(id)
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

        public void Draw(GameTime timer)
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

        public abstract void DrawBorder();
        public abstract void DrawInner();
        public abstract void DrawInnerPressed();
	}
}