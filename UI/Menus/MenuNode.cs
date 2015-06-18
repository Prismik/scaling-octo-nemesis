using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTUI.Menus
{
    public abstract class MenuNode : UIItem
    {
        public Menu Menu { get; set; }
        public string ID { get; private set; }
        public Action Action    { get; set; }
        public MenuNode Top     { get; set; }
        public MenuNode Right   { get; set; }
        public MenuNode Bottom  { get; set; }
        public MenuNode Left    { get; set; }
        bool _active;
        internal bool _leaving;
        internal bool _init;
        public bool Active { get { return _active; } set { OnToggleActive(value); _active = value; } }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public MenuNode(string id, Action action, Vector2 position)
            : base (id, position, new Vector2(0, 0))
        {
            Action = action;
            ID = id;
            Scale = 1;
        }

        internal abstract void OnToggleActive(bool value);
        public abstract Vector2 GetSize();
        public abstract void LeaveContext(float val, int pos);
        public abstract void EnterContext(float val, int pos);
        public abstract bool LeaveContextDone();
        public abstract bool EnterContextDone();
    }
}
