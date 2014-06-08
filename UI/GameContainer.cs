namespace PrismUI
{
	public class GameContainer : GameComponent
	{
		List<GameComponent> _components = new List<GameComponent>();
		public GameContainer(string id)
			: base(id)
		{

		}

		public void Add(GameComponent component)
		{
			_components.add(component);
		}

		public GameComponent Remove(GameComponent component)
		{
			_components.remove(component);
		}

		public GameComponent Find(string id, bool recursive)
		{
			foreach (GameComponent gc in _components)
				if (gc.Name == id)
					return gc;

			return null;
		}

		private GameComponent FindRecursive(string id)
		{
			foreach (GameComponent gc in _components)
				if (gc.Name == id)
					return gc;
				else if (typeof(gc) == GameContainer)
					return gc.FindRecursive(id);

			return null;
		}

		public virtual override void Update(GameTime elapsedTime)
		{
			foreach (GameComponent gc in _components)
				gc.Update(elapsedTime);
		}

		public virtual override void Draw()
		{
			foreach (GameComponent gc in _components)
				gc.Draw();
		}
	}
}