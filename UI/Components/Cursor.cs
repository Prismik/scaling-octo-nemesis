using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI
{
    public static class Cursor
    {
        public static Texture2D Pointer { get; set; }
        public static Texture2D Help    { get; set; }
        public static Texture2D Input   { get; set; }
        public static Texture2D Loading { get; set; }
        
        public static Cursors Current   { get; set; }

        static Rectangle _area = new Rectangle(0, 0, 32, 32);
        public static void Update(Point position)
        {
            _area.Location = position;
        }

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

    public enum Cursors { POINTER, HELP, INPUT, LOADING }
}
