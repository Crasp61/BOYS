using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform _enemyCheck;
    [SerializeField] LayerMask _enemyLayer;

    private float _hitCoolDown = 1f;
    private float _CDtimer;

    [SerializeField] private int _playerDamage = 5;
    [SerializeField] private float _attackRange;

    private void Update()
    {
        attack();
    }

    public void attack()
    {
        if (_CDtimer <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Collider2D enemies = Physics2D.OverlapCircle(_enemyCheck.position, _attackRange, _enemyLayer);
                enemies.gameObject.GetComponent<Enemy>().TakeDamage(_playerDamage);
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
