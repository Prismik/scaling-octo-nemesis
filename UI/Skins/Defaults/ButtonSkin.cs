using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI.Skins.Defaults
{
    public class ButtonSkin: Skin
    {
        public Vector2 Size { get { return _skinnedButton.Size; } }
        public Vector2 Position { get { return _skinnedButton.Position; } }
        public String Value { get { return _skinnedButton.Value; } }
        public float TextSize { get { return _skinnedButton.TextSize; } }

        private Button _skinnedButton;
        private SpriteFont _font;

        public ButtonSkin(Button button, SpriteFont font)
        {
            State = SkinStates.ENABLED;
            _skinnedButton = button;
            _font = font;
        }

        public override void Update(GameTime elapsedTime)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            DrawButton(sb);
            DrawText(sb);
        }

        private void DrawButton(SpriteBatch sb)
        { 
            Color c = FlatColors.MIDNIGHT_BLUE;
            if (State == SkinStates.HOVER)
                c = FlatColors.WET_ASPHALT;

            DrawingTools.DrawRectangle(sb, Position, Size, c, LayerDepths.D3);
        }

        private void DrawText(SpriteBatch sb)
        {
            // TODO Center Y as well
            Vector2 middle = Position + new Vector2((Size.X - TextSize) / 2, 5);
            sb.DrawString(_font, Value, middle, FlatColors.SILVER, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }
    }
}

