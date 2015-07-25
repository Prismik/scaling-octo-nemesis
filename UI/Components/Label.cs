using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI
{
    /// <summary>
    /// Graphical control element which displays text
    /// </summary>
	public class Label : UIItem
	{
        SpriteFont _font;
        string _value;
        public Label(string id, string text, Vector2 position, Vector2 size, SpriteFont font)
            : base(id, position, size)
		{
            _font = font;
            _value = text;
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
            sb.DrawString(_font, _value, Position, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }
	}
}