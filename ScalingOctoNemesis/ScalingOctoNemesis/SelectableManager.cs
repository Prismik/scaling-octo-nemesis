using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScalingOctoNemesis.Entities;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis
{
    public class SelectableManager
    {
        List<Selectable> _selected = new List<Selectable>();
        List<Selectable> _entities = new List<Selectable>();
        public SelectableManager()
        {

        }

        public void Select(Vector2 start, Vector2 end)
        {
            foreach (Selectable s in _selected)
                s.Release();

            _selected.Clear();
            int x = (int)Math.Min(start.X, end.X);
            int y = (int)Math.Min(start.Y, end.Y);
            Rectangle zone = new Rectangle(x, y, (int)Math.Abs(start.X - end.X), (int)Math.Abs(start.Y - end.Y));
            foreach (Selectable s in _entities)
                if (zone.Intersects(new Rectangle((int)s.Position.X, (int)s.Position.Y, (int)s.Size.X, (int)s.Size.Y)))
                {
                    _selected.Add(s);
                    s.Select();
                }

        }

        public void Move(Vector2 location)
        {
            foreach (Selectable s in _selected)
            {
                if (s is Moveable)
                {
                    ((Moveable)s).Move(location);
                }
            }
        }

        public void AddEntity(Selectable s)
        {
            _entities.Add(s);
        }

        public void Update(GameTime time)
        {
            foreach (Selectable s in _entities)
                s.Update(time);
        }
    }
}
