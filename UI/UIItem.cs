using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TTUI 
{
	public abstract class UIItem : UIComponent
	{
        internal List<KeyEventHandler> _handlers = new List<KeyEventHandler>();

        public bool Focused { get; set; }

        public UIItem(string id, Vector2 position, Vector2 size)
           : base(id, position, size) 
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