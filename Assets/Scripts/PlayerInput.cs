using System;
using UnityEngine.Events;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    public event Action OnInteractKeyPressed = delegate { };

    public UnityEvent TestEvent;

    private void Start() {
        if (TestEvent == null) {
            TestEvent = new UnityEvent();
        }
    }

    private void Update() {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnInteractKeyPressed();
        }
    }
}
