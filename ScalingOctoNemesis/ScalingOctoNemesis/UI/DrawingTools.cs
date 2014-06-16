public static class DrawingTools
{
	private static Texture2D t;
	public static void Init()
	{
        // create 1x1 texture for line drawing
        t = new Texture2D(GraphicsDevice, 1, 1);
        t.SetData<Color>(new Color[] { Color.White });// fill the texture with white
	}

	public static void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color c)
	{
		Vector2 edge = end - start;

        // calculate angle to rotate line
        float angle = (float)Math.Atan2(edge.Y , edge.X);

        // rectangle defines shape of line and position of start of line
        // sb will strech the texture to fill this rectangle
        // width of line, change this to make thicker line
        sb.Draw(t, new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1), 
            null, c, angle, new Vector2(0, 0), SpriteEffects.None, 0);
	}

	public static void DrawRectangle(SpriteBatch sb, Rectangle rect, Color c)
	{
		sb.Draw(t, rect, c);
	}
}