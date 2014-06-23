using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI 
{
	public class Label : UIItem
	{
        public Label(string id, string text, float x, float y, float width, float height, float paddingX, float paddingY)
			: base(id, x, y, width, height, paddingX, paddingY)
		{
			
		}

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        //    throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch sb)
        {
            DrawText();
        }

        public virtual void DrawText()
        {

            }
	}
}