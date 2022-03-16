using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlay : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    

    private EventBus _eventBus;
    void Start()
    {
        _eventBus = Container.Instance.EventBus;
        button.onClick.AddListener(OnButtonClicked);
        _eventBus.Subscribe<GameOverEvent>(OnGameOver);
    }

    private void OnButtonClicked()
    {
        fade.enabled = false;
        text.gameObject.SetActive(false);
        _eventBus.Fire(new GameStartedEvent());
    }

    private void OnGameOver()
    {
        fade.enabled = true;
        text.gameObject.SetActive(true);
    }
}
