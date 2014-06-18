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

namespace ScalingOctoNemesis.UI
{
	public class InputField : UIItem
	{
		int _cursor;
		int _maxLen;

		// Determines if the InputField has the focus
		public bool Focused { get; set; }
		// TBD
		public bool Active 	{ get; set; }
		// Textual value held in the input field
		public string Value { get; set; }

		public InputField(string placeholder, string id, int maxLen,
			float x, float y, float width, float height)
			: base(id, x, y, width, height)
		{
			//_input = input;
			Value = placeholder;
			_cursor = placeholder.Length;
			_maxLen = maxLen;
		}

		public InputField(string placeholder, string id, int maxLen,
			float x, float y)
			: base(id, x, y, 100, 20)
		{
			//_input = input;
			Value = placeholder;
			_cursor = placeholder.Length;
			_maxLen = maxLen;
		}

		public void Focus()
		{
			if (!Focused)
				_cursor = Value.Length;
		}

		// Specify the position of the mouse click
		// that initiated a focus event
		public void Focus(Vector2 pos)
		{
			if (!Focused)
				_cursor = GetCharAt(pos.X);
		}

		private void RemoveChar()
		{
			if (Value.Length != 0)
				Value.Remove(--_cursor, 1);
		}
		
		private void InsertChar(char c)
		{
			if (Value.Length != _maxLen)
				Value.Insert(_cursor++, c.ToString());
		}

		private void IncrementCursor()
		{
			if (cursor <= Value.Length)
				cursor++;
		}

		private void DecrementCursor()
		{
			if (cursor > 0)
				cursor--:
		}

		// Returns the array's position of the char
		private int GetCharAt(float x)
		{
			int width= 0;
			for (int i = 0; i != Value.Length; ++i)
			{
				width += SpriteFont.MeasureString((string)c).X;
				if (width >= x)
					return i;
			}
		}

		private Rectangle GetCharRectangle(int indicator)
		{
			return SpriteFont.MeasureString((string)Value[indicator]);
		}

		private char KeyInAscii(Keys key)
		{
			// Returns a char if the key pressed
			// is a character in the ascii table.
			// Otherwise, returns an empty char.
			return ' ';
		}

		public void Update()
		{
			if (Focused && _input.IsNewKeyPress()) 
				Keys key = _input.LastKeyPress;
				if (key == Keys.Back)
					RemoveChar();
				else if ((char c = KeyInAscii(key)) != '')
					InsertChar(c);
				else if (key == Keys.left)
					DecrementCursor();
				else if (key == Keys.right)
					IncrementCursor();
			}
		}

		public void Draw(SpriteBatch sb)
		{
			DrawBorder(sb);
            DrawBackground(sb);
            DrawText(sb);
            if (Focused)
            	DrawCursor(sb);
		}

        public virtual void DrawBorder(SpriteBatch sb)
        {
        	DrawingTools.DrawLine(sb, new Vector2(Position.X, Position.Y), 
        		new Vector2(Position.X+Size.X, Position.Y), Color.Red);
        	DrawingTools.DrawLine(sb, new Vector2(Position.X+Size.X, Position.Y), 
        		new Vector2(Position.X+Size.X, Position.Y+Size.Y), Color.Red);
        	DrawingTools.DrawLine(sb, new Vector2(Position.X+Size.X, Position.Y+Size.Y), 
        		new Vector2(Position.X, Position.Y+Size.Y), Color.Red);
        	DrawingTools.DrawLine(sb, new Vector2(Position.X, Position.Y+Size.Y), 
        		new Vector2(Position.X+Size.X, Position.Y), Color.Red);
        }

        public virtual void DrawBackground(SpriteBatch sb)
        {
        	DrawingTools.DrawRectangle(sb, 
        		new Rectangle(Position.X, Position.Y, Size.X, Size.Y), Color.Chocolate);
        }

        public virtual void DrawText(SpriteBatch sb)
        {
        	sb.DrawString(_font, Value, Position + new Vector2(5, 2), Color.White);
        }

        public abstract void DrawCursor(SpriteBatch sb)
        {

        }
	}
}