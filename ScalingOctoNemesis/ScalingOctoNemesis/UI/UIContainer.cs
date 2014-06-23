using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
namespace ScalingOctoNemesis.UI
{
	public abstract class UIContainer : UIComponent
	{ 
		List<UIComponent> _components = new List<UIComponent>();
		public UIContainer(string id, float x, float y,
            float width, float height, float paddingX, float paddingY)
			: base(id, x, y, width, height, paddingX, paddingY)
		{

		}

		public void Add(UIComponent component)
		{
			_components.Add(component);
		}

		public bool Remove(UIComponent component)
		{
			return _components.Remove(component);
		}

		public UIComponent Find(string id, bool recursive)
		{
			foreach (UIComponent gc in _components)
				if (gc.Name == id)
					return gc;

			return null;
		}

		private UIComponent FindRecursive(string id)
		{
			foreach (UIComponent gc in _components)
				if (gc.Name == id)
					return gc;
				else if (gc.GetType() == typeof(UIContainer))
					return ((UIContainer)gc).FindRecursive(id);

			return null;
		}

		public override void Update(GameTime elapsedTime)
		{
			foreach (UIComponent gc in _components)
				gc.Update(elapsedTime);
		}

		public override void Draw(SpriteBatch sb)
		{
			foreach (UIComponent gc in _components)
				gc.Draw(sb);
		}
	}
}