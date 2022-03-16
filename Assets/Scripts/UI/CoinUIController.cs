using TMPro;
using UnityEngine;

public class CoinUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI CoinText;
    
    private DataService _dataService;
    private EventBus _eventBus;
    
    private void Start()
    {
        _dataService = Container.Instance.DataService;
        _eventBus = Container.Instance.EventBus;
        
        CoinText.text = _dataService.UserData.CoinAmount.ToString();

        _eventBus.Subscribe<DataChangedEvent>(OnDataChangedEvent);
    }

    private void OnDataChangedEvent()
    {
        CoinText.text = _dataService.UserData.CoinAmount.ToString();
    }
}
