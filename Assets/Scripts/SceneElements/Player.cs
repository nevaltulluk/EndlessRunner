using System;
using System.Timers;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float maxJumpHeight;

    public int multiplier;

    private EventBus _eventBus;
    private GameStateService _gameStateService;
    private DataService _dataService;
    
    private Vector3 _mouseButtonDownPosition;
    
    private float _jumpTimer;
    private float _initialYPosition;
    private int _frameBlock;
    private DataChangedEvent _dataChangedEvent = new DataChangedEvent();
    
    void Start()
    {
        _eventBus = Container.Instance.EventBus;
        _gameStateService = Container.Instance.GameStateService;
        _dataService = Container.Instance.DataService;
        
        _mouseButtonDownPosition = new Vector3();
        _initialYPosition = transform.localPosition.y;
        
        _eventBus.Subscribe<GameStartedEvent>(OnGameStarted);
    }
    
    void Update()
    {
        if (_frameBlock + 5 > Time.frameCount) return;
        if (!_gameStateService.IsPlaying) return;

        Vector3 position = transform.position;
        
        _jumpTimer += Time.deltaTime;
        
        if (Input.GetMouseButtonDown(0))
        {
            _mouseButtonDownPosition = Input.mousePosition;
            _jumpTimer = 0;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 offset = Input.mousePosition - _mouseButtonDownPosition;
            position = new Vector3(Mathf.Clamp(position.x + offset.x * Time.deltaTime * 2, -3.75f,3.75f), position.y, position.z);
            transform.position = position;
            _mouseButtonDownPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_jumpTimer < 0.25f && transform.localPosition.y <= _initialYPosition * 1.1)
            {
                body.AddForce(Vector3.up * multiplier);
                animator.Play("Jump");
                return;
            }
        }

        position = new Vector3(position.x, Mathf.Min(position.y, maxJumpHeight), position.z);
        transform.position = position;
        
        if (Math.Abs(position.y - maxJumpHeight) < 0.01)
        {
            body.velocity = Vector3.zero;
        }
    }

    private void OnGameStarted()
    {
        _dataService.UserData.CurrentScore = 0;
        _eventBus.Fire(_dataChangedEvent);
        
        _frameBlock = Time.frameCount;
        animator.Play("Running");
    }

    private void OnTriggerEnter(Collider other)
    {
        RoadElement element = other.gameObject.GetComponent<RoadElement>();
        if (element.type == RoadElementType.Coin)
        {
            _dataService.UserData.CoinAmount++;
            _eventBus.Fire(_dataChangedEvent);
            _dataService.Save();
            other.gameObject.SetActive(false);
        }
        else
        {
            animator.Play("Death");
            _eventBus.Fire(new GameOverEvent());
        }
    }

    private void FixedUpdate()
    {
        if (!_gameStateService.IsPlaying) return;

        _dataService.UserData.CurrentScore += Mathf.FloorToInt(Time.deltaTime * 200);
        _eventBus.Fire(_dataChangedEvent);
    }
}
