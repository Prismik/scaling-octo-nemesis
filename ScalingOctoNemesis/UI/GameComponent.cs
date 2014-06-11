namespace PrismUI 
{
	public class GameComponent
	{
		public Vector2 Position { get; set; }
		public string Name { get; set; }
		public GameComponent(string id)
		{
			_id = id;
		}

		public abstract void Update(GameTime);
		public abstract void Draw();	
	}
}