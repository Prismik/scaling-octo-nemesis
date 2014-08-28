using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScalingOctoNemesis.Util;
namespace ScalingOctoNemesis.UI 
{
	public abstract class UIComponent
	{
		public virtual Vector2 Position { get; set; }
        public virtual Vector2 Padding  { get; set; }
		public virtual Vector2 Size 	{ get; set; }

		public string Name 		{ get; set; }
        public string Id 		{ get; private set; }

        public virtual bool Visible 	{ get; set; }

		public UIComponent(string id, float x, float y,
			float width, float height, float paddingX, float paddingY)
		{
			Id = id;
            Visible = true;
			Position = new Vector2(x, y);
			Size = new Vector2(width, height);
            Padding = new Vector2(paddingX, paddingY);
		}

        public UIComponent(string id, Vector2 pos, Vector2 size, Vector2 padding)
        {
            Id = id;
            Position = pos;
            Size = size;
            Padding = padding;
        }

        public bool PointInComponent(int x, int y)
        {
            Point p = Resolution.PointToResolution(new Vector2(x, y));
            Rectangle rec = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X + (int)Padding.X * 2, (int)Size.Y + (int)Padding.Y * 2);
            return rec.Contains(p.X, p.Y);
        }

		public abstract void Update(GameTime gameTime);
		public abstract void Draw(SpriteBatch sb);	
	}
}