using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScalingOctoNemesis.UI.Menus;
using ScalingOctoNemesis.Util;

namespace ScalingOctoNemesis.States
{
    class MainMenu : GameState
    {
        SpriteFont _font;
        Menu _menu;

        public MainMenu(StateManager manager)
            : base(manager)
        {
            InitMenu();
        }

        public void InitMenu()
        {
            List<MenuNode> nodes = new List<MenuNode>();
            MenuNode conNode = new ColoredMenuNode("Connect", "Connect To Game", delegate
            {
                Manager.AddState(new ConnectTo(Manager));
                Manager.RemoveState(this);
            }, new Vector2(250, 50), _font);

            MenuNode optNode = new ColoredMenuNode("Options", "Options", delegate
            {
                //Manager.AddState(new LocalMultiplayer(Manager));
                //Manager.RemoveState(this);
            }, new Vector2(250, 150), _font);

            MenuNode extNode = new ColoredMenuNode("Exit", "Exit", delegate
            {
                Manager.Game.Exit();
            }, new Vector2(250, 250), _font);

            conNode.Bottom = optNode;
            conNode.Top = extNode;

            optNode.Bottom = extNode;
            optNode.Top = conNode;

            extNode.Bottom = conNode;
            extNode.Top = optNode;

            nodes.AddRange(new MenuNode[] { conNode, optNode, extNode });
            _menu = new ColoredMenu(nodes, _font, Manager.SpriteBatch);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("monospace");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _menu.Update(gameTime);
        }

        public override void Draw()
        {
            Manager.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Resolution.TransformMatrix);
            _menu.Draw();
            Manager.SpriteBatch.End();
        }
    }
}
