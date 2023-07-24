using Raylib_cs;
using System.Numerics;

public sealed class Button
{
    private readonly Rectangle _rect;
    private readonly Color _colorButton;
    private readonly Color _textColor;
    private readonly string _text;
    private readonly int _textSize;
    private readonly Vector2 _textPosition;

    public Button(Rectangle rect, Color buttonColor, string text, Color textColor, int textSize = 20)
    {
        _rect = rect;
        _colorButton = buttonColor;
        _text = text;
        _textColor = textColor;
        _textSize = textSize;

        _textPosition = new(
            _rect.x + _rect.width / 2 - Raylib.MeasureText(_text, 20) / 2, 
            _rect.y + _rect.height / 2);
    }

    public void Update(bool isPressed)
    {
        Action toDo = isPressed ? DrawPressed : Draw;
        toDo.Invoke();

        DrawText();
    }

    private void Draw() => Raylib.DrawRectangleRec(_rect, _colorButton);

    private void DrawPressed() => 
        Raylib.DrawRectangleRec(_rect, new(_colorButton.r, _colorButton.g, _colorButton.b, Convert.ToByte(200)));

    private void DrawText() => 
        Raylib.DrawText(_text, (int)_textPosition.X, (int)_textPosition.Y, _textSize, _textColor);
}