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

		public bool Focused { get; set; }
		public bool Active 	{ get; set; }
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

/*
 t = new Texture2D(GraphicsDevice, 1, 1);
        t.SetData<Color>(
            new Color[] { Color.White });// fill the texture with white
*/
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