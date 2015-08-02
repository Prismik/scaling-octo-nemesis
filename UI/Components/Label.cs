using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTUI.Skins.Defaults;

namespace TTUI
{
    /// <summary>
    /// Graphical control element which displays text
    /// </summary>
    public class Label: UIItem
    {
        SpriteFont _font;
        public string Value { get; set; }
        public Label(string id, string text, Vector2 position, Vector2 size, SpriteFont font)
            : base(id, position, size)
        {
            _font = font;
            Value = text;
            Skin = new LabelSkin(this, _font);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        //    throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch sb)
        {
            Skin.Draw(sb);
        }
    }
}