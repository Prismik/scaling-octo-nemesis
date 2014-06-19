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
			float width, float height)
		{
			Id = id;
			Position = new Vector2(x, y);
			Size = new Vector2(width, height);
		}

		public abstract void Update(GameTime gameTime);
		public abstract void Draw(SpriteBatch sb);	
	}
}