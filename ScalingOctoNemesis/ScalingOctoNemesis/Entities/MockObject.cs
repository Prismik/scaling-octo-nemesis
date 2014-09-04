using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ScalingOctoNemesis.UI;

namespace ScalingOctoNemesis.Entities
{
    public class MockObject : Selectable, Moveable
    {
        public float Speed { get; set; }
        public Vector2 _moveLocation = Vector2.Zero;
        public MockObject(Vector2 pos)
        {
            Position = pos;
            Speed = 1.5f;
            Size = new Vector2(30, 30);
        }

        public override void Click(MouseButton button)
        {
            if (button == MouseButton.Left)
                Select();
        }

        public override void Select()
        {
            base.Select();
        }

        public override void Release()
        {
            base.Release();   
        }

        public void Move(Vector2 location)
        {
            _moveLocation = location;
        }

        public override void Update(GameTime time)
        {
            if (_moveLocation != Vector2.Zero)
            {
                Vector2 direction = Vector2.Normalize(_moveLocation - Position);
                Position += Speed * direction;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            Color c = Selected ? Color.Purple : Color.DarkOliveGreen;
            DrawingTools.DrawRectangle(sb, Position, new Vector2(30, 30), c, LayerDepths.POST_FRONT);
        }
    }
}
