using Google.Apis.YouTube.v3.Data;
using Raylib_cs;

public sealed class GetResultsState : IState
{
    private readonly AppStateMachine _stateMachine;
    private readonly IUIFactory _uiFactory;
    private readonly CancellationTokenSource _tokenSource = new();
    private readonly CancellationToken _token;

    private List<string> _responceContainer = new();
    private bool _backToScrapingMenuButtonPressed;
    private Task<PlaylistItemListResponse>? _responceTask;

    public GetResultsState(AppStateMachine stateMachine, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
        _token = _tokenSource.Token;
    }

    public async void Enter()
    {
        try
        {
            _responceTask = Task.Run(() => PlaylistAPIService.RequestPlaylistData(_token));
            var responceData = await _responceTask;
            SetDataToResponceContainer(responceData);
        }
        catch (Exception ex)
        {
            _stateMachine.Enter<FailedMenuState, string>(ex.Message);
        }
    }

    public void Update()
    {
        DisplayResponceText();

        Button? backToScrapingMenuButton =
            _uiFactory.CreateButton("Get back to craping menu", Color.RED, Color.BLACK, yOffset: 625, 300, 75);
        if (backToScrapingMenuButton is not null)
        {
            backToScrapingMenuButton.Update();
            HandleButtonInteraction(backToScrapingMenuButton);
        }
    }

    public void Exit()
    {
    }

    private void DisplayResponceText()
    {
        if (_responceContainer.Count > 0)
            _uiFactory.CreateText(GetResponceText(), yOffset: 150, Color.GREEN, 15);
        else
            _uiFactory.CreateText("Waiting for server response...", yOffset: 150, Color.MAGENTA, 15);
    }

    private string GetResponceText()
    {
        string responceText = "";

        for (var i = 0; i < _responceContainer.Count; i++)
            responceText += $"Video #{i} has name \"{_responceContainer[i]}\"\n";

        return responceText;
    }

    private void HandleButtonInteraction(Button backToScrapingMenuButton)
    {
        if (_backToScrapingMenuButtonPressed && Raylib.IsMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON))
        {
            CancelResponceTaskIfRunning();
            _stateMachine.Enter<ScrapingMenuState>();
        }

        if (backToScrapingMenuButton.IsPressed)
            _backToScrapingMenuButtonPressed = true;
        else
            _backToScrapingMenuButtonPressed = false;
    }

    private void CancelResponceTaskIfRunning()
    {
        if (_responceTask?.Status == TaskStatus.Running)
            _tokenSource.Cancel();
    }

    private void SetDataToResponceContainer(PlaylistItemListResponse response)
    {
        foreach (var item in response.Items)
            _responceContainer.Add(item.Snippet.Title);
    }
}