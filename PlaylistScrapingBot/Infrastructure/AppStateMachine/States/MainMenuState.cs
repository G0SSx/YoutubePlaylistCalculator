using Raylib_cs;

public sealed class MainMenuState : IState
{
    private readonly AppStateMachine _stateMachine;
    private readonly IUIFactory _uiFactory;

    private bool _startJorneyButtonWasPressedPastFrame;

    public MainMenuState(AppStateMachine stateMachine, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        // Text
        _uiFactory.CreateText("Playlist scraping bot welcomes you!", 150, Color.YELLOW, 30);

        // Button
        Button? startJorneyButton = _uiFactory.CreateButton("Start jorney", Color.RED, Color.BLACK, 400, 300, 75);
        if (startJorneyButton is not null)
        {
            startJorneyButton.Update();

            if (_startJorneyButtonWasPressedPastFrame && Raylib.IsMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON))
                HandleStartJorneyButtonPressed();

            if (startJorneyButton.IsPressed)
                _startJorneyButtonWasPressedPastFrame = true;
            else
                _startJorneyButtonWasPressedPastFrame = false;
        }
    }

    public void Exit()
    {
    }

    private void HandleStartJorneyButtonPressed()
    {
        _stateMachine.Enter<ScrapingMenuState>();
    }
}