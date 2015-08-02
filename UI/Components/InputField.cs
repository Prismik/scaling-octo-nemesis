using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TTUI.Util;
using TTUI.Skins.Defaults;

namespace TTUI
{
    /// <summary>
    /// Graphical control element that enables the user to input text information.
    /// </summary>
    public class InputField : UIItem
    {
        internal int _cursor;
        int _maxLen;
        internal bool _cursorVisible = true;

        List<char> _forbiddenChars = new List<char>();
        Timer _blinkTimer = new Timer();
        SpriteFont _font;

        /// <summary>
        /// Gets or sets a value indicating whether this input field is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets the text value within this input field.
        /// </summary>
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

            Skin = new InputSkin(this, _font);
        }

        public override void Press(object o, MouseEventArgs e)
        {
            base.Press(o, e);
            if (PointInComponent(e.X, e.Y))
                OnFocus();
            else
                OnLostFocus();
        }

        public override void Release(object o, MouseEventArgs e)
        {
            base.Release(o, e);
            if (!PointInComponent(e.X, e.Y))
                OnLostFocus();
                
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

        /// <summary>
        /// Clear the text within this input field and returns it.
        /// </summary>
        public string Clear()
        {
            _cursor = 0;
            string value = Value;
            Value = "";
            return value;
        }

        /// <summary>
        /// Sets the forbidden characters that will be ignored when added to this input field.
        /// </summary>
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
                Value = Value.Insert(_cursor++, c.ToString());
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
            Skin.Draw(sb);
        }
    }
}