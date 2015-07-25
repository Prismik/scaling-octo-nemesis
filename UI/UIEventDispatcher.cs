using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TTUI
{
    // TODO Allows to have UI over other UI elements and only handle the top UI elements events.
    public class UIEventDispatcher: IDisposable
    {
        private List<UIContainer> _containers = new List<UIContainer>();

        public UIEventDispatcher()
        {
            InputSystem.MouseMove += Move;
            InputSystem.MouseDown += Press;
            InputSystem.MouseUp += Release;
        }

        public void Dispose()
        {
            InputSystem.MouseMove -= Move;
            InputSystem.MouseDown -= Press;
            InputSystem.MouseUp -= Release;
        }

    	public UIComponent GetComponentAtPosition(Vector2 pos)
    	{
            return null;
    	}

        public void Add(UIContainer container)
        {
            _containers.Add(container);
        }

    	public void Update()
    	{
    		
    	}

        public void Draw(SpriteBatch sb)
        {
            foreach (UIComponent c in _containers)
                if (c.Visible)
                    c.Draw(sb);
        }

        public void Press(object o, MouseEventArgs e)
        {
            foreach (UIContainer c in _containers)
                c.Press(o, e);
        }

        public void Release(object o, MouseEventArgs e)
        {
            foreach (UIContainer c in _containers)
                c.Release(o, e);
        }

        public void Move(object o, MouseEventArgs e)
        {
            foreach (UIContainer c in _containers)
                c.Move(o, e);
        }
    }
}
