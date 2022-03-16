using TMPro;
using UnityEngine;

public class HighScoreUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI HighScoreText;
    
    private DataService _dataService;
    private EventBus _eventBus;
    
    private void Start()
    {
        _dataService = Container.Instance.DataService;
        _eventBus = Container.Instance.EventBus;
        
        HighScoreText.text = _dataService.UserData.HighScore.ToString();
        
        _eventBus.Subscribe<DataChangedEvent>(OnDataChangedEvent);
        _eventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        _eventBus.Subscribe<GameOverEvent>(OnGameOver);
    }

    private void OnDataChangedEvent()
    {
        if (_dataService.UserData.HighScore < _dataService.UserData.CurrentScore)
        {
            _dataService.UserData.HighScore = _dataService.UserData.CurrentScore;
            _dataService.Save();
        }
        
        HighScoreText.text = _dataService.UserData.HighScore.ToString();
    }

    private void OnGameStarted()
    {
        gameObject.SetActive(false);
    }   
    
    private void OnGameOver()
    {
        gameObject.SetActive(true);
    }
}