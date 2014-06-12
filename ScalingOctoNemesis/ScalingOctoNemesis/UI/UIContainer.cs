using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace ScalingOctoNemesis.UI
{
	public abstract class UIContainer : UIComponent
	{ 
		List<GameComponent> _components = new List<GameComponent>();
		public GameContainer(string id)
			: base(id)
		{

		}

		public void Add(GameComponent component)
		{
			_components.Add(component);
		}

		public bool Remove(GameComponent component)
		{
			return _components.Remove(component);
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
				else if (gc.GetType() == typeof(GameContainer))
					return ((GameContainer)gc).FindRecursive(id);

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