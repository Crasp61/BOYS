using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private List<MeeleWeapon> meeleWeapon;
    private List<GameObject> equipedMeeleWeapon = new List<GameObject>() { };
    [SerializeField] private List<RangeWeapon> rangeWeapon;
    [SerializeField] private List<GameObject> equipedRangeWeapon = new List<GameObject>() { };

    private int _arrowCount;

    private string _WeaponToTakeTag = null;

    public int _equipedMeeleWeaponNumber;
    public int _equipedRangeWeaponNumber;

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


    [SerializeField] private Transform _enemyCheck;
    [SerializeField] private LayerMask _enemyLayer;


    private float _hitCoolDown = 0.5f;

    [SerializeField] private int _playerDamage = 5;
    [SerializeField] private float _attackRange = 0.43f;


    private GameObject[] meeleWeaponMas = new GameObject[3];
    private GameObject[] rangeWeaponMas = new GameObject[3];

    private GameObject _weaponToDestroy;

    private int weaponNumber;

    private bool readyToShoot = true;
    private bool isReloading = false;
    private bool isAttacking = false;
    private void Update()
    {
        if (!isAttacking)
        {
            StartCoroutine(MeeleAttack());
        }
        if (readyToShoot)
        {
            StartCoroutine(RangeAttack());
        }
        if (!isReloading)
        {
            StartCoroutine(Reaload());
        }

        SetWeapons();
        MasFeeler();
    }

    public IEnumerator RangeAttack()
    {
        if (equipedRangeWeapon.Count > 0)
        {
            if (_arrowCount > 0)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    readyToShoot = false;
                    yield return new WaitForSeconds(_bowCd);
                    if (Player._isFacingRight)
                    {
                        Instantiate(_arrow, _pointToshootRight.position, _pointToshootRight.rotation);
                    }
                    if (Player._isFacingRight == false)
                    {
                        Instantiate(_arrow, _pointToshootLeft.position, _pointToshootLeft.rotation);
                    }
                    _arrowCount--;
                    readyToShoot = true;
                }
            }
        }
    }
    public IEnumerator Reaload()
    {
        if (_arrowCount < arrowCount)
        {
            isReloading = true;
            yield return new WaitForSeconds(_bowCd + 1);
            _arrowCount++;
            isReloading = false;
        }
    }

    public IEnumerator MeeleAttack()
    {
        if (Input.GetMouseButton(0))
        {
            isAttacking = true;
            yield return new WaitForSeconds(_hitCoolDown);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(_enemyCheck.position, _attackRange, _enemyLayer);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].gameObject.GetComponent<Enemy>().TakeDamage(_playerDamage);
            }
            isAttacking=false;
        }
    }
        
    public void SetWeapons()
    {
        if (_WeaponToTakeTag != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeWeapons(weaponNumber);
            }
        }
    }
    public void ChangeWeapons(int weaponNumber)
    {
        if (_WeaponToTakeTag == "MeeleWeapon")
        {
            SetCharateristics(meeleWeapon[weaponNumber].MeeleWeaponDamage, meeleWeapon[weaponNumber].MeeleWeaponCD, meeleWeapon[weaponNumber].MeeleWeaponAttackRange);
            if (equipedMeeleWeapon.Count > 0)
            {
                Instantiate(equipedMeeleWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
            }
            equipedMeeleWeapon.Clear();
            equipedMeeleWeapon.Add(meeleWeaponMas[weaponNumber]);
            Destroy(_weaponToDestroy);
        }

        if (_WeaponToTakeTag == "RangeWeapon")
        {
            SetCharateristics(rangeWeapon[weaponNumber].RangeWeaponDamage, rangeWeapon[weaponNumber].RangeWeaponCD, rangeWeapon[weaponNumber].RangeWeaponMovementSpeed, rangeWeapon[weaponNumber].RangeWeaponDistanceToRB, rangeWeapon[weaponNumber].RangeWeaponArrowCount);
            if (equipedRangeWeapon.Count > 0)
            {
                Instantiate(equipedRangeWeapon[0], _weaponSpawn.position, _weaponSpawn.rotation);
            }
            equipedRangeWeapon.Clear();
            equipedRangeWeapon.Add(rangeWeaponMas[weaponNumber]);
        }
        Destroy(_weaponToDestroy);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "MeeleWeapon" || other.gameObject.tag == "RangeWeapon")
        {
            _WeaponToTakeTag = other.gameObject.tag;
            for (int i = 0; i < meeleWeaponMas.Length; i++)
            {
                if (meeleWeaponMas[i].GetComponent<SpriteRenderer>().color == other.gameObject.GetComponent<SpriteRenderer>().color)
                {
                    weaponNumber = i;
                }
            }
            for (int i = 0; i < rangeWeaponMas.Length; i++)
            {
                if (rangeWeaponMas[i].GetComponent<SpriteRenderer>().color == other.gameObject.GetComponent<SpriteRenderer>().color)
                {
                    weaponNumber = i;
                }
            }
            _weaponToDestroy = other.gameObject;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        _WeaponToTakeTag = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_enemyCheck.position, _attackRange);
    }

    public void SetCharateristics(int damage, float CD, float range)
    {
        _hitCoolDown = CD;
        _playerDamage = damage;
        _attackRange = range;
    }

    private float _bowCd;
    public static int _bowDamage;
    public static float arrowMovementSpeed;
    public static float arrowDistanceToRb;
    private int arrowCount;
    public void SetCharateristics(int damage, float cd, float movementSpeed, float distanceToRb, int Count)
    {
        _bowCd = cd;
        _bowDamage = damage;
        arrowMovementSpeed = movementSpeed;
        arrowDistanceToRb = distanceToRb;
        arrowCount = Count;
        _arrowCount = arrowCount;

    }

    public void MasFeeler()
    {
        meeleWeaponMas[0] = _axeGameObject;
        meeleWeaponMas[1] = _swordGameObject;
        meeleWeaponMas[2] = _daggerGameObject;
        rangeWeaponMas[0] = _longBowGameObject;
        rangeWeaponMas[1] = _shortBowGameObject;
        rangeWeaponMas[2] = _classicBowGameObject;
    }

    public void UseModOfWeapon(int number, int someNumber)
    {
        if (number == 2)
        {

        }
    }
}