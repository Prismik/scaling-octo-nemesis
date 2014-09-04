using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace ScalingOctoNemesis.UI 
{
	public class Label : UIItem
	{
        SpriteFont _font;
        string value;
        public Label(string id, string text, float x, float y, float width, float height, SpriteFont font)
			: base(id, x, y, width, height)
		{
            _font = font;
            value = text;
		}

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        //    throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch sb)
        {
            DrawText(sb);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
            sb.DrawString(_font, value, Position, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }
	}
}