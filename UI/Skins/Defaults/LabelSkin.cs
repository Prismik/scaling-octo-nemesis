using System;
using Microsoft.Xna.Framework.Graphics;
using TTUI;
using Microsoft.Xna.Framework;

namespace TTUI.Skins.Defaults
{
    public class LabelSkin: Skin
    {
        public Vector2 Size { get { return _skinnedLabel.Size; } }
        public Vector2 Position { get { return _skinnedLabel.Position; } }
        public String Value { get { return _skinnedLabel.Value; } }

        private Label _skinnedLabel;
        private SpriteFont _font;
        public LabelSkin(Label label, SpriteFont font)
        {
            State = SkinStates.ENABLED;
            _skinnedLabel = label;
            _font = font;
        }

        public override void Update(GameTime elapsedTime)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            DrawText(sb);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
            sb.DrawString(_font, Value, Position, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }
    }
}

