using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using States;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScalingOctoNemesis.UIComponents;
using Microsoft.Xna.Framework;

namespace ScalingOctoNemesis.States
{
    class InnerGame : GameState
    {
        InputField input;
        Button up;
        Button down;
        ChatBox chat;
        DropDown choice;
        SpriteFont _font;
        public InnerGame(StateManager manager)
            : base(manager)
        {
            //choice = new DropDown("dropdown", 350, 50, 150, 15, 5, 5, _font);
            chat = new ChatBox("ChatBox", new Vector2(50, 150), new Vector2(100, 400), Vector2.Zero, _font);
            up = new Button("U", "U", 25, 25, 350, 50, 5, 5, _font);
            up.Action = chat.UpIndex;
            down = new Button("D", "D", 25, 25, 350, 100, 5, 5, _font);
            down.Action = chat.DownIndex;
            input = new InputField("Test", _font, "input1", 12, 75, 75, 100, 30, 5, 5);
            input.AddKeyHandler(delegate(object o, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter)
                {
                    string val = input.Value;
                    input.Clear();
                    chat.AddMessage(val, "Prismik");
                }
            });

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
            up.Update(gameTime);
            down.Update(gameTime);
            input.Update(gameTime);
            chat.Update(gameTime);
            //choice.Update(gameTime);
        }

        public override void Draw()
        {
            Manager.SpriteBatch.Begin();
            up.Draw(Manager.SpriteBatch);
            down.Draw(Manager.SpriteBatch);
            input.Draw(Manager.SpriteBatch);
            chat.Draw(Manager.SpriteBatch);
            //choice.Draw(Manager.SpriteBatch);
            Manager.SpriteBatch.End();
        }
    }
}
