using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
public static class DrawingTools
{
	private static Texture2D t;
	public static void Init(GraphicsDevice gc)
	{
        // create 1x1 texture for line drawing
        t = new Texture2D(gc, 1, 1);
        t.SetData<Color>(new Color[] { Color.White });// fill the texture with white
	}

	public static void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color c, float layer)
	{
		Vector2 edge = end - start;

        // calculate angle to rotate line
        float angle = (float)Math.Atan2(edge.Y , edge.X);

        // rectangle defines shape of line and position of start of line
        // sb will strech the texture to fill this rectangle
        // width of line, change this to make thicker line
        sb.Draw(t, new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1), 
            null, c, angle, new Vector2(0, 0), SpriteEffects.None, layer);
	}

	public static void DrawRectangle(SpriteBatch sb, Rectangle rect, Color c, float layer)
	{
		sb.Draw(t, rect, null, c, 0, Vector2.Zero, SpriteEffects.None, layer);
	}

    public static void DrawRectangle(SpriteBatch sb, Vector2 position, Vector2 size, Color c, float layer)
    {
        Rectangle r = new Rectangle((int)position.X, (int)position.Y, // position
                                    (int)size.X, (int)size.Y);
        DrawRectangle(sb, r, c, layer);
    }

	public static void DrawEmptyRectangle(SpriteBatch sb, Vector2 pos, Vector2 size, Color c, float layer)
	{
        Vector2 topLeft     = pos;
        Vector2 topRight    = new Vector2(pos.X + size.X, pos.Y);
        Vector2 bottomRight = new Vector2(pos.X + size.X, pos.Y + size.Y);
        Vector2 bottomLeft  = new Vector2(pos.X, pos.Y + size.Y); 
    	DrawingTools.DrawLine(sb, topLeft, topRight, c, layer);
    	DrawingTools.DrawLine(sb, topRight, bottomRight, c, layer);
    	DrawingTools.DrawLine(sb, bottomRight, bottomLeft, c, layer);
    	DrawingTools.DrawLine(sb, bottomLeft, topLeft, c, layer);
	}
}