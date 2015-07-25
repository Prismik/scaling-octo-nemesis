using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTUI.Util;
using Microsoft.Xna.Framework.Input;

namespace TTUI 
{
    /// <summary>
    /// Graphical user interface element that handles UI events.
    /// </summary>
    public abstract class UIComponent
    {
        /// <summary>
        /// Gets or sets the component position.
        /// </summary>
        public virtual Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the component size.
        /// </summary>
        public virtual Vector2 Size { get; set; }

        public string Name { get; set; }
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this component is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public virtual bool Visible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this component is currently hovered.
        /// </summary>
        /// <value><c>true</c> if hover; otherwise, <c>false</c>.</value>
        public virtual bool Hover { get; set; }

        public UIComponent(string id, Vector2 position, Vector2 size)
        {
            Id = id;
            Position = position;
            Size = size;
        }

        /// <summary>
        /// Determines if a given point is within component.
        /// </summary>
        /// <returns><c>true</c>, if the point is in the component, <c>false</c> otherwise.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public virtual bool PointInComponent(int x, int y)
        {
            Point p = Resolution.PointToResolution(new Vector2(x, y));
            Rectangle rec = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            return rec.Contains(p.X, p.Y);
        }

        public virtual void Press(object o, MouseEventArgs args)
        {
            
        }

        public virtual void Release(object o, MouseEventArgs args)
        {

        }

        public virtual void Move(object o, MouseEventArgs e)
        {
            Hover = PointInComponent(e.X, e.Y);
        }

        public virtual void HandleInputEvents() { }
        public virtual void IgnoreInputEvents() { }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch sb);    
    }
}