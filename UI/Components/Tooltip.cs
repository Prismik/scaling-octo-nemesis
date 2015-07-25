using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTUI
{
    /// <summary>
    /// Graphical user interface element showed when the user hovers the cursor over an item for a 
    /// certain time without clicking it.
    public class Tooltip : UIItem
    {
        public Tooltip(string id, Vector2 position, Vector2 size)
            : base(id, position, size)
        { 
        
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            //throw new NotImplementedException();
        }
    }
}
