using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TTUI.Util;

namespace TTUI
{
	public class InputField : UIItem
    {
        int _cursor;
        int _maxLen;
        bool _cursorVisible = true;

        List<char> _forbiddenChars = new List<char>();
        Timer _blinkTimer = new Timer();
        SpriteFont _font;

		public bool Active 	{ get; set; }
		public string Value { get; private set; }

		public InputField(string placeholder, SpriteFont sf, string id, int maxLen,
			Vector2 position, Vector2 size)
			: base(id, position, size)
		{
            Initialize(placeholder, maxLen, sf);
		}

        private void Initialize(string placeholder, int maxLen, SpriteFont sf)
        {
            Value = placeholder;
            _cursor = placeholder.Length;
            _maxLen = maxLen;
            _font = sf;

            Size = new Vector2(Size.X, _font.MeasureString("A").Y + 10);
            Visible = true;
        }

        public void Dispose()
        {
            InputSystem.MouseDown -= Press;
            InputSystem.MouseUp -= Release;
            InputSystem.MouseMove -= Move;
        }

        public virtual void Press(object o, MouseEventArgs args)
        {
            if (PointInComponent(args.X, args.Y))
                OnFocus();
            else
                OnLostFocus();
        }

        public virtual void Release(object o, MouseEventArgs args)
        {

        }

		public void OnFocus()
		{
            if (!Focused)
            {
                Focused = true;
                _cursor = Value.Length;
                InputSystem.CharEntered += HandleInput;
                InputSystem.KeyDown += HandleKeys;
                foreach (KeyEventHandler h in _handlers)
                    InputSystem.KeyDown += h;
            }
		}

        public void OnLostFocus()
        {
            if (Focused)
            {
                InputSystem.CharEntered -= HandleInput;
                InputSystem.KeyDown -= HandleKeys;
                foreach (KeyEventHandler h in _handlers)
                    InputSystem.KeyDown -= h;

                Focused = false;
            }
        }

		// Specify the position of the mouse click
		// that initiated a focus event
		public void Focus(Vector2 pos)
		{
			if (!Focused)
				_cursor = StringHelper.GetCharInfoAt(_font, Value, pos.X).position;
		}

        public string Clear()
        {
            _cursor = 0;
            string value = Value;
            Value = "";
            return value;
        }

        public void SetForbiddenChars(List<char> chars)
        {
            _forbiddenChars = chars;
        }

		private void RemoveChar()
		{
            if (Value.Length != 0 && _cursor != 0)
            {
                _blinkTimer.Reset();
                _cursorVisible = true;
                Value = Value.Remove(--_cursor, 1);
            }
		}

		private void InsertChar(char c)
        {
            if (_font.Characters.Contains(c) && !_forbiddenChars.Contains(c) && Value.Length != _maxLen)
            {
                _blinkTimer.Reset();
                _cursorVisible = true;
                string str = c.ToString();
                Value = Value.Insert(_cursor++, str);
            }
		}

		private int IncrementCursor()
        {
            _blinkTimer.Reset();
            _cursorVisible = true;
			if (_cursor < Value.Length)
				_cursor++;

            return _cursor;
		}

		private int DecrementCursor()
        {
            _blinkTimer.Reset();
            _cursorVisible = true;
            if (_cursor > 0)
                _cursor--;

            return _cursor;
		}

        private void HandleInput(object sender, CharacterEventArgs e)
        {
            if (e.Character != '\b' && e.Character != '\r')
                InsertChar(e.Character);
        }

        private void HandleKeys(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if (key == Keys.Back)
                RemoveChar();
            else if (key == Keys.Left)
                DecrementCursor();
            else if (key == Keys.Right)
                IncrementCursor();
        }

        public override void HandleInputEvents()
        {
            base.HandleInputEvents();
            InputSystem.MouseDown += Press;
            InputSystem.MouseUp += Release;
            InputSystem.MouseMove += Move;
        }

        public override void IgnoreInputEvents()
        {
            base.IgnoreInputEvents();
            InputSystem.MouseDown -= Press;
            InputSystem.MouseUp -= Release;
            InputSystem.MouseMove -= Move;
        }
		public override void Update(GameTime timer)
		{
            _blinkTimer.Update(timer.ElapsedGameTime.TotalMilliseconds);
            if (_blinkTimer.Time >= 500)
            {
                _cursorVisible = !_cursorVisible;
                _blinkTimer.Reset();
            }
		}

		public override void Draw(SpriteBatch sb)
		{
            if (Visible)
            {
                DrawBackground(sb);
                if (Focused)
                    DrawBorder(sb);
            
                DrawText(sb);
                if (Focused && _cursorVisible)
                    DrawCursor(sb);
            }
		}

        public virtual void DrawBorder(SpriteBatch sb)
        {
            DrawingTools.DrawEmptyRectangle(sb, Position, Size, FlatColors.SUNFLOWER, LayerDepths.D2);
        }

        public virtual void DrawBackground(SpriteBatch sb)
        {
            DrawingTools.DrawRectangle(sb, Position, Size, FlatColors.MIDNIGHT_BLUE, LayerDepths.D3);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
            sb.DrawString(_font, Value, Position + new Vector2(5, 5), FlatColors.SILVER, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }

        public virtual void DrawCursor(SpriteBatch sb)
        {
            CharInfo info = StringHelper.GetCharInfoFrom(_font, Value, _cursor, Position + new Vector2(5, 5), 1f);
            if (info == CharInfo.Empty)
                info = StringHelper.GetCharInfoFrom(_font, " ", 0, Position + new Vector2(5, 5), 1f);
            
            DrawingTools.DrawRectangle(sb, info.area, new Color(0, 0, 0, 0.5f), LayerDepths.FRONT);
        }
	}
}