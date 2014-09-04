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
using ScalingOctoNemesis.Util;

namespace ScalingOctoNemesis.States
{
    class GameRoom : GameState
    {
        Texture2D _background = null;

        Label _name;
        Label _civ;
        Label _player;
        Label _team;

        InputField input;
        Button up;
        Button down;
        Button populate;
        Button exit;
        ChatBox chat;
        ScrollBar scroll;
        SpriteFont _font;
        GameSlot[] _slots = new GameSlot[8];
        public GameRoom(StateManager manager, string player)
            : base(manager)
        {
            _name = new Label("name", "Name", 45, 5, 120, 20, _font);
            _civ = new Label("civ", "Civ", 250, 5, 120, 20, _font);
            _player = new Label("player", "Player", 450, 5, 120, 20, _font);
            _team = new Label("team", "Team", 550, 5, 120, 20, _font);

            for (int i = 0; i != _slots.Length; ++i)
                _slots[i] = new GameSlot(_font, new Vector2(40, i * 30 + 50), new Vector2(600, 25));

            populate = new Button("Populate", "populate", new Vector2(100, 25), new Vector2(800, 50), _font);
            populate.Action = delegate { Populate("Player"); };
            chat = new ChatBox("ChatBox", new Vector2(50, 400), new Vector2(600, 300), _font);
            up = new Button("U", "U", 25, 25, 650, 350, _font);
            up.Action = chat.UpIndex;
            down = new Button("D", "D", 25, 25, 650, 720, _font);
            down.Action = chat.DownIndex;
            scroll = new ScrollBar("Scroll", 12, (int)(chat.Size.Y),
                                    (int)(chat.Position.X + chat.Size.X),
                                    chat.Position.Y, 
                                    up, down);
            scroll.ScrollUp = delegate {
                if (!chat.Full)
                    return;

                int topMsg = (int)(scroll.ScrollPct * chat.Messages);
                if (chat.Index == topMsg)
                    return;

                if (chat.Index > topMsg/* - chat.MaxMsg*/)
                {
                    while (chat.Index != topMsg)
                        chat.UpIndex();
                }
                
            };
            scroll.ScrollDown = delegate {
                if (!chat.Full)
                    return;

                int topMsg = (int)(scroll.ScrollPct * chat.Messages);
                if (scroll.ScrollPct == 1.0f)
                    topMsg--;
                if (chat.Index == topMsg)
                    return;

                if (chat.Index < topMsg/* - chat.MaxMsg*/)
                {
                    while (chat.Index != topMsg)
                        chat.DownIndex();
                }
            };

            input = new InputField("Test", _font, "input1", 12, 50, 710, 590, 30);
            input.AddKeyHandler(delegate(object o, KeyEventArgs args) {
                if (args.KeyCode == Keys.Enter)
                {
                    string val = input.Value;
                    input.Clear();
                    chat.AddMessage(val, "Prismik");
                    scroll.InnerLength = chat.InnerLength;
                    if (chat.Full)
                        chat.DownIndex();
                }
            });

            input.OnFocus();

            exit = new Button ("X", "X", 24, 24, 990, 0, _font);
            exit.Action = delegate { Manager.Game.Exit(); };

            Populate(player);
        }

        private void Populate(string name)
        {
            for (int i= 0; i != _slots.Length; ++i)
                if (_slots[i].Available && _slots[i].Open)
                {
                    Player p = new Player(name, GameSlot.Colors[i], "Yamato");
                    _slots[i].Populate(p);
                    break;
                }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("monospace");
            _background = Manager.Game.Content.Load<Texture2D>("bg");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            up.Update(gameTime);
            down.Update(gameTime);
            input.Update(gameTime);
            chat.Update(gameTime);
            populate.Update(gameTime);
            scroll.Update(gameTime);
            exit.Update(gameTime);
        }

        public override void Draw()
        {
            Manager.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Resolution.TransformMatrix);
            Manager.SpriteBatch.Draw(_background, new Rectangle(0, 0, 1366, 768), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, LayerDepths.BACK);  
            up.Draw(Manager.SpriteBatch);
            down.Draw(Manager.SpriteBatch);
            input.Draw(Manager.SpriteBatch);
            chat.Draw(Manager.SpriteBatch);
            populate.Draw(Manager.SpriteBatch);
            scroll.Draw(Manager.SpriteBatch);
            exit.Draw(Manager.SpriteBatch);
            _name.Draw(Manager.SpriteBatch);
            _civ.Draw(Manager.SpriteBatch);
            _player.Draw(Manager.SpriteBatch);
            _team.Draw(Manager.SpriteBatch);
            foreach (GameSlot g in _slots)
                g.Draw(Manager.SpriteBatch);
            Manager.SpriteBatch.End();
        }
    }
}
