using Raylib_cs;

public class ScrapingMenuState : IState
{
    private readonly AppStateMachine _stateMachine;
    private readonly IUIFactory _uiFactory;

    private InputField? _inputField;
    private Button? _checkIfURLValidButton;

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
        _inputField = _uiFactory.CreateInputField(350, 450, 50, 20);
        _inputField?.Update();

        // Button
        _checkIfURLValidButton = _uiFactory.CreateButton("Scrap the link", Color.RED, Color.BLACK, 500, 150, 75);
        if (_checkIfURLValidButton is not null)
        {
            _checkIfURLValidButton.Update();

            if (_checkIfURLValidButton.IsPressed)
                CheckIfURLValid();
        }
    }

    public void Exit()
    {
    }

    private void CheckIfURLValid()
    {
        // check url with URLService
    }
}