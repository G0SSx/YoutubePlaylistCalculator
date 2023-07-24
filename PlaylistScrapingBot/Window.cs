using Raylib_cs;
using System.Numerics;

public sealed class Window
{
    private enum RenderingState { Menu, Scraping, Results, Failed }
    
    private readonly int _width;
    private readonly int _height;

    private RenderingState _state;

    public Window(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void Run()
    {
        Raylib.InitWindow(_width, _height, "Playlist calculator");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            switch (_state)
            {
                case RenderingState.Menu:
                    ShowMainMenu();
                    break;
                case RenderingState.Scraping:
                    ShowScrapingMenu();
                    break;
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private void ShowMainMenu()
    {
        // ELEMENTS

        var startJorneyButton = CentralizedXButton("Start jorney", Color.RED, Color.BLACK, 400, 150, 75);

        // LOGIC

        if (startJorneyButton.isPresed)
            _state = RenderingState.Scraping;

        // RENDERING

        Raylib.ClearBackground(Color.DARKGRAY);

        CentralizedXText("Best ever text you'll ever see!", 50, Color.YELLOW);

        startJorneyButton.button.Update(startJorneyButton.isPresed);
    }

    private void ShowScrapingMenu()
    {
        Raylib.ClearBackground(Color.BLACK);
    }

    private (Button button, bool isPresed) CentralizedXButton(string text, Color buttonColor, Color textColor, int yOffset, int width, 
        int height, int textSize = 20)
    {
        Rectangle rect = new(_width / 2 - width / 2, yOffset, width, height);
        Button button = new(rect, buttonColor, text, textColor, textSize);

        bool isPressed = false;
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                isPressed = true;
        }

        return (button, isPressed);
    }

    private void CentralizedXText(string text, int yOffset, Color color, int textSize = 20)
    {
        Vector2 position = new(GetCentralizedTextXPosition(text, textSize), yOffset);

        Text.Draw(text, position, textSize, color);
    }

    private int GetCentralizedTextXPosition(string text, int size) => (_width - Raylib.MeasureText(text, size)) / 2;
}