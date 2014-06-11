using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI 
{
	public class Label : GameItem
	{
        public Label(string id, string text)
			: base(id)
		{
			
		}

        public void Draw(SpriteBatch sb)
        {
            DrawText();
        }

        public abstract void DrawText();
	}
}