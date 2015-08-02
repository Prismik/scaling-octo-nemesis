using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI.Skins.Defaults
{
    public class InputSkin: Skin
    {

        public Vector2 Size { get { return _skinnedInput.Size; } }
        public Vector2 Position { get { return _skinnedInput.Position; } }
        public String Value { get { return _skinnedInput.Value; } }
        public bool CursorVisible { get { return _skinnedInput._cursorVisible; } }

        private InputField _skinnedInput;
        private SpriteFont _font;

        public InputSkin(InputField input, SpriteFont font)
        {
            State = SkinStates.ENABLED;
            _skinnedInput = input;
            _font = font;
        }

        public override void Update(GameTime elapsedTime)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            if (_skinnedInput.Visible)
            {
                DrawBackground(sb);
                if (State == SkinStates.ACTIVE || State == SkinStates.PRESSED)
                    DrawBorder(sb);

                DrawText(sb);
                if ((State == SkinStates.ACTIVE || State == SkinStates.PRESSED) && CursorVisible)
                    DrawCursor(sb);
            }
        }

        public virtual void DrawBorder(SpriteBatch sb)
        {
            DrawingTools.DrawEmptyRectangle(sb, Position, Size, FlatColors.SUNFLOWER, LayerDepths.D2);
        }

        public virtual void DrawBackground(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, Position, Size, FlatColors.MIDNIGHT_BLUE, LayerDepths.D3);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
            sb.DrawString(_font, Value, Position + new Vector2(5, 5), FlatColors.SILVER, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }

        public virtual void DrawCursor(SpriteBatch sb)
        {
            CharInfo info = StringHelper.GetCharInfoFrom(_font, Value, _skinnedInput._cursor, Position + new Vector2(5, 5), 1f);
            if (info == CharInfo.Empty)
                info = StringHelper.GetCharInfoFrom(_font, " ", 0, Position + new Vector2(5, 5), 1f);

            DrawingTools.DrawRectangle(sb, info.area, new Color(0, 0, 0, 0.5f), LayerDepths.FRONT);
        }
    }
}

