using Raylib_cs;

public sealed class ScrapingMenuState : IState
{
    private readonly AppStateMachine _stateMachine;
    private readonly IUIFactory _uiFactory;
    
    private bool _checkURLButtonWasPressedPastFrame;
    private string _url = "";

    public ScrapingMenuState(AppStateMachine stateMachine, IUIFactory uiFactory)
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
        _uiFactory.CreateText("Put your youtube playlist link into text filed below:", yOffset: 150, Color.MAGENTA);

        // Input field
        InputField? inputField = _uiFactory.CreateInputField(350, 450, 50, _url, 20);
        UpdateInput(inputField);

        // Button
        Button? checkURLButton =
            _uiFactory.CreateButton("Scrap the link", Color.RED, Color.BLACK, yOffset: 500, 300, 75);
        if (checkURLButton is not null)
        {
            checkURLButton.Update();
            HandleButtonInteraction(checkURLButton);
        }
    }

    public void Exit()
    {
    }

    private void HandleButtonInteraction(Button checkURLButton)
    {
        if (_checkURLButtonWasPressedPastFrame && Raylib.IsMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON))
            TrySetURL();

        if (checkURLButton.IsPressed)
            _checkURLButtonWasPressedPastFrame = true;
        else
            _checkURLButtonWasPressedPastFrame = false;
    }

    private void TrySetURL()
    {
        try
        {
            PlaylistAPIService.TrySetPlaylistId(_url);
            _stateMachine.Enter<GetResultsState>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void UpdateInput(InputField? inputField)
    {
        if (inputField is not null)
        {
            inputField.Update();
            _url = inputField.URL;
        }
    }
}