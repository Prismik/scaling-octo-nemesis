using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTUI.Util;

namespace TTUI 
{
	public abstract class UIComponent
	{
		public virtual Vector2 Position { get; set; }
		public virtual Vector2 Size { get; set; }

		public string Name { get; set; }
        public string Id { get; private set; }

        public virtual bool Visible { get; set; }
        public virtual bool Hover { get; set; }

        public UIComponent(string id, Vector2 position, Vector2 size)
        {
            Id = id;
            Position = position;
            Size = size;
        }

        public bool PointInComponent(int x, int y)
        {
            Point p = Resolution.PointToResolution(new Vector2(x, y));
            Rectangle rec = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            return rec.Contains(p.X, p.Y);
        }

		public abstract void Update(GameTime gameTime);
		public abstract void Draw(SpriteBatch sb);	
	}
}