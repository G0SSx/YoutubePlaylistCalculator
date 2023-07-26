using Raylib_cs;
using TextCopy;

public static class KeyboardInputService
{
    public static string? GetPressedButtonValues()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_V) &&
            Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL))
        {
            return ClipboardService.GetText();
        }

        foreach (KeyboardKey key in Enum.GetValues(typeof(KeyboardKey)))
        {
            if (Raylib.IsKeyPressed(key) && IsValidKey(key))
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
                {
                    return Convert.ToChar(key).ToString().ToUpper();
                }

                return Convert.ToChar(key).ToString().ToLower();
            }
        }

        return null;
    }

    private static bool IsValidKey(KeyboardKey key)
    {
        return (key >= KeyboardKey.KEY_A && key <= KeyboardKey.KEY_Z) ||
               (key >= KeyboardKey.KEY_ZERO && key <= KeyboardKey.KEY_NINE);
    }
}