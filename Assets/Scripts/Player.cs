
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    private EventBus _eventBus;
    private Vector3 mouseButtonDownPosition;
    private Vector3 initialPlayerPosition;
    void Start()
    {
        _eventBus = Container.Instance.GetEventBus();
        _eventBus.Subscribe<GameStartedEvent>(OnGameStarted);
        animator.Play("Idle");
        mouseButtonDownPosition = new Vector3();
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseButtonDownPosition = Input.mousePosition;
            
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 offset = Input.mousePosition - mouseButtonDownPosition;
            Vector3 position = transform.position;
            position = new Vector3(Mathf.Clamp(position.x + offset.x * Time.deltaTime * 2, -3.75f,3.75f), position.y, position.z);
            transform.position = position;
            mouseButtonDownPosition = Input.mousePosition;
        }
    }

    private void OnGameStarted()
    {
        animator.Play("Running");
    }

    private void OnTriggerEnter(Collider other)
    {
        RoadElement element = other.gameObject.GetComponent<RoadElement>();
        if (element.type == RoadElementType.Coin)
        {
            
        }
        else
        {
            animator.Play("Death");
            _eventBus.Fire(new GameOverEvent());
        }
    }
}
