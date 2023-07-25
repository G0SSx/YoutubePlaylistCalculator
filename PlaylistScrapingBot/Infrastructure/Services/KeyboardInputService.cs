using Raylib_cs;

public static class KeyboardInputService
{
    public static char? GetPressedButtonValues()
    {
        char? input = null;

        foreach (KeyboardKey key in Enum.GetValues(typeof(KeyboardKey)))
        {
            if (Raylib.IsKeyPressed(key) && IsValidKey(key))
            {
                input = Convert.ToChar(key);
                Console.WriteLine(input);
                break;
            }
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_SEMICOLON) && 
            Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
        {
            input = ':';
            Console.WriteLine(input);
        }

        return input;
    }

    private static bool IsValidKey(KeyboardKey key)
    {
        return (key >= KeyboardKey.KEY_A && key <= KeyboardKey.KEY_Z) ||
               (key >= KeyboardKey.KEY_ZERO && key <= KeyboardKey.KEY_NINE) ||
               key == KeyboardKey.KEY_SLASH ||
               key == KeyboardKey.KEY_PERIOD;
    }
}