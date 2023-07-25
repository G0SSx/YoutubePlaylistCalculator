using Raylib_cs;
using System.Numerics;

public sealed class Button
{
    public bool IsPressed { get; private set; }
    public Rectangle Rect { get; init; }

    private readonly Color _colorButton;
    private readonly Color _textColor;
    private readonly string _text;
    private readonly int _textSize;
    private readonly Vector2 _textPosition;

    public Button(Rectangle rect, Color buttonColor, string text, Color textColor, int textSize = 20)
    {
        Rect = rect;
        _colorButton = buttonColor;
        _text = text;
        _textColor = textColor;
        _textSize = textSize;

        _textPosition = new(
            Rect.x + Rect.width / 2 - Raylib.MeasureText(_text, 20) / 2,
            Rect.y + Rect.height / 2);
    }

    public void Update()
    {
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Rect) &&
            Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
        {
            DrawRectPressed();
            IsPressed = true;
        }
        else
        {
            DrawRect();
            IsPressed = false;
        }

        DrawText();
    }

    private void DrawRect() => Raylib.DrawRectangleRec(Rect, _colorButton);

    private void DrawRectPressed() =>
        Raylib.DrawRectangleRec(Rect, new Color(_colorButton.r, _colorButton.g, _colorButton.b, Convert.ToByte(200)));

    private void DrawText() =>
        Raylib.DrawText(_text, (int)_textPosition.X, (int)_textPosition.Y, _textSize, _textColor);
}