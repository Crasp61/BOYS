using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private List<Weapon> weapon = new List<Weapon>() { new Axe(1f, 20, 0.8f), new Sword(0.5f, 12, 0.43f), new Dagger(0.3f, 8, 0.3f) };
    public List<GameObject> equipedWeapon = new List<GameObject>() {null } ;

    private string _weaponToTakeTag = null;
    
    [SerializeField] private GameObject _axeGameObject;
    [SerializeField] private GameObject _swordGameObject;
    [SerializeField] private GameObject _daggerGameObject;
    [SerializeField] Transform _weaponSpawn;

    [SerializeField] Transform _enemyCheck;
    [SerializeField] LayerMask _enemyLayer;

    private float _hitCoolDown = 0.5f;
    private float _CDtimer;

    [SerializeField] private int _playerDamage = 5;
    [SerializeField] private float _attackRange = 0.43f;

    private void Update()
    {
        Attack();
        ChangeWeapons();
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
                }
                _CDtimer = _hitCoolDown;
            }
        }
        else
        {
            _CDtimer -= Time.deltaTime;
        }
    }

    public void ChangeWeapons()
    {
        if (_weaponToTakeTag != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_weaponToTakeTag == "Axe")
                {
                    SetCharateristics(weapon[0].WeaponCD, weapon[0].WeaponDamage, weapon[0].WeaponAttackrange);
                    if (equipedWeapon.Count > 0)
                    {
                        Instantiate(equipedWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedWeapon.Clear();
                    equipedWeapon.Add(_axeGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("Axe"));
                }
                if (_weaponToTakeTag == "Sword")
                {
                    SetCharateristics(weapon[1].WeaponCD, weapon[1].WeaponDamage, weapon[1].WeaponAttackrange);
                    if (equipedWeapon.Count > 0)
                    {
                        Instantiate(equipedWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedWeapon.Clear();
                    equipedWeapon.Add(_swordGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("Sword"));
                }
                if (_weaponToTakeTag == "Dagger")
                {
                    SetCharateristics(weapon[2].WeaponCD, weapon[2].WeaponDamage, weapon[2].WeaponAttackrange);
                    if (equipedWeapon.Count > 0)
                    {
                        Instantiate(equipedWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedWeapon.Clear();
                    equipedWeapon.Add(_daggerGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("Dagger"));
                }
            }
        }
    }

    public void SetCharateristics(float CD, int damage , float range)
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
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        _weaponToTakeTag = other.gameObject.tag;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        _weaponToTakeTag = null;
    }
}
