using Microsoft.Xna.Framework;
namespace ScalingOctoNemesis.UI 
{
	public abstract class GameComponent
	{
		public Vector2 Position { get; set; }
		public string Name { get; set; }
        public string Id { get; private set; }
		public GameComponent(string id)
		{
			Id = id;
		}

		public abstract void Update(GameTime gameTime);
		public abstract void Draw();	
	}
}