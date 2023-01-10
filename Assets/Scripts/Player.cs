using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
public class Player : Creature
{
    private float _horizontal;                                                 //Вот здесь начинается Всё что связано с передвижением(бег,Рвывок,Прыжок)
    public static bool _isFacingRight = true;
    private bool _canDash = true;  //Вот этот отдел : переменные для рывка
    private bool _isDashing;
    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private float _jumpForce = 4f; //переменные для прыжка
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Transform WallCheck;

    private float _slideTime;
    private bool isTouchingWall;
    private bool wallSliding;
    private Vector2 _jumpAngle = new Vector2(30f, 10f);
    private bool _blocMove = false;

    [SerializeField] float wallSlidingSpeed;

    private int jumpCount;


    protected override void Update()                                                          
    {
        base.Update();
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
        slide();

        Debug.Log(_curentHealth);
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
        if (_blocMove == false)
        rb.velocity = new Vector2(_horizontal * _movementSpeed, rb.velocity.y);
    }


    private bool IsGrounded()                                                           //Методы прыжка
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, _groundLayer);
        
    }

    private bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, _groundLayer);
    }
    private void jump()
    {
        if (IsGrounded())
        {
            jumpCount = 1;
            _slideTime = 5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            jumpCount -= 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x * 100, rb.velocity.y * 0.5f);
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
            transform.localScale *= new Vector2(-1, 1);
            _isFacingRight = !_isFacingRight;
        }
    }

    private void slide()
    {
        if (IsTouchingWall() && IsGrounded() == false)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }
        if (_slideTime > 0)
        {
            if (wallSliding)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, Math.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _blocMove = true;
                    transform.localScale *= new Vector2(-1, 1);
                    _isFacingRight = !_isFacingRight;

                    rb.velocity = new Vector2(transform.localScale.x * _jumpAngle.x, _jumpAngle.y);
                }
                _slideTime -= Time.deltaTime;
            }
        }
        if (_blocMove && IsTouchingWall() || IsGrounded() || _horizontal != 0)
        {
            _blocMove = false;
        }
    }

}
