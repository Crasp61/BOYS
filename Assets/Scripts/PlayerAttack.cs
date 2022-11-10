using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform _enemyCheck;
    [SerializeField] LayerMask _enemyLayer;

    private float _hitCoolDown = 0.4f;
    private float _CDtimer;

    [SerializeField] private int _playerDamage = 5;
    [SerializeField] private float _attackRange;
   
    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (_CDtimer <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(_enemyCheck.position, _attackRange, _enemyLayer);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].gameObject.GetComponent<Enemy>().TakeDamage(_playerDamage);
                    Console.WriteLine("hello");
                }
                _CDtimer = _hitCoolDown;
            }
        }
        else
        {
            _CDtimer -= Time.deltaTime;
        }
    }

    public void SetCharateristics(int damage, float range, float CD)
    {
        _hitCoolDown = CD;
        _playerDamage = damage;
        _attackRange = range;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_enemyCheck.position, _attackRange);
    }
}
