using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player : Creature
{
    protected float _horizontal;
    private bool _isFacingRight = true;

    private bool _canDash = true;
    private bool _isDashing;
    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCoolDown = 1f;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] private float _jumpForce = 4f;


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
        _ghostTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            return;
        }
        FixedMove();
    }

    protected void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedMove()
    {

        rb.velocity = new Vector2(_horizontal * _movementSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
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

    private float _ghostHitCoolDown=1;
    private float _ghostTimer;
    private int _ghostDamage=5;
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<GhostEnemy>() && _ghostTimer <= 0)
        {
            TakeDamage(_ghostDamage);
            _ghostTimer = _ghostHitCoolDown;
        }
    }
}
