using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using States;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScalingOctoNemesis.UIComponents;
using ScalingOctoNemesis.Util;

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

        Button t1;
        Button t2;
        Button t3;
        public ConnectTo(StateManager manager)
            : base(manager)
        {
            t1 = new Button("1366x768", "res1", new Vector2(180, 25), new Vector2(25, 25), new Vector2(5, 5), _font);
            t1.Action = delegate { Resolution.ChangeResolution(1366, 768); };
            t2 = new Button("1024x768", "res2", new Vector2(180, 25), new Vector2(25, 85), new Vector2(5, 5), _font);
            t2.Action = delegate { Resolution.ChangeResolution(1024, 768); };
            t3 = new Button("800x600", "res3", new Vector2(180, 25), new Vector2(25, 125), new Vector2(5, 5), _font);
            t3.Action = delegate { Resolution.ChangeResolution(800, 600); };

            _title = new Label("tit", "Connect To Game", 450, 20, 200, 20, 0, 0, _font);

            _inputLabel = new Label("inp", "IP Address", 500, 75, 200, 20, 0, 0, _font);
            _input = new InputField("", _font, "ip", 15, new Vector2(500, 50), new Vector2(150, 25), new Vector2(5, 5));

            _nameLabel = new Label("name", "Name", 500, 100, 200, 20, 0, 0, _font);
            _name = new InputField("", _font, "ip", 15, new Vector2(500, 125), new Vector2(150, 25), new Vector2(5, 5));
            _connect = new Button("Connect", "con", new Vector2(120, 25), new Vector2(500, 450), new Vector2(5, 5), _font);
            _connect.Action = delegate {
                string ip = _input.Value;
                if (_name.Value.Length > 0)
                {
                    Manager.AddState(new GameRoom(Manager, _name.Value));
                    Manager.RemoveState(this);
                }
            };
            _cancel = new Button("Cancel", "can", new Vector2(120, 25), new Vector2(500, 390), new Vector2(5, 5), _font);
            _cancel.Action = delegate {
                Manager.Game.Exit();
            };
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
            t1.Update(gameTime);
            t2.Update(gameTime);
            t3.Update(gameTime);
        }

        public override void Draw()
        {

            Manager.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Resolution.TransformMatrix);
            _title.Draw(Manager.SpriteBatch);
            _inputLabel.Draw(Manager.SpriteBatch);
            _input.Draw(Manager.SpriteBatch);
            _connect.Draw(Manager.SpriteBatch);
            _cancel.Draw(Manager.SpriteBatch);
            _nameLabel.Draw(Manager.SpriteBatch);
            _name.Draw(Manager.SpriteBatch);
            t1.Draw(Manager.SpriteBatch);
            t2.Draw(Manager.SpriteBatch);
            t3.Draw(Manager.SpriteBatch);
            Manager.SpriteBatch.End();
        }
    }
}
