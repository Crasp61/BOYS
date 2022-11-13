using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Animations;

public class GhostEnemy : Enemy
{
    [SerializeField] Transform _playerPos;
    [SerializeField] Transform _enemyPos;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] private float _detectRange;
    private bool playerToTheRight = false;
    private bool playerToTheLeft = false;
    private bool playerLower = false;
    private bool playerHigher = false;

    private bool _playerDetectedMark = false;
    private void Update()
    {
        if(_enemyPos.position.x > _playerPos.position.x)
        {
            playerToTheLeft = true;
            playerToTheRight = false;
        }
        if(_enemyPos.position.x < _playerPos.position.x)
        {
            playerToTheLeft = false;
            playerToTheRight = true;
        }
        if (_enemyPos.position.y > _playerPos.position.y)
        {
            playerLower = true;
            playerHigher = false;
        }
        if (_enemyPos.position.y < _playerPos.position.y)
        {
            playerLower = false;
            playerHigher = true;
        }
    }

    private void FixedUpdate()
    {
        if (PlayerClose()) 
        {
            _playerDetectedMark = true; 
        }
        if(_playerDetectedMark)
        {
            _movementSpeed = 1f;
            if (playerToTheLeft)
            {
                rb.velocity = new Vector2(_movementSpeed * -1, rb.velocity.y);

            }
            if (playerToTheRight)
            {
                rb.velocity = new Vector2(_movementSpeed, rb.velocity.y);
            }
            if (playerLower)
            {
                rb.velocity = new Vector2(rb.velocity.x, _movementSpeed * -1);
            }
            if (playerHigher)
            {
                rb.velocity = new Vector2(rb.velocity.x, _movementSpeed);
            }
        }
        else
        {
            _movementSpeed = 0f;
        }
    }
    private bool PlayerClose()
    {
        return Physics2D.OverlapCircle(_enemyPos.position, _detectRange, _playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_enemyPos.position, _detectRange);
    }
}
