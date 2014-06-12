using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI
{
	public class DropDown : UIContainer
	{
		Action OnSelect { get; set; }
        public bool Expanded { get; set; } 
		public DropDown(string id, float width, float height)
			: base(id)
		{
            
		}
		
		public void Press()
		{
			
		}

        public void Draw(SpriteBatch sb)
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