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

    public static CharInfo GetCharInfoFrom(SpriteFont sf, string t, int pos, Vector2 startLocation, float scale)
    {
        if (t.Length == 0)
            return CharInfo.Empty;

        int width = 0;
        Vector2 measure = Vector2.Zero;
        for (int i = 0; i != t.Length; ++i)
        {
            measure = sf.MeasureString(t[i].ToString()) * scale;
            if (i >= pos)
                return new CharInfo(i,
                    new Vector2(width + startLocation.X, startLocation.Y),
                    new Rectangle(width + (int)startLocation.X, (int)startLocation.Y, (int)measure.X, (int)measure.Y));

            width += (int)measure.X;
        }

        return new CharInfo(t.Length, new Vector2(width + startLocation.X, startLocation.Y), 
            new Rectangle(width + (int)startLocation.X, (int)startLocation.Y, (int)measure.X, (int)measure.Y));
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

    // Equality operator. Returns null if either operand is null, 
    // otherwise returns true or false:
    public static bool operator ==(CharInfo x, CharInfo y)
    {
        return x.area == y.area && x.location == y.location && x.position == y.position;
    }

    // Equality operator. Returns null if either operand is null, 
    // otherwise returns true or false:
    public static bool operator !=(CharInfo x, CharInfo y)
    {
        return !(x == y);
    }
}
