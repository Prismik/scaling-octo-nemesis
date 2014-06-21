/* There are two choices
 *	1. Have a UI library with Inputs, SelectList and such	
 *  2. Have the UI inputs be part of the menu architecture
 * 
 * We probably need to be able to handle mouse / pointer selection
 * as it enhance the UI and makes it possible to use inputs in standard
 * computer games (i.e. non-console specific).
 */ 

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

		// Determines if the InputField has the focus
		public bool Focused { get; set; }
		// TBD
		public bool Active 	{ get; set; }
		// Textual value held in the input field
		public string Value { get; set; }

		public InputField(string placeholder, SpriteFont font, string id, int maxLen,
			float x, float y, float width, float height)
			: base(id, x, y, width, height)
		{
            Initialize(placeholder, maxLen, font);
		}

		public InputField(string placeholder, SpriteFont sf, string id, int maxLen,
			float x, float y)
			: base(id, x, y, 100, 30)
		{
            Initialize(placeholder, maxLen, sf);
		}

        private void Initialize(string placeholder, int maxLen, SpriteFont sf)
        {
            Value = placeholder;
            _cursor = placeholder.Length;
            _maxLen = maxLen;
            _font = sf;
        }

		public void OnFocus()
		{
            if (!Focused)
            {
                Focused = true;
                _cursor = Value.Length;
                InputSystem.CharEntered += HandleInput;
                InputSystem.KeyDown += HandleKeys;
            }
		}

        public void OnLostFocus()
        {
            if (Focused)
            {
                InputSystem.CharEntered -= HandleInput;
                InputSystem.KeyDown -= HandleKeys;
            }
        }

		// Specify the position of the mouse click
		// that initiated a focus event
		public void Focus(Vector2 pos)
		{
			if (!Focused)
				_cursor = StringHelper.GetCharInfoAt(_font, Value, pos.X).position;
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
            if (e.Character != '\b')
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
        	DrawingTools.DrawLine(sb, new Vector2(Position.X, Position.Y), 
        		new Vector2(Position.X+Size.X, Position.Y), Color.LightGray);
        	DrawingTools.DrawLine(sb, new Vector2(Position.X+Size.X, Position.Y), 
        		new Vector2(Position.X+Size.X, Position.Y+Size.Y), Color.LightGray);
        	DrawingTools.DrawLine(sb, new Vector2(Position.X+Size.X, Position.Y+Size.Y),
                new Vector2(Position.X, Position.Y + Size.Y), Color.LightGray);
        	DrawingTools.DrawLine(sb, new Vector2(Position.X, Position.Y+Size.Y),
                new Vector2(Position.X, Position.Y), Color.LightGray);
        }

        public virtual void DrawBackground(SpriteBatch sb)
        {
        	DrawingTools.DrawRectangle(sb, 
        		new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.DarkSlateGray);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
        	sb.DrawString(_font, Value, Position + new Vector2(5, 2), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        public virtual void DrawCursor(SpriteBatch sb)
        {
            CharInfo info = StringHelper.GetCharInfoFrom(_font, Value, _cursor, Position + new Vector2(5, 2), 1f);
            if (info == CharInfo.Empty)
                info = StringHelper.GetCharInfoFrom(_font, " ", 0, Position + new Vector2(5, 2), 1f);
            
            DrawingTools.DrawRectangle(sb, info.area, new Color(0, 0, 0, 0.5f));
        }
	}
}