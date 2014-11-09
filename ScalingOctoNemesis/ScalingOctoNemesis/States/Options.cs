using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using States;
using ScalingOctoNemesis.UI;
using Microsoft.Xna.Framework;
using ScalingOctoNemesis.Util;
using Microsoft.Xna.Framework.Graphics;

namespace ScalingOctoNemesis.States
{
    class Options: GameState
    {
        Label _title;
        DropDown _resolution;
        SpriteFont _font;
        Button _cancel;
        Button _apply;
        Button _ok;
        public Options(StateManager manager)
            : base(manager)
        {
            _title = new Label("title", "Options", Resolution.ViewportCenter.X, 20, 100, 20, _font);
            _resolution = new DropDown("res", new Vector2(Resolution.ViewportCenter.X, Resolution.ViewportCenter.Y - 50), new Vector2(200, 35), _font);
            DropDownItem r1 = new DropDownItem(new Vector2(1366, 768), _font);
            _resolution.AddItem(r1);
            DropDownItem r2 = new DropDownItem(new Vector2(1024, 768), _font);
            _resolution.AddItem(r2);
            DropDownItem r3 = new DropDownItem(new Vector2(800, 600), _font);
            _resolution.AddItem(r3);
            _resolution.SetSelected((DropDownItem)_resolution.FindItem(x => x.Id == Resolution.CurrentResolution.ToString()));

            _cancel = new Button("Cancel", "cancel", new Vector2(120, 35), new Vector2(Resolution.ViewportCenter.X - 120, Resolution.VirtualViewport.Y - 100), _font);
            _cancel.Action = delegate {
                GoBack();
            };
            _apply = new Button("Apply", "apply", new Vector2(120, 35), new Vector2(Resolution.ViewportCenter.X + 10, Resolution.VirtualViewport.Y - 100), _font);
            _apply.Action = delegate
            {
                ApplySettings();
            };
            _ok = new Button("Ok", "ok", new Vector2(120, 35), new Vector2(Resolution.ViewportCenter.X + 140, Resolution.VirtualViewport.Y - 100), _font);
            _ok.Action = delegate
            {
                ApplySettings();
                GoBack();
            };
        }

        private void ApplySettings()
        {
            ChangeResolution();
        }

        private void GoBack()
        {
            Manager.AddState(new MainMenu(Manager));
            Manager.RemoveState(this);
        }

        private void ChangeResolution()
        {
            //DropDownItem item = (DropDownItem)_resolution.GetS();
            //Vector2 v = (Vector2)item.Value;
            //Resolution.ChangeResolution((int)v.X, (int)v.Y);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("monospace");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            Manager.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Resolution.TransformMatrix);
            _resolution.Draw(Manager.SpriteBatch);
            _title.Draw(Manager.SpriteBatch);
            _cancel.Draw(Manager.SpriteBatch);
            _apply.Draw(Manager.SpriteBatch);
            _ok.Draw(Manager.SpriteBatch);
            Manager.SpriteBatch.End();
        }
    }
}
