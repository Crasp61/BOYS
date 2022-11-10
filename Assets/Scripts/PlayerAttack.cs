using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Collider2D _equipedWeapon = null;
    [SerializeField] Transform _weaponSpawn;

    [SerializeField] Transform _enemyCheck;
    [SerializeField] LayerMask _enemyLayer;

    private float _hitCoolDown = 0.5f;
    private float _CDtimer;

    [SerializeField] private int _playerDamage = 5;
    [SerializeField] private float _attackRange = 0.43f;

    private string _tagOfWeapon;
    private string _tagOfEquipedWeapon;

    private float _axeCD = 1f;
    private int _axeDamage = 20;
    private float _axeAttackRange = 0.8f;

    private float _swordCD = 0.5f;
    private int _swordDamage = 12;
    private float _swordAttackRange = 0.43f;

    private float _daggerCD = 0.3f;
    private int _daggerDamage = 8;
    private float _daggerAttackRange = 0.3f;

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
    
    public void OnTriggerStay2D(Collider2D collider)
    {
        _tagOfWeapon = collider.gameObject.tag;
        if (_tagOfWeapon == "Axe")
        {
            if (Input.GetKey(KeyCode.E))
            {
                SetCharateristics(_axeDamage, _axeAttackRange, _axeCD);
                Destroy(collider.gameObject);
            }
        }
        if (_tagOfWeapon == "Sword")
        {
            if (Input.GetKey(KeyCode.E))
            {
                SetCharateristics(_swordDamage, _swordAttackRange, _swordCD);
                Destroy(collider.gameObject);
            }
        }
        if (_tagOfWeapon == "Dagger")
        {
            if (Input.GetKey(KeyCode.E))
            {
                SetCharateristics(_daggerDamage, _daggerAttackRange, _daggerCD);
                Destroy(collider.gameObject);
            }
        }
    }
}
