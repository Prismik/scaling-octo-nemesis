using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TTUI
{
    /// <summary>
    /// Graphical user interface element that handle a list of UIComponent.
    /// </summary>
    public class UIContainer : UIComponent
    { 
        public List<UIComponent> _components = new List<UIComponent>();
        public int ComponentsCount { get { return _components.Count; } }
        private UIComponent _active = null;
            
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
           
        public override void Move(object o, MouseEventArgs e)
        {
            base.Move(o, e);
            foreach (UIComponent c in _components)
                c.Move(o, e);
        }

        public override void Press(object o, MouseEventArgs e)
        {
            base.Press(o, e);
            if (_active == null)
            {
                foreach (UIComponent c in _components)
                {
                    c.Press(o, e);
                    if (c.PointInComponent(e.X, e.Y))
                        _active = c;
                }
            }
            else
            {
                if (!_active.PointInComponent(e.X, e.Y))
                {
                    foreach (UIComponent c in _components)
                        c.Press(o, e);
                }

                _active.Press(o, e);
                _active = null;
            }
        }

        public override void Release(object o, MouseEventArgs e)
        {
            base.Release(o, e);
            foreach (UIComponent c in _components)
                c.Release(o, e);
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