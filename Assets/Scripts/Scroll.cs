using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
        if (IsGr(LeftMiddleGroundCheck, 0.05f, _groundLayer) || IsGr(RightMiddleGroundCheck, 0.05f, _groundLayer) || IsGr(LeftDownGroundCheck, 0.05f, _groundLayer) ||
          IsGr(RightDownGroundCheck, 0.05f, _groundLayer) || IsGr(LeftHightGroundCheck, 0.05f, _groundLayer) || IsGr(RightHightGroundCheck, 0.05f, _groundLayer))
        {
            rb.velocity = new Vector2(_horizontal * _speed * 0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(_horizontal * _speed * -1, rb.velocity.y);
        }
    }
    private bool IsGr(Transform transform, float rad, LayerMask layer)
    {
        return Physics2D.OverlapCircle(transform.position, rad, layer);
    }
   
}


