namespace ScalingOctoNemesis.UI 
{
	public abstract class UIItem : UIComponent
	{
		public GameItem(string id, float x, float y,
			float width, float height)
			: base(id, x, y, width, height)
		{
			
		}
	}
}