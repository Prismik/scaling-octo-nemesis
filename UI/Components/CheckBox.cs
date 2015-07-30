using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI
{
    /// <summary>
    /// Graphical control element that permits the user to make a choice between 
    /// one of two possible mutually exclusive options.
    /// </summary>
    public class CheckBox : UIItem, IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this checkbox is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public bool Checked { get; private set; }
        public CheckBox(string id, Vector2 position, Vector2 size)
            : base(id, position, size)
        {

        }

        public override void Press(object o, MouseEventArgs e)
        {
            if (PointInComponent(e.X, e.Y))
                Checked = !Checked;
        }

        public void Dispose()
        {
            InputSystem.MouseDown -= Press;
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
