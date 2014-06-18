public static class StringHelper
{
	// Helper to get all the information of the char.
	public static CharInfo GetCharInfoAt(SpriteFont sf, string t, float x) 
	{
		if (t.Length == 0)
			return null;

		int width= 0;
		for (int i = 0; i != t.Length; ++i)
		{
			Vector2 measure = sf.MeasureString((string)c);
			width += measure.X;
			if (width >= x)
				return new CharInfo(i, 
					new Vector2(width, 0), 
					new Rectangle(width, 0, measure.X, measure.Y));
		}
	}

	// Helper to get just the area
	public static Rectangle GetCharAreaAt(SpriteFont sf, string t, float x)
	{
		return GetCharInfoAt(sf, t, x).area;
	}

	// Helper to get just the location
	public static Vector2 GetCharLocationAt(SpriteFont sf, string t, float x)
	{
		return GetCharInfoAt(sf, t, x).location;
	}

	// Helper to get just the position
	public static int GetCharPositionAt(SpriteFont sf, string t, float x)
	{
		return GetCharInfoAt(st, t, x).position;
	}
}

public struct CharInfo
{
	int position;
	Vector2 location;
	Rectangle area;
	
 	CharInfo(int p, Vector2 l, Rectangle a)
	{
		position = p;
		location = l;
		area = a;
	}
}
