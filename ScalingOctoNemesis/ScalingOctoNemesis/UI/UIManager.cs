using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
    		// If (Left click just happened)
    			// Get position of new click
    			// comp = GetComponentAtPosition(position of click) 
    			// if (comp != null)
    				// handle click on the component

    		// NOT SURE ABOUT THIS
    		// Get new key presses
    		// handle key press on the focused component
    		// if no handler, search for other components with a handler
    	}
    }
}
