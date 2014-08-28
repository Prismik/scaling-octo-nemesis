using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScalingOctoNemesis.UI.Menus
{
    public abstract class Menu : IDisposable
    {
        public SpriteFont Font { get; private set; }
        SpriteBatch _spriteBatch;
        List<MenuNode> _nodes = new List<MenuNode>();
        bool _init = false;
        bool _leaving = false;

        public MenuNode CurrentNode { get; private set; }
        public Menu(List<MenuNode> linkedNodes, SpriteFont font, SpriteBatch sb)
        {
            Font = font;
            _nodes = linkedNodes;
            _spriteBatch = sb;
            CurrentNode = _nodes.First();
            CurrentNode.Active = true;

            foreach (MenuNode node in _nodes)
                node.Menu = this;

            InputSystem.KeyUp += KeyPress;
            InputSystem.MouseMove += Move;
            InputSystem.MouseDown += Click;
        }

        public void Dispose()
        {
            InputSystem.KeyUp -= KeyPress;
            InputSystem.MouseMove -= Move;
            InputSystem.MouseDown -= Click;
        }

        /// <summary>
        /// Changes the active menu node.
        /// </summary>
        /// <param name="newNode">The new node to be active.</param>
        protected virtual void ChangeActive(MenuNode newNode)
        {
            CurrentNode.Active = false;
            newNode.Active = true;
            CurrentNode = newNode;
        }

        protected virtual void EnterContext(MenuNode node)
        {
            _init = true;
            node.EnterContext(pass, 1);
        }

        protected virtual void LeaveContext(MenuNode node, int pos)
        {
            node.LeaveContext(pass, pos);
            pass += 0.1f;
        }

        protected abstract bool LeaveContextDone(MenuNode node);
        protected abstract bool EnterContextDone(MenuNode node);

        float pass = 0.3f;
        public virtual void Update(GameTime gameTime)
        {
            int i = 0;
            bool doneLeave = false;
            foreach (MenuNode node in _nodes)
            {
                node.Update(gameTime);

                if (!_init)
                {
                    EnterContext(node);
                    EnterContextDone(node);
                }

                if (_leaving)
                {
                    LeaveContext(node, i++);
                    doneLeave = LeaveContextDone(node);
                }
            }

            if (doneLeave)
                CurrentNode.Action();
        }

        public virtual void Move(object o, MouseEventArgs e)
        {
            foreach (MenuNode node in _nodes)
                if (node.PointInComponent(e.X, e.Y))
                    ChangeActive(node);
        }

        public virtual void Click(object o, MouseEventArgs e)
        {
            foreach (MenuNode node in _nodes)
                if (node.PointInComponent(e.X, e.Y))
                    _leaving = true;
        }

        public virtual void KeyPress(object o, KeyEventArgs e)
        {
            if (_init && !_leaving)
            {
                if (e.KeyCode == Keys.Enter)
                    _leaving = true;
                else if (e.KeyCode == Keys.Down)
                    ChangeActive(CurrentNode.Bottom);
                else if (e.KeyCode == Keys.Up)
                    ChangeActive(CurrentNode.Top);
            }
        }

        public virtual void Draw()
        {
            foreach (MenuNode node in _nodes)
                node.Draw(_spriteBatch);
        }
    }
}
