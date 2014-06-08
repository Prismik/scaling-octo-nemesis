namespace PrismUI
{
	public class Button : GameItem
	{
		Action _action;
		public Button(Action action, InputState input, string default, string id, float width, float height)
			: base(id)
		{
			action = _action;
		}
		
		public void Press()
		{
			
		}
	}
}