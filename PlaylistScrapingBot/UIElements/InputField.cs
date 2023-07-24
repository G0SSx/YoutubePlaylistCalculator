using Raylib_cs;

public class InputField
{
    private static KeyboardInputService? _keyboardInputService;
    private static string _inputText = "";

    public InputField(KeyboardInputService keyboardInputService)
    {
        _keyboardInputService = keyboardInputService;
    }

    public void Update()
    {
        Draw();
        UpdateText();
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RAYWHITE);

        // Draw the input field rectangle
        Raylib.DrawRectangle(100, 200, 400, 50, Color.LIGHTGRAY);
        Raylib.DrawRectangleLines(100, 200, 400, 50, Color.DARKGRAY);

        // Draw the input text
        Raylib.DrawText(_inputText, 110, 210, 20, Color.DARKGRAY);

        Raylib.EndDrawing();
    }

    private static void UpdateText()
    {
        if (_keyboardInputService is null)
            return;

        if (_inputText.Length > 0 && Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
            SubtractInputText();

        _inputText += _keyboardInputService.GetPressedButtonValues();
    }

    private static void SubtractInputText() => _inputText = _inputText.Substring(0, _inputText.Length - 1);
}