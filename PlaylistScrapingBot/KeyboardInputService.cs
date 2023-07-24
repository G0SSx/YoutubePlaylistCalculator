using Raylib_cs;

public class KeyboardInputService
{
    private static Dictionary<KeyboardKey, bool>? _keyboardButtons;
    private static bool _colonWasPressed;

    public KeyboardInputService()
    {
        _keyboardButtons = new Dictionary<KeyboardKey, bool>
        {
            { KeyboardKey.KEY_A, false },
            { KeyboardKey.KEY_B, false },
            { KeyboardKey.KEY_C, false },
            { KeyboardKey.KEY_D, false },
            { KeyboardKey.KEY_E, false },
            { KeyboardKey.KEY_F, false },
            { KeyboardKey.KEY_G, false },
            { KeyboardKey.KEY_H, false },
            { KeyboardKey.KEY_I, false },
            { KeyboardKey.KEY_J, false },
            { KeyboardKey.KEY_K, false },
            { KeyboardKey.KEY_L, false },
            { KeyboardKey.KEY_M, false },
            { KeyboardKey.KEY_N, false },
            { KeyboardKey.KEY_O, false },
            { KeyboardKey.KEY_P, false },
            { KeyboardKey.KEY_Q, false },
            { KeyboardKey.KEY_R, false },
            { KeyboardKey.KEY_S, false },
            { KeyboardKey.KEY_T, false },
            { KeyboardKey.KEY_U, false },
            { KeyboardKey.KEY_V, false },
            { KeyboardKey.KEY_W, false },
            { KeyboardKey.KEY_X, false },
            { KeyboardKey.KEY_Y, false },
            { KeyboardKey.KEY_Z, false },
            { KeyboardKey.KEY_ZERO, false },
            { KeyboardKey.KEY_ONE, false },
            { KeyboardKey.KEY_TWO, false },
            { KeyboardKey.KEY_THREE, false },
            { KeyboardKey.KEY_FOUR, false },
            { KeyboardKey.KEY_FIVE, false },
            { KeyboardKey.KEY_SIX, false },
            { KeyboardKey.KEY_SEVEN, false },
            { KeyboardKey.KEY_EIGHT, false },
            { KeyboardKey.KEY_NINE, false },
            { KeyboardKey.KEY_LEFT_SHIFT, false},
            { KeyboardKey.KEY_SLASH, false},
            { KeyboardKey.KEY_PERIOD, false},
            { KeyboardKey.KEY_SEMICOLON, false},
        };
    }

    public static char? GetPressedButtonValues()
    {
        foreach (KeyboardKey key in Enum.GetValues(typeof(KeyboardKey)))
        {
            if (Raylib.IsKeyPressed(key) && IsValidKey(key))
                return Convert.ToChar(key);
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_SEMICOLON) && 
            Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT) && 
            _colonWasPressed == false)
        {
            _colonWasPressed = true;
            return ':';
        }
        else
            _colonWasPressed = false;

        return null;
    }

    static bool IsValidKey(KeyboardKey key)
    {
        return (key >= KeyboardKey.KEY_A && key <= KeyboardKey.KEY_Z) ||
               (key >= KeyboardKey.KEY_ZERO && key <= KeyboardKey.KEY_NINE) ||
               key == KeyboardKey.KEY_SLASH ||
               key == KeyboardKey.KEY_PERIOD ||
               (Raylib.IsKeyPressed(KeyboardKey.KEY_SEMICOLON) && Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT_SHIFT));
    }
}