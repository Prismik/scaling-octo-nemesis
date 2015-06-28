using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI
{
    // TODO Allows to have UI over other UI elements and only handle the top UI elements events.
    class UIManager
    {
    	List<UIComponent> _components = new List<UIComponent>();

    	public UIManager()
    	{

    	}

    	public UIComponent GetComponentAtPosition(Vector2 pos)
    	{
            return null;
    	}

    	public void Update()
    	{
    		
    	}

        public void Draw(SpriteBatch sb)
        {
            foreach (UIComponent c in _components)
                if (c.Visible)
                    c.Draw(sb);
        }
    }
}
