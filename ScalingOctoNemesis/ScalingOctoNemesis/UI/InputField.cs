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
	public class InputField : GameItem
	{
		InputState _input;
		int cursor;

		public bool Focused { get; set; }
		public bool Active { get; set; }
		public string Value { get; set; }

		public InputField(InputState input, string placeholder, string id)
			: base(id)
		{
			//_input = input;
			Value = placeholder;
			cursor = placeholder.Length;
		}

		private void RemoveChar()
		{
			Value.Remove(--cursor, 1);
		}
		
		private void InsertChar(char c)
		{
			Value.Insert(cursor++, c.ToString());
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
			if (Focused)
				if (_input.IsNewKeyPress())
					if (_input.LastKeyPress == Keys.Enter)
					{
						Focused = false;
					 	Active = true;
					}

			if (Active) 
			{
				if (_input.IsNewKeyPress())
				{
					Keys key = _input.LastKeyPress;
					if (key == Keys.Back)
						RemoveChar();
					else if (key == Keys.Enter)
					{
						Focused = true;
						Active = false;
					}
					else if ((char c = KeyInAscii(key)) != '')
					{
						InsertChar(c);
					}
				}
			}
		}

		public void Draw(SpriteBatch sb)
		{
			DrawBorder();
            DrawBackground();
            DrawText();
            DrawCursor();
		}

        public abstract void DrawBorder();
        public abstract void DrawBackground();
        public abstract void DrawText();
        public abstract void DrawCursor();
	}
}