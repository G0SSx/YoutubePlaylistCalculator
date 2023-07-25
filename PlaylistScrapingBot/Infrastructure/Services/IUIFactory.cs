using Raylib_cs;

public interface IUIFactory
{
    Window CreateWindow(int width, int height);
    void CreateText(string text, int yOffset, Color color, int textSize = 20);
    InputField? CreateInputField(int yOffset, int width, int height, int textSize = 15);
    Button? CreateButton(string text, Color buttonColor, Color textColor, int yOffset, int width,
        int height, int textSize = 20);
}