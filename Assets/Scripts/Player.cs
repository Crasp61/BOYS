using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player : Creature
{
    private float _horizontal;                                                 //Вот здесь начинается Всё что связано с передвижением(бег,Рвывок,Прыжок)
    private bool _isFacingRight = true;

    private bool _canDash = true;  //Вот этот отдел : переменные для рывка
    private bool _isDashing;
    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private float _jumpForce = 4f; //переменные для прыжка
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask _groundLayer;

    private void Update()                                                          
    {
        if (_isDashing)
        {
            return;
        }

        Move();

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }

        jump();

        Flip();

    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            return;
        }
        FixedMove();
    }

    protected void Move()                                                               //Методы движения
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
    }
    private void FixedMove()
    {

        rb.velocity = new Vector2(_horizontal * _movementSpeed, rb.velocity.y);
    }


    private bool IsGrounded()                                                           //Методы прыжка
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, _groundLayer);
    }
    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }


    private IEnumerator Dash()                                                          //Методы рывка
    {
        _canDash = false;
        _isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        _isDashing = false;
        yield return new WaitForSeconds(_dashingCoolDown);
        _canDash = true;
    }

    private void Flip()                                                                 //метод поворота персонажа(по большей части для рывка)
    {
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {

            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }

}
