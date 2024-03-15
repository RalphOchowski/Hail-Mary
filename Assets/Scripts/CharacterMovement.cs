using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Transform PlayerOrientation;
    [SerializeField] float speed = 8.0f;
    [SerializeField] float JumpPower = 5.0f;
    Rigidbody2D Rigidbody;
    Animator CharacterAnimator;
    float HorizontalMovement;
    float VerticalMovement;
    bool IsFacingRight;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        CharacterAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        Movement();
    }

    void Movement()
    {
        FlipSprite();
        Rigidbody.velocity = new Vector2(HorizontalMovement * speed, Rigidbody.velocity.y);
        if(HorizontalMovement != 0) CharacterAnimator.SetBool("IsWalking", true);
        else CharacterAnimator.SetBool("IsWalking", false);
        /*if (gameObject.GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        else if(Input.GetButtonDown("Jump")) Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, JumpPower);*/
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Rigidbody.velocity += new Vector2(0f, JumpPower);
        }
    }

    void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(Rigidbody.velocity.x) > Mathf.Epsilon;
        if(isPlayerMoving) PlayerOrientation.localScale = new Vector2(Mathf.Sign(Rigidbody.velocity.x), 1f);
    }
}
