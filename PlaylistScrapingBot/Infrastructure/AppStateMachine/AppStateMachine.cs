using Raylib_cs;
using System.Numerics;

public sealed class AppStateMachine
{
    private readonly Dictionary<Type, IState> _states;

    private IState? _state;

    public AppStateMachine(IUIFactory uiFactory)
    {
        _states = new Dictionary<Type, IState>
        {
            [typeof(MainMenuState)] = new MainMenuState(this, uiFactory),
            [typeof(ScrapingMenuState)] = new ScrapingMenuState(this, uiFactory),
        };
    }

    public void Enter<TStateType>() where TStateType : IState
    {
        _state?.Exit();
        _state = _states[typeof(TStateType)];
        _state?.Enter();
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DARKGRAY);

            if (_state is null)
                Console.WriteLine("Current state is null!");
            else
                _state.Update();

            Raylib.EndDrawing();
        }
    }
}