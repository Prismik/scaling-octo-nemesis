using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScalingOctoNemesis.UI
{
    class UIManager
    {
    	UIComponent _focused = null;
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
