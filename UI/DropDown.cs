namespace PrismUI
{
	public class DropDown : GameContainer
	{
		Action OnSelect { get; set; }
		public DropDown(InputState input, string id, float width, float height)
			: base(id)
		{
			action = _action;
		}
		
		public void Press()
		{
			
		}
	}
}