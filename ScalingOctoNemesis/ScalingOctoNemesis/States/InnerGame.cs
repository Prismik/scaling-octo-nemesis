using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using States;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework.Graphics;

namespace ScalingOctoNemesis.States
{
    class InnerGame : GameState
    {
        InputField input;
        SpriteFont _font;
        public InnerGame(StateManager manager)
            : base(manager)
        {
             input = new InputField("Test", _font, "input1", 12, 75, 75);
             input.OnFocus();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("monospace");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            input.Update(gameTime);
        }

        public override void Draw()
        {
            Manager.SpriteBatch.Begin();
            input.Draw(Manager.SpriteBatch);
            Manager.SpriteBatch.End();
        }
    }
}
