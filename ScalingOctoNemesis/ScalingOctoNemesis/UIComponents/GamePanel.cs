using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ScalingOctoNemesis.Entities;

namespace ScalingOctoNemesis.UIComponents
{
    class GamePanel : UIComponent, IDisposable
    {
        bool _showLasso = false;
        Vector2 _lassoStart = Vector2.Zero;
        Vector2 _lassoEnd = Vector2.Zero;
        SelectableManager _manager;
        MockObject obj;
        MockObject obj2;
        MockObject obj3;

        public GamePanel(SelectableManager manager, Vector2 pos, Vector2 size)
            : base("GamePanel", pos, size, Vector2.Zero)
        {
            obj = new MockObject(new Vector2(Position.X + 60, Position.Y + 60));
            obj2 = new MockObject(new Vector2(Position.X + 120, Position.Y + 75));
            obj3 = new MockObject(new Vector2(Position.X + 280, Position.Y + 300));
            _manager = manager;
            _manager.AddEntity(obj);
            _manager.AddEntity(obj2);
            _manager.AddEntity(obj3);
            InputSystem.MouseDown += Click;
            InputSystem.MouseUp += Release;
            InputSystem.MouseMove += Move;
        }

        public void Dispose()
        {
            InputSystem.MouseDown -= Click;
            InputSystem.MouseUp -= Release;
            InputSystem.MouseMove -= Move;
        }

        private void Click(object o, MouseEventArgs e)
        {
            if (PointInComponent(e.X, e.Y))
            {
                if (e.Button == MouseButton.Left)
                {
                    _showLasso = true;
                    _lassoStart = _lassoEnd = new Vector2(e.X, e.Y);
                }
                else if (e.Button == MouseButton.Right)
                {
                    _manager.Move(new Vector2(e.X, e.Y));
                }
            }
        }

        private void Release(object o, MouseEventArgs e)
        {
            if (_showLasso)
                _manager.Select(_lassoStart, _lassoEnd);

            _showLasso = false;
        }

        private void Move(object o, MouseEventArgs e)
        {
            if (PointInComponent(e.X, e.Y))
            {
                if (_showLasso)
                    _lassoEnd = new Vector2(e.X, e.Y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _manager.Update(gameTime);
        }

        private void DrawLasso(SpriteBatch sb)
        {
            DrawingTools.DrawEmptyRectangle(sb, _lassoStart, _lassoEnd - _lassoStart, Color.Green, LayerDepths.FRONT);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (_showLasso)
                DrawLasso(sb);

            obj.Draw(sb);
            obj2.Draw(sb);
            obj3.Draw(sb);
        }
    }
}
