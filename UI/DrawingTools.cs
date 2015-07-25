using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// Provides a set of helper functions to draw into a graphics device.
/// </summary>
public static class DrawingTools
{
    private static Texture2D t;

    /// <summary>
    /// Initializes the DrawingTools with the specified GraphicsDevice. It is mandatory to
    /// call this function prior to any other DrawingTools functions.
    /// </summary>
    /// <param name="gc">The graphics device.</param>
    public static void Init(GraphicsDevice gc)
    {
        // create 1x1 texture for line drawing
        t = new Texture2D(gc, 1, 1);
        t.SetData<Color>(new Color[] { Color.White });// fill the texture with white
    }

    /// <summary>
    /// Draws a line.
    /// </summary>
    /// <param name="sb">Sb.</param>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    /// <param name="c">C.</param>
    /// <param name="layer">Layer.</param>
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

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="sb">Sb.</param>
    /// <param name="rect">Rect.</param>
    /// <param name="c">C.</param>
    /// <param name="layer">Layer.</param>
    public static void DrawRectangle(SpriteBatch sb, Rectangle rect, Color c, float layer)
    {
        sb.Draw(t, rect, null, c, 0, Vector2.Zero, SpriteEffects.None, layer);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="sb">Sb.</param>
    /// <param name="position">Position.</param>
    /// <param name="size">Size.</param>
    /// <param name="c">C.</param>
    /// <param name="layer">Layer.</param>
    public static void DrawRectangle(SpriteBatch sb, Vector2 position, Vector2 size, Color c, float layer)
    {
        Rectangle r = new Rectangle((int)position.X, (int)position.Y, // position
                                    (int)size.X, (int)size.Y);
        DrawRectangle(sb, r, c, layer);
    }

    /// <summary>
    /// Draws a rectangle without filling the middle.
    /// </summary>
    /// <param name="sb">The SpriteBatch used for this rectangle draw.</param>
    /// <param name="pos">The position, top-left, of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="c">The color of the rectangle.</param>
    /// <param name="layer">The layer on which to draw the rectangle.</param>
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