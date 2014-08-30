using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using States;
using ScalingOctoNemesis.UIComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScalingOctoNemesis.Util;
using ScalingOctoNemesis.UI;

namespace ScalingOctoNemesis.States
{
    class InnerGame : GameState
    {
        SpriteFont _font;
        TopBar _top;
        GamePanel _panel;
        BottomPanel _bottom;
        SelectableManager _selectManager;
        public InnerGame(StateManager manager)
            : base(manager)
        {
            _selectManager = new SelectableManager();

            float topY = Resolution.VirtualViewport.Y * 1 / 15;
            float midY = Resolution.VirtualViewport.Y * 11 / 15;
            float botY = Resolution.VirtualViewport.Y * 3 / 15;
            _top = new TopBar(new Vector2(0, 0), 
                              new Vector2(Resolution.VirtualViewport.X, topY));
            _panel = new GamePanel(_selectManager, new Vector2(0, topY), 
                                   new Vector2(Resolution.VirtualViewport.X, midY));
            _bottom = new BottomPanel(new Vector2(0, Resolution.VirtualViewport.Y - botY), 
                                      new Vector2(Resolution.VirtualViewport.X, botY));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("monospace");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            _top.Update(gameTime);
            _panel.Update(gameTime);
            _bottom.Update(gameTime);
        }

        public override void Draw()
        {
            Manager.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Resolution.TransformMatrix);
            _top.Draw(Manager.SpriteBatch);
            _panel.Draw(Manager.SpriteBatch);
            _bottom.Draw(Manager.SpriteBatch);
            Manager.SpriteBatch.End();
        }
    }
}
