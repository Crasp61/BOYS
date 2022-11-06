using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player : Creature
{
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private bool isGround = false;
    private float _horizontal;
    private bool _isFacingRight = true;
    
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

        Flip();
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            return;
        }
        jump();
        FixedMove();
    }

    protected override void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedMove()
    {
        rb.velocity = new Vector2(_horizontal * _movementSpeed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
    private void jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            rb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
        }
    }

    private bool _canDash = true;
    private bool _isDashing;
    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;

    private IEnumerator Dash()
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

    private void Flip()
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
