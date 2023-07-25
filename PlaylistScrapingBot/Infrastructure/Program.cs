IUIFactory uiFactory = new UIFactory();
Window window = uiFactory.CreateWindow(1000, 800);
AppStateMachine appStateMachine = new(uiFactory);
appStateMachine.Enter<MainMenuState>();

window.Init();
appStateMachine.Run();
window.Close();