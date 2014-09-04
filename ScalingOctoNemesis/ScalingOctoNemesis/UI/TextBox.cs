using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI 
{
	public class TextBox : InputField
	{
		public TextBox(string placeholder, SpriteFont sf, string id, float x, float y, float width, float height)
			: base(placeholder, sf, id, 999, x, y, width, height)
		{

		}
	}
}