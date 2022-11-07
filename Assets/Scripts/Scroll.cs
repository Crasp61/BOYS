using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private float _horizontal;
    private float _speed= 0.2f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform LeftMiddleGroundCheck;
    [SerializeField] Transform RightMiddleGroundCheck;
    [SerializeField] Transform LeftDownGroundCheck;
    [SerializeField] Transform RightDownGroundCheck;
    [SerializeField] Transform LeftHightGroundCheck;
    [SerializeField] Transform RightHightGroundCheck;
    [SerializeField] protected LayerMask _groundLayer;
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (IsMiddleleftGrounded() || IsMiddleRightGrounded() || IsDownleftGrounded() || IsDownRightGrounded() || IsHightleftGrounded() || IsHightRightGrounded())
        {
            rb.velocity = new Vector2(_horizontal * _speed * 0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(_horizontal * _speed * -1, rb.velocity.y);
        }
    }
    private bool IsMiddleleftGrounded()
    {
        return Physics2D.OverlapCircle(LeftMiddleGroundCheck.position, 0.05f, _groundLayer);
    }
    private bool IsMiddleRightGrounded()
    {
        return Physics2D.OverlapCircle(RightMiddleGroundCheck.position, 0.05f, _groundLayer);
    }
    private bool IsDownleftGrounded()
    {
        return Physics2D.OverlapCircle(LeftDownGroundCheck.position, 0.05f, _groundLayer);
    }
    private bool IsDownRightGrounded()
    {
        return Physics2D.OverlapCircle(RightDownGroundCheck.position, 0.05f, _groundLayer);
    }
    private bool IsHightleftGrounded()
    {
        return Physics2D.OverlapCircle(LeftHightGroundCheck.position, 0.05f, _groundLayer);
    }
    private bool IsHightRightGrounded()
    {
        return Physics2D.OverlapCircle(RightHightGroundCheck.position, 0.05f, _groundLayer);
    }
}

