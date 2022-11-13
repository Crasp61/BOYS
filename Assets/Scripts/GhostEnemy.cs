using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Animations;

public class GhostEnemy : Enemy
{
    private Transform _target;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] Transform _enemyPos;
    [SerializeField] float _rangeOfDetection;

    protected override void Start()
    {
        base.Start();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (PlayerIsClose())
        {
            Move();
            SetAngle(_target.position);
        }
    }

    protected void Move()
    {
        transform.Translate(Vector2.down * _movementSpeed * Time.deltaTime);
    }
    
    private bool PlayerIsClose()
    {
        return Physics2D.OverlapCircle(_enemyPos.position, _rangeOfDetection, _playerLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_enemyPos.position, _rangeOfDetection);
    }
}
