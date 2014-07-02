using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI 
{
	public abstract class UIComponent
	{
		// Relative to the component's container
		// Or relative to the main container ?
		// In all cases, it is the top-left point
		// of the component.
		public Vector2 Position { get; set; }
        public Vector2 Padding  { get; set; }
		public Vector2 Size 	{ get; set; }

		public string Name 		{ get; set; }
        public string Id 		{ get; private set; }

        public bool Visible 	{ get; set; }
        // An idea to resolve cross-components input handling
        // would be to add a list of bound components
        // public List<UIComponent> BoundComponents { get; set; }
        // OR
        // Add a parent handler such as chaine de commande
        // public UIComponent ParentHandler { get; set; }
        // Main view points to bottom panel. Bottom panel
        // points to top panel
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
            Rectangle rec = new Rectangle(Position.X, Position.Y, Size.X + Padding.X*2, Size.Y + Padding.Y*2)
            return rec.Contains(x, y);
        }

		public abstract void Update(GameTime gameTime);
		public abstract void Draw(SpriteBatch sb);	
	}
}