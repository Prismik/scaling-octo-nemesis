using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public static class StringHelper
{
	// Helper to get all the information of the char.
	public static CharInfo GetCharInfoAt(SpriteFont sf, string t, float x) 
	{
		if (t.Length == 0)
			return CharInfo.Empty;

		int width= 0;
		for (int i = 0; i != t.Length; ++i)
		{
			Vector2 measure = sf.MeasureString(t[i].ToString());
			width += (int)measure.X;
			if (width >= x)
				return new CharInfo(i, 
					new Vector2(width, 0), 
					new Rectangle(width, 0, (int)measure.X, (int)measure.Y));
		}

        return CharInfo.Empty;
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
		return GetCharInfoAt(sf, t, x).position;
	}
}

public struct CharInfo
{
	public int position;
	public Vector2 location;
	public Rectangle area;
	
 	public CharInfo(int p, Vector2 l, Rectangle a)
	{
		position = p;
		location = l;
		area = a;
	}

    public static CharInfo Empty
    {
        get { return new CharInfo(-1, Vector2.Zero, Rectangle.Empty); }
    }
}
