using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScalingOctoNemesis.UIComponents
{
    public class ChatBox : UIContainer
    {
        SpriteFont _font;
        int limit = 5;
        public ChatBox(string id, Vector2 pos, Vector2 size, Vector2 padding, SpriteFont font)
            : base(id, pos, size, padding)
        {
            _font = font;
        }

        public void AddMessage(string message, string from)
        {
            Label label = new Label(ComponentsCount.ToString(), from + ": " + message, 0, 0, 0, 0, 0, 0, _font);
            if (ComponentsCount != limit)
                _components.Remove(_components.Find(x => x.Id == ComponentsCount.ToString()));
            else
                foreach (UIComponent c in _components)
                    c.Position -= new Vector2(0, 20);

            label.Position = new Vector2(Position.X + Padding.X, Position.Y + Padding.Y + ComponentsCount * 20);
            _components.Add(label);
        }
    }
}
