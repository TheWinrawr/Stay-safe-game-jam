using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerInput))]
public class Player : MonoBehaviour {

    public Animator animator;
    public float moveSpeed = 6;

    private float interactBoxSkinWidth = 0.2f;

    private PlayerInput input;
    private Rigidbody2D rb;
    private new BoxCollider2D collider;

    private bool canUseInput;

    private void Awake() {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Start () {
        input.OnInteractKeyPressed += OnInteractKeyPressed;
        canUseInput = true;
    }

    private void FixedUpdate() {
        if (canUseInput) {
            MovePlayer();
        }
    }

    private void MovePlayer() {
        animator.SetFloat("Horizontal", input.Horizontal);
        animator.SetBool("IsMovingSideways", input.Horizontal != 0);

        Vector2 velocity = new Vector2(input.Horizontal * moveSpeed, input.Vertical * moveSpeed);
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
        if (input.Horizontal != 0) {
            float rotation = (input.Horizontal < 0) ? 0 : 180;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    private void OnInteractKeyPressed() {
        if (!canUseInput) {
            return;
        }
        LayerMask mask = LayerMask.GetMask("interactable");
        Vector2 castSize = new Vector2(collider.size.x + interactBoxSkinWidth, collider.size.y + interactBoxSkinWidth);
        Collider2D hit = Physics2D.OverlapBox(rb.position, castSize, 0f, mask);
        if (hit != null) {
            IInteractable interactable = hit.GetComponentInChildren<IInteractable>();
            if (interactable != null) {
                interactable.DoAction();
            }
        }
    }

    public void DisableInput() {
        canUseInput = false;
    }

    public void EnableInput() {
        canUseInput = true;
    }
}
