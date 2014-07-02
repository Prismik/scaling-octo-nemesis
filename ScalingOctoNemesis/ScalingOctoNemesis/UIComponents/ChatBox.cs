using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScalingOctoNemesis.Util;

namespace ScalingOctoNemesis.UIComponents
{
    public class ChatBox : UIContainer
    {
        int _index = 0; // Index from which to draw the messages
        int _maxLines; // Max nb of lines to be shown concurently 
        float _lineHeight; // Height of each message lines
        SpriteFont _font;

        // Replaced by vector be cause we need direct access
        // Or any collection that allows the behaviours
        // of both Vector and Queue (Direct access + FIFO)
        Queue<UIComponent> _messages = new Queue<UIComponent>();
        Timer _timer = new Timer();
        public ChatBox(string id, Vector2 pos, Vector2 size, Vector2 padding, SpriteFont font)
            : base(id, pos, size, padding)
        {
            _font = font;
            _lineHeight = _font.MeasureString(" ").Y;
            _maxLines = (Size.Y - Padding.Y * 2) / _lineHeight;
        }

        public void UpIndex()
        {
            if (_index != 0)
                --_index;
        }

        public void DownIndex()
        {
            if (_index != _maxLines-1)
                ++_index;
        }

        public void AddMessage(string message, string from)
        {
            Label label = new Label(ComponentsCount.ToString(), from + ": " + message, 0, 0, 0, 0, 0, 0, _font);
            //label.Position = new Vector2(Position.X + Padding.X, Position.Y + Padding.Y + ComponentsCount * 20);
            _components.Add(label);
            _messages.Enqueue(label);
            //Delay.AddOps(new DelayOps(RemoveMessage, new Timer(), 3000));
        }

        private void RemoveMessage()
        {
            _components.Remove(_messages.Dequeue());
            if (_components.Count != 0)
                foreach (UIComponent c in _components)
                    c.Position -= new Vector2(0, 20);
        }

        public override void Update(GameTime elapsedTime)
        {
            base.Update(elapsedTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (_messages.Length == 0)
                return;

            for (int i = _index, j = 0; i != _messages.Length && j != _lines; ++i)
            {
                _messages[i].Position = Position + Padding + new Vector2(0, j * _lineHeight);
                _messages[i].Draw(sb);
            }

        }
    }
}
