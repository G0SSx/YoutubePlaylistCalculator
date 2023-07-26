// Services
IUIFactory uiFactory = new UIFactory();

// Logic elements
Window window = uiFactory.CreateWindow(1000, 800);
AppStateMachine appStateMachine = new(uiFactory);
appStateMachine.Enter<MainMenuState>();

// App logic
window.Init();
appStateMachine.Run();
window.Close();