using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI
{
	public class UIContainer : UIComponent
	{ 
		public List<UIComponent> _components = new List<UIComponent>();
        public int ComponentsCount { get { return _components.Count; } }

        public UIContainer(string id, Vector2 position, Vector2 size)
            : base(id, position, size)
        {

        }

		public void Add(UIComponent component)
		{
            component.Position += this.Position;
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

        private void ActivateComponentsEventListening()
        {
            foreach (UIComponent gc in _components)
                gc.HandleInputEvents();
        }

        private void DeactivateComponentsEventListening()
        {
            foreach (UIComponent gc in _components)
                gc.IgnoreInputEvents();
        }

        public override void Move(object o, MouseEventArgs e)
        {
            base.Move(o, e);
            if (Hover)
                ActivateComponentsEventListening();
            else
                DeactivateComponentsEventListening();
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