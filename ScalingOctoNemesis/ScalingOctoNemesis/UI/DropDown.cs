using Microsoft.Xna.Framework.Graphics;
using System;
namespace ScalingOctoNemesis.UI
{
	public abstract class DropDown : UIContainer
	{
		Action OnSelect { get; set; }
        public bool Expanded { get; set; } 
		public DropDown(string id, float x, float y, float width, float height)
			: base(id, x, y, width, height)
		{
            
		}
		
		public void Press()
		{
			
		}

        public override void Draw(SpriteBatch sb)
        {
            DrawBorder();
            DrawBackground();
            DrawSelectedItem();
            if (Expanded)
                DrawExpandedList();
        }

        public abstract void DrawBorder();
        public abstract void DrawBackground();
        public abstract void DrawSelectedItem();
        public abstract void DrawExpandedList();
	}
}