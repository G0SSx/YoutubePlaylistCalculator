using Raylib_cs;

public sealed class Window
{
    public readonly int Width;
    public readonly int Height;

    public Window(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void Init()
    {
        Raylib.InitWindow(Width, Height, "Playlist calculator");
        Raylib.SetTargetFPS(60);
    }

    public void Close() => Raylib.CloseWindow();
}