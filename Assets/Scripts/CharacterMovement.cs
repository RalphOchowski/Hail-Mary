using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Transform PlayerOrientation;
    [SerializeField] float speed = 8.0f;
    [SerializeField] float JumpPower = 5.0f;
    [SerializeField] List<CinemachineVirtualCamera> PlayerCams;
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
        Walk();
    }

    void OnJump(InputValue value)
    {
        if (!gameObject.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed) rb.velocity += new Vector2(0f, JumpPower);
    }

    void OnMove(InputValue value)
    {
        HorizontalInput = value.Get<Vector2>();
    }

    void Walk()
    {
        Vector2 PlayerVelocity = new Vector2(HorizontalInput.x * speed, rb.velocity.y);
        rb.velocity = PlayerVelocity;
        FlipSprite();
    }

    void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (isPlayerMoving)
        {
            CharacterAnimator.SetBool("IsWalking", true);
            PlayerOrientation.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            if (rb.velocity.x < 0f)
            {
                PlayerCams[0].Priority = 0;
                PlayerCams[1].Priority = 1;
                PlayerCams[2].Priority = 0;
            }
            else if (rb.velocity.x > 0f)
            {
                PlayerCams[0].Priority = 0;
                PlayerCams[1].Priority = 0;
                PlayerCams[2].Priority = 1;
            }
        }
        else
        {
            CharacterAnimator.SetBool("IsWalking", false);
            PlayerCams[0].Priority = 1;
            PlayerCams[1].Priority = 0;
            PlayerCams[2].Priority = 0;
        }
    }
}
