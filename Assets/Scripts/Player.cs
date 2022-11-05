using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private bool isGround = false;
    [SerializeField] private float _dashForce;
    private float _timer;
    [SerializeField] private float _dashCoolDown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
            isGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
            isGround = false;
    }

    private void FixedUpdate()
    {
        Move();
        Dash();
        jump();
    }

     protected override void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * _movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * _movementSpeed * Time.deltaTime);
        }
    }

    private void jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            rb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
        }
    }

    protected void Dash()
    {
        
    }
}
