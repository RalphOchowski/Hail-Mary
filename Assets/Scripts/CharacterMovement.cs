using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Transform PlayerOrientation;
    [SerializeField] float speed = 8.0f;
    [SerializeField] float JumpPower = 5.0f;
    Rigidbody2D rb;
    Animator CharacterAnimator;
    Vector2 HorizontalInput;
    bool IsFacingRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CharacterAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //HorizontalMovement = Input.GetAxisRaw("Horizontal");
        Walk();
    }


    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, JumpPower);
        }
    }
    void Walk()
    {
        Vector2 PlayerVelocity = new Vector2(HorizontalInput.x * speed, rb.velocity.y);
        rb.velocity = PlayerVelocity;
    }

    void OnMove(InputValue value)
    {

        HorizontalInput = value.Get<Vector2>();
    }

    void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(isPlayerMoving) PlayerOrientation.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
    }
}
