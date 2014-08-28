using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis.UI.Menus
{
    class ColoredMenu : Menu
    {
        public ColoredMenu(List<MenuNode> linkedNodes, SpriteFont font, SpriteBatch sb)
            : base(linkedNodes, font, sb)
        {

        }

        protected override bool EnterContextDone(MenuNode node)
        {
            return true;
        }

        protected override bool LeaveContextDone(MenuNode node)
        {
            if (node.Position.X + node.GetSize().X <= 0)
                return true;

            return false;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);   
        }
    }
}
