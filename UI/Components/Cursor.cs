using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI
{
    /// <summary>
    /// Allows to change the cursor's image.
    /// </summary>
    public static class Cursor
    {
        /// <summary>
        /// Gets or sets the default cursor.
        /// </summary>
        /// <value>The cursor image.</value>
        public static Texture2D Pointer { get; set; }

        /// <summary>
        /// Gets or sets the help cursor.
        /// </summary>
        /// <value>The cursor image.</value>
        public static Texture2D Help { get; set; }

        /// <summary>
        /// Gets or sets the input cursor.
        /// </summary>
        /// <value>The cursor image.</value>
        public static Texture2D Input { get; set; }

        /// <summary>
        /// Gets or sets the loading cursor.
        /// </summary>
        /// <value>The cursor image.</value>
        public static Texture2D Loading { get; set; }

        /// <summary>
        /// Gets or sets the current cursor.
        /// </summary>
        public static Cursors Current { get; set; }

        static Rectangle _area = new Rectangle(0, 0, 32, 32);


        public static void Update(Point position)
        {
            _area.Location = position;
        }

        /// <summary>
        /// Draw the current cursor.
        /// </summary>
        public static void Draw(SpriteBatch sb)
        {
            switch (Current)
            {
                case Cursors.POINTER: sb.Draw(Pointer, _area, Color.White); break;
                case Cursors.INPUT: sb.Draw(Input, _area, Color.White); break;
                default: break;
            }
        }
    }

    /// <summary>
    /// Supported cursors.
    /// </summary>
    public enum Cursors { POINTER, HELP, INPUT, LOADING }
}
