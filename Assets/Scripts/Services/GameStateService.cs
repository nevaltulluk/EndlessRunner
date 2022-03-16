public class GameStateService
{
    private EventBus _eventBus;

    public bool IsPlaying;
    public GameStateService()
    {
        _eventBus = Container.Instance.EventBus;
        
        _eventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        _eventBus.Subscribe<GameOverEvent>(OnGameOver);
    }

    private void OnGameStarted()
    {
        IsPlaying = true;
    }  
    
    private void OnGameOver()
    {
        IsPlaying = false;
    }
}
