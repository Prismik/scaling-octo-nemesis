using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScalingOctoNemesis.Util;

namespace ScalingOctoNemesis.UI
{
	public class InputField : UIItem
	{
        int _cursor;
        bool _cursorVisible = true;
        Timer _blinkTimer = new Timer();
		int _maxLen;
        SpriteFont _font;
        bool Hover { get; set; }
		// TBD
		public bool Active 	{ get; set; }
		// Textual value held in the input field
		public string Value { get; private set; }

		public InputField(string placeholder, SpriteFont font, string id, int maxLen,
			float x, float y, float width, float height, float paddingX, float paddingY)
			: base(id, x, y, width, height, paddingX, paddingY)
		{
            Initialize(placeholder, maxLen, font);
		}

		public InputField(string placeholder, SpriteFont sf, string id, int maxLen,
			Vector2 pos, Vector2 size, Vector2 padding)
			: base(id, pos, size, padding)
		{
            Initialize(placeholder, maxLen, sf);
		}

        private void Initialize(string placeholder, int maxLen, SpriteFont sf)
        {
            Value = placeholder;
            _cursor = placeholder.Length;
            _maxLen = maxLen;
            _font = sf;
            InputSystem.MouseDown += Press;
            InputSystem.MouseUp += Release;
            InputSystem.MouseMove += Move;
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

        public virtual void Move(object o, MouseEventArgs args)
        {
            if (PointInComponent(args.X, args.Y))
            {
                // change the cursor
                Hover = true;
            }
            else
                Hover = false;
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
            if (Value.Length != _maxLen)
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
            DrawBackground(sb);
            DrawBorder(sb);
            DrawText(sb);
            if (Focused && _cursorVisible)
            	DrawCursor(sb);
		}

        public virtual void DrawBorder(SpriteBatch sb)
        {
            DrawingTools.DrawEmptyRectangle(sb, Position, Size + Padding * 2, Color.LightGray, LayerDepths.D2);
        }

        public virtual void DrawBackground(SpriteBatch sb)
        {
        	DrawingTools.DrawRectangle(sb, Position, Size + Padding * 2, Color.DarkSlateGray, LayerDepths.D3);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
        	sb.DrawString(_font, Value, Position + Padding, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, LayerDepths.D1);
        }

        public virtual void DrawCursor(SpriteBatch sb)
        {
            CharInfo info = StringHelper.GetCharInfoFrom(_font, Value, _cursor, Position + Padding, 1f);
            if (info == CharInfo.Empty)
                info = StringHelper.GetCharInfoFrom(_font, " ", 0, Position + Padding, 1f);
            
            DrawingTools.DrawRectangle(sb, info.area, new Color(0, 0, 0, 0.5f), LayerDepths.FRONT);
        }
	}
}