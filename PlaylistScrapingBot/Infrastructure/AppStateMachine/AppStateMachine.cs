using Raylib_cs;

public sealed class AppStateMachine
{
    private readonly Dictionary<Type, IUpdatableState> _states;

    private IUpdatableState? _activeState;

    public AppStateMachine(IUIFactory uiFactory)
    {
        _states = new Dictionary<Type, IUpdatableState>
        {
            [typeof(MainMenuState)] = new MainMenuState(this, uiFactory),
            [typeof(ScrapingMenuState)] = new ScrapingMenuState(this, uiFactory),
            [typeof(GetResultsState)] = new GetResultsState(this, uiFactory),
            [typeof(FailedMenuState)] = new FailedMenuState(this, uiFactory),
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state?.Enter();
    }

    public void Enter<TState, TPayLoad>(TPayLoad payload) where TState : class, IPayloadedState<TPayLoad>
    {
        TState state = ChangeState<TState>();
        state?.Enter(payload);
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DARKGRAY);

            if (_activeState is null)
                Console.WriteLine("Current state is null!");
            else
                _activeState.Update();

            Raylib.EndDrawing();
        }
    }

    private TState ChangeState<TState>() where TState : class, IUpdatableState
    {
        _activeState?.Exit();

        TState state = GetState<TState>();
        _activeState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IUpdatableState =>
        _states[typeof(TState)] as TState ?? throw new ArgumentNullException("State wasn't found");
}