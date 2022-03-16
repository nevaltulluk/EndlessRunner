using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    
    private DataService _dataService;
    private EventBus _eventBus;
    
    private void Start()
    {
        _dataService = Container.Instance.DataService;
        _eventBus = Container.Instance.EventBus;
        
        ScoreText.text = _dataService.UserData.CurrentScore.ToString();
        
        _eventBus.Subscribe<DataChangedEvent>(OnDataChangedEvent);
        _eventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        _eventBus.Subscribe<GameOverEvent>(OnGameOver);
        
        gameObject.SetActive(false);
    }

    private void OnDataChangedEvent()
    {
        ScoreText.text = _dataService.UserData.CurrentScore.ToString();
    }
    
    private void OnGameStarted()
    {
        gameObject.SetActive(true);
    }   
    
    private void OnGameOver()
    {
        gameObject.SetActive(false);
    }
}