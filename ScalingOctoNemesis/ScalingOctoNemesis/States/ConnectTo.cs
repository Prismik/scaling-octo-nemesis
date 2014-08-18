using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using States;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScalingOctoNemesis.UIComponents;

namespace ScalingOctoNemesis.States
{
    class ConnectTo : GameState
    {
        SpriteFont _font;

        Label _title;
        Label _inputLabel;
        Label _nameLabel;
        InputField _input;
        InputField _name;
        Button _connect;
        Button _cancel;
        public ConnectTo(StateManager manager)
            : base(manager)
        {
            _title = new Label("tit", "Connect To Game", 450, 20, 200, 20, 0, 0, _font);

            _inputLabel = new Label("inp", "IP Address", 500, 275, 200, 20, 0, 0, _font);
            _input = new InputField("", _font, "ip", 15, new Vector2(500, 300), new Vector2(150, 25), new Vector2(5, 5));

            _nameLabel = new Label("name", "Name", 500, 175, 200, 20, 0, 0, _font);
            _name = new InputField("", _font, "ip", 15, new Vector2(500, 200), new Vector2(150, 25), new Vector2(5, 5));
            _connect = new Button("Connect", "con", new Vector2(120, 25), new Vector2(500, 350), new Vector2(5, 5), _font);
            _connect.Action = delegate {
                string ip = _input.Value;
                if (_name.Value.Length > 0)
                {
                    Manager.AddState(new GameRoom(Manager, _name.Value));
                    Manager.RemoveState(this);
                }
            };
            _cancel = new Button("Cancel", "can", new Vector2(120, 25), new Vector2(500, 390), new Vector2(5, 5), _font);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("monospace");

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _input.Update(gameTime);
            _connect.Update(gameTime);
            _cancel.Update(gameTime);
            _name.Update(gameTime);
        }

        public override void Draw()
        {

            Manager.SpriteBatch.Begin(SpriteSortMode.BackToFront, null);
            _title.Draw(Manager.SpriteBatch);
            _inputLabel.Draw(Manager.SpriteBatch);
            _input.Draw(Manager.SpriteBatch);
            _connect.Draw(Manager.SpriteBatch);
            _cancel.Draw(Manager.SpriteBatch);
            _nameLabel.Draw(Manager.SpriteBatch);
            _name.Draw(Manager.SpriteBatch);
            Manager.SpriteBatch.End();
        }
    }
}
