using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI
{
    public class CheckBox : UIItem, IDisposable
    {
        public bool Checked { get; private set; }
        public CheckBox(string id, Vector2 position, Vector2 size)
            : base(id, position, size)
        {
            InputSystem.MouseDown += Click;
        }

        private void Click(object o, MouseEventArgs args)
        {
            if (PointInComponent(args.X, args.Y))
                Checked = !Checked;
        }

        public void Dispose()
        {
            InputSystem.MouseDown -= Click;
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public virtual void DrawBox(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, Position, Size, FlatColors.MIDNIGHT_BLUE, LayerDepths.POST_FRONT);
        }

        public virtual void DrawCheck(SpriteBatch sb)
        {
            // TODO Better checked state drawing by default
            DrawingTools.DrawRectangle(sb, Position + new Vector2(4, 4), Size - new Vector2(8, 8), FlatColors.CONCRETE, LayerDepths.POST_FRONT);
        }

        public override void Draw(SpriteBatch sb)
        {
            DrawBox(sb);
            if (Checked)
                DrawCheck(sb);
        }
    }
}
