using Raylib_cs;
using System.Numerics;

public class UIFactory : IUIFactory
{
    private Window? _window;

    public Button? CreateButton(string text, Color buttonColor, Color textColor, int yOffset, int width,
        int height, int textSize = 20)
    {
        if (_window is null)
        {
            ThrowWindowNullException();
            return null;
        }

        Rectangle rect = new(_window.Width / 2 - width / 2, yOffset, width, height);
        Button button = new(rect, buttonColor, text, textColor, textSize);

        return button;
    }

    public InputField? CreateInputField(int yOffset, int width, int height, int textSize = 15)
    {
        if (_window is null)
        {
            ThrowWindowNullException();
            return null;
        }

        Vector2 position = new Vector2(_window.Width / 2 - width / 2, yOffset);
        return new(position, width, height, textSize);
    }

    public void CreateText(string text, int yOffset, Color color, int textSize = 20)
    {
        Vector2 position = new(GetCentralizedTextXPosition(text, textSize), yOffset);

        Text.Draw(text, position, textSize, color);
    }

    public Window CreateWindow(int width, int height)
    {
        _window = new(width, height);
        return _window;
    }

    private int GetCentralizedTextXPosition(string text, int size)
    {
        if (_window is null)
        {
            ThrowWindowNullException();
            return 0;
        }

        return (_window.Width - Raylib.MeasureText(text, size)) / 2;
    }

    private void ThrowWindowNullException() => Console.WriteLine("Window was not created!");
}