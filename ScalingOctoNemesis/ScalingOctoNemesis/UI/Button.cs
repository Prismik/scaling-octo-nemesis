using System;
using Microsoft.Xna.Framework;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI
{
	public class Button : UIItem, IDisposable
	{
        bool _pressed = false;

        public Action Action;   { get; set; }
        public bool Enabled     { get; set; }
        public Tooltip Tooltip  { get; set; }
        
		public Button(string value, string id, 
            float width, float height, float x, float y, float paddingX, float paddingY)
			: base(id, x, y, width, height, paddingX, paddingY)
		{
            Initialize();
		}

        public Button(string value, string id, 
            Vector2 size, Vector2 pos, Vector2 padding)
            : base(id, pos, size, padding)
        {
            Initialize();
        }
		
        private void Initialize()
        {
            InputSystem.MouseDown += Press;
            InputSystem.MouseUp += Release;
        }

        public void Dispose()
        {
            InputSystem.MouseDown  -= Press;
            InputSystem.MouseUp -= Release;
        }

		public virtual void Press(object o, MouseEventArgs args)
		{
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

        public override void Update(GameTime timer)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            DrawBorder();
            if (!_pressed)
            {
                DrawInner();
                Tooltip.Draw(sb);
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