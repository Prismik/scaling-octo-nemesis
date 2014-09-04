using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace ScalingOctoNemesis.UI 
{
	public abstract class UIItem : UIComponent
	{
        internal List<KeyEventHandler> _handlers = new List<KeyEventHandler>();
        // Determines if the InputField has the focus
        public bool Focused { get; set; }
		public UIItem(string id, float x, float y,
			float width, float height)
			: base(id, x, y, width, height)
		{
			
		}

        public UIItem(string id, Vector2 pos, Vector2 size)
           : base(id, pos, size) 
        {

        }

        public void AddKeyHandler(KeyEventHandler func)
        {
            _handlers.Add(func);
        }

        public void RemoveKeyHandler(KeyEventHandler func)
        {
            _handlers.Remove(func);
        }
	}
}