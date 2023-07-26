using Raylib_cs;

public sealed class FailedMenuState : IPayloadedState<string>
{
    private readonly AppStateMachine _stateMachine;
    private readonly IUIFactory _uiFactory;

    private string _errorText = "Error message wasn't set";
    private bool _checkURLButtonWasPressedPastFrame;

    public FailedMenuState(AppStateMachine stateMachine, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
    }

    public void Enter(string errorMessage)
    {
        _errorText = GetFormattedErrorMessage(errorMessage);
    }

    public void Update()
    {
        // Text
        _uiFactory.CreateText(_errorText, 100, Color.MAROON, 30);

        // Button
        Button? checkURLButton =
            _uiFactory.CreateButton("Scrap the link", Color.RED, Color.BLACK, yOffset: 500, 300, 75);
        if (checkURLButton is not null)
        {
            checkURLButton.Update();

            if (_checkURLButtonWasPressedPastFrame && Raylib.IsMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON))
                _stateMachine.Enter<ScrapingMenuState>();

            if (checkURLButton.IsPressed)
                _checkURLButtonWasPressedPastFrame = true;
            else
                _checkURLButtonWasPressedPastFrame = false;
        }
    }

    public void Exit()
    {
    }

    private string GetFormattedErrorMessage(string errorMessage, int stringLength = 30)
    {
        string formattedMessage = "";

        for (int i = 0; i < errorMessage.Length; i += stringLength)
        {
            int remainingChars = errorMessage.Length - i;
            int charsToTake = Math.Min(stringLength, remainingChars);
            formattedMessage += errorMessage.Substring(i, charsToTake);

            if (remainingChars > charsToTake)
                formattedMessage += "\n";
        }

        return formattedMessage;
    }
}