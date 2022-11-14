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
    private List<GameObject> equipedWeapon = new List<GameObject>() { } ;

    private string _meeleWeaponToTakeTag = null;
    
    [SerializeField] private GameObject _axeGameObject;
    [SerializeField] private GameObject _swordGameObject;
    [SerializeField] private GameObject _daggerGameObject;
    [SerializeField] private Transform _weaponSpawn;

    public int _bowDamage = 10;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Transform _pointToshoot;
    private float _timeToReload = 1f;
    private float _bowTimer;

    [SerializeField] private Transform _enemyCheck;
    [SerializeField] private LayerMask _enemyLayer;

    private float _hitCoolDown = 0.5f;
    private float _CDtimer;

    [SerializeField] private int _playerDamage = 5;
    [SerializeField] private float _attackRange = 0.43f;

    private void Update()
    {
        MeeleAttack();
        ChangeWeapons();
        RangeAttack();
    }

    public void RangeAttack()
    {
        if (_bowTimer <=0 && Input.GetMouseButtonDown(1))
        {
            Instantiate(_arrow, _pointToshoot.position, _pointToshoot.rotation);
            _bowTimer = _timeToReload;
        }
        else
        {
            _bowTimer -= Time.deltaTime;
        }
    }
    public void MeeleAttack()
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
        if (_meeleWeaponToTakeTag != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_meeleWeaponToTakeTag == "Axe")
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
                if (_meeleWeaponToTakeTag == "Sword")
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
                if (_meeleWeaponToTakeTag == "Dagger")
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
            _meeleWeaponToTakeTag = other.gameObject.tag;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
            _meeleWeaponToTakeTag = null;
    }
}
