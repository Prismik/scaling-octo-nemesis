using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ScalingOctoNemesis.UI
{
    class CheckBox : UIItem, IDisposable
    {
        public bool Checked { get; private set; }
        public CheckBox(string id, Vector2 pos, Vector2 size)
            : base(id, pos, size)
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
            DrawingTools.DrawRectangle(sb, Position, Size, Color.MediumAquamarine, LayerDepths.POST_FRONT);
        }

        public virtual void DrawCheck(SpriteBatch sb)
        {
            if (Checked)
            {
                DrawingTools.DrawLine(sb, Position + new Vector2(7, 7), Position + Size - new Vector2(3, 3), Color.Black, LayerDepths.FRONT);
                DrawingTools.DrawLine(sb, Position + Size - new Vector2(3, 3), Position + new Vector2(Size.X + 3, -3), Color.Black, LayerDepths.FRONT);
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            DrawBox(sb);
            DrawCheck(sb);
        }
    }
}
