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
    private List<MeeleWeapon> meeleWeapon = new List<MeeleWeapon>() { new Axe(1f, 20, 0.8f), new Sword(0.5f, 12, 0.43f), new Dagger(0.3f, 8, 0.3f) };
    private List<GameObject> equipedMeeleWeapon = new List<GameObject>() { };
    private List<RangeWeapon> rangeWeapon = new List<RangeWeapon>() { new LongBow(1.5f, 12, 12, 0.1f, 4), new ShortBow(1, 6, 8, 0.1f, 12), new ClassicBow(1.25f, 8, 10f, 0.1f, 8)};
    [SerializeField] private List<GameObject> equipedRangeWeapon = new List<GameObject>() { };

    private string _WeaponToTakeTag = null;
    
    [SerializeField] private GameObject _axeGameObject;
    [SerializeField] private GameObject _swordGameObject;
    [SerializeField] private GameObject _daggerGameObject;
    [SerializeField] private Transform _weaponSpawn;

    [SerializeField] private GameObject _longBowGameObject;
    [SerializeField] private GameObject _shortBowGameObject;
    [SerializeField] private GameObject _classicBowGameObject;

    

    [SerializeField] private GameObject _arrow;
    [SerializeField] private Transform _pointToshootRight;
    [SerializeField] private Transform _pointToshootLeft;
    private float _timeToReload = 0.2f;
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
        if (equipedRangeWeapon.Count > 0)
        {
            if (_bowTimer <= 0 && Input.GetMouseButtonDown(1))
            {
                if (Player._isFacingRight)
                {
                    Instantiate(_arrow, _pointToshootRight.position, _pointToshootRight.rotation);
                }
                if (Player._isFacingRight == false)
                {
                    Instantiate(_arrow, _pointToshootLeft.position, _pointToshootLeft.rotation);
                }
                _bowTimer = _timeToReload;
            }
            else
            {
                _bowTimer -= Time.deltaTime;
            }
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
        if (_WeaponToTakeTag != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_WeaponToTakeTag == "Axe")
                {
                    SetCharateristics(meeleWeapon[0].WeaponCD, meeleWeapon[0].WeaponDamage, meeleWeapon[0].WeaponAttackrange);
                    if (equipedMeeleWeapon.Count > 0)
                    {
                        Instantiate(equipedMeeleWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedMeeleWeapon.Clear();
                    equipedMeeleWeapon.Add(_axeGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("Axe"));
                }
                if (_WeaponToTakeTag == "Sword")
                {
                    SetCharateristics(meeleWeapon[1].WeaponCD, meeleWeapon[1].WeaponDamage, meeleWeapon[1].WeaponAttackrange);
                    if (equipedMeeleWeapon.Count > 0)
                    {
                        Instantiate(equipedMeeleWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedMeeleWeapon.Clear();
                    equipedMeeleWeapon.Add(_swordGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("Sword"));
                }
                if (_WeaponToTakeTag == "Dagger")
                {
                    SetCharateristics(meeleWeapon[2].WeaponCD, meeleWeapon[2].WeaponDamage, meeleWeapon[2].WeaponAttackrange);
                    if (equipedMeeleWeapon.Count > 0)
                    {
                        Instantiate(equipedMeeleWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedMeeleWeapon.Clear();
                    equipedMeeleWeapon.Add(_daggerGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("Dagger"));
                }
                if (_WeaponToTakeTag == "LongBow")
                {
                    SetCharateristics(rangeWeapon[0].WeaponCD, rangeWeapon[0].WeaponDamage, rangeWeapon[0].MovementSpeed, rangeWeapon[0].DistanseToRb, rangeWeapon[0].ArrowCount);
                    if (equipedRangeWeapon.Count > 0)
                    {
                        Instantiate(equipedRangeWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedRangeWeapon.Clear();
                    equipedRangeWeapon.Add(_longBowGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("LongBow"));
                }
                if (_WeaponToTakeTag == "ShortBow")
                {
                    SetCharateristics(rangeWeapon[1].WeaponCD, rangeWeapon[1].WeaponDamage, rangeWeapon[1].MovementSpeed, rangeWeapon[1].DistanseToRb, rangeWeapon[1].ArrowCount);
                    if (equipedRangeWeapon.Count > 0)
                    {
                        Instantiate(equipedRangeWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedRangeWeapon.Clear();
                    equipedRangeWeapon.Add(_shortBowGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("ShortBow"));
                }
                if (_WeaponToTakeTag == "ClassicBow")
                {
                    SetCharateristics(rangeWeapon[2].WeaponCD, rangeWeapon[2].WeaponDamage, rangeWeapon[2].MovementSpeed, rangeWeapon[2].DistanseToRb, rangeWeapon[2].ArrowCount);
                    if (equipedRangeWeapon.Count > 0)
                    {
                        Instantiate(equipedRangeWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
                    }
                    equipedRangeWeapon.Clear();
                    equipedRangeWeapon.Add(_classicBowGameObject);
                    Destroy(GameObject.FindGameObjectWithTag("ClassicBow"));
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_enemyCheck.position, _attackRange);
    }

    public void SetCharateristics(float CD, int damage , float range)
    {
            _hitCoolDown = CD;
            _playerDamage = damage;
            _attackRange = range;
    }
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        _WeaponToTakeTag = other.gameObject.tag;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        _WeaponToTakeTag = null;
    }
    private float _bowCd;
    public static int _bowDamage;
    public static float arrowMovementSpeed;
    public static float arrowDistanceToRb;
    private int arrowCount;
    public void SetCharateristics(float cd, int damage, float movementSpeed, float distanceToRb, int Count)
    {
        _bowCd = cd;
        _bowDamage = damage;
        arrowMovementSpeed = movementSpeed;
        arrowDistanceToRb = distanceToRb;
        arrowCount = Count;
        
    }
}

