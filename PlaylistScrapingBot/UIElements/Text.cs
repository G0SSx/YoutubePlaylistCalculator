using Raylib_cs;
using System.Numerics;

public sealed class Text
{
    public static void Draw(string text, Vector2 position, int size, Color color)
    {
        if (size < 1)
            size = 1;

        Raylib.DrawText(text, (int)position.X, (int)position.Y, size, color);
    }
}