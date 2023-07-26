using Raylib_cs;
using System.Numerics;

public sealed class InputField
{
    private readonly Vector2 _position;
    private readonly int _width;
    private readonly int _height;
    private readonly int _textSize;

    public string URL;

    public InputField(string existingURL, Vector2 position, int width, int height, int textSize)
    {
        URL = existingURL;
        _position = position;
        _width = width;
        _height = height;
        _textSize = textSize;
    }

    public void Update()
    {
        UpdateText();
        Draw();
    }

    private void Draw()
    {
        Rectangle rec = new(_position.X, _position.Y, _width, _height);
        
        Raylib.DrawRectangleRec(rec, Color.LIGHTGRAY);
        Raylib.DrawRectangleLinesEx(rec, 3, Color.DARKGRAY);

        Raylib.DrawText(URL, (int)rec.x + 5, (int)rec.y + _textSize, _textSize, Color.MAGENTA);
    }

    private void UpdateText()
    {
        if (URL.Length > 0 && Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
            SubtractInputText();

        URL += KeyboardInputService.GetPressedButtonValues() ?? "";
    }

    private void SubtractInputText() => URL = URL.Substring(0, URL.Length - 1);
}