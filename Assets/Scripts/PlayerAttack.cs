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
   // private List<RangeWeapon> rangeWeapon = new List<RangeWeapon>() { new LongBow(1.5f, 12, 12, 0.1f, 4), new ShortBow(1, 6, 8, 0.1f, 12), new ClassicBow(1.25f, 8, 10f, 0.1f, 8) };
    [SerializeField] private List<GameObject> equipedRangeWeapon = new List<GameObject>() { };



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
    private float _timeToCd = 0.2f;
    private float _bowTimer;
    private int _arrowCount = 5;
    private float _timeToReload;


    [SerializeField] private Transform _enemyCheck;
    [SerializeField] private LayerMask _enemyLayer;


    private float _hitCoolDown = 0.5f;
    private float _CDtimer;
    [SerializeField] private int _playerDamage = 5;
    [SerializeField] private float _attackRange = 0.43f;

    private bool _swordMode = false;
    private bool _daggerMode = false;
    private bool _axeMode = false ;


    private void Update()
    {
        MeeleAttack();
        ChangeWeapons();
        RangeAttack();
        if  (_arrowCount == 5)
        {
            _timeToReload = 1f;
        }
    }

    public void RangeAttack()
    {
        if (equipedRangeWeapon.Count > 0)
        {
            if (_arrowCount > 0)
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
                    _arrowCount--;
                    _bowTimer = _timeToCd;
                }
                else
                {
                    _bowTimer -= Time.deltaTime;
                }
            }
            if (_timeToReload <= 0)
            {
                if (_arrowCount < 5)
                {
                    _arrowCount++;
                    _timeToReload = 1f;
                }
            }
            else
            {
                _timeToReload -= Time.deltaTime;
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
                    if (_daggerMode == false && _axeMode == false && _swordMode == false)
                        enemies[i].gameObject.GetComponent<Enemy>().TakeDamage(_playerDamage);
                    if (_swordMode)
                        enemies[i].gameObject.GetComponent<Enemy>().CriticalChanceMode(_playerDamage);
                    if (_daggerMode)
                        enemies[i].gameObject.GetComponent<Enemy>().Bleeding(_playerDamage);
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
                    _axeMode = true;
                    _swordMode = false;
                    _daggerMode = false;
                    SetCharateristics(meeleWeapon[0].MeeleWeaponDamage, meeleWeapon[0].MeeleWeaponCD, meeleWeapon[0].MeeleWeaponAttackRange);
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
                    _axeMode = false;
                    _swordMode = true;
                    _daggerMode = false;
                    SetCharateristics(meeleWeapon[1].MeeleWeaponDamage, meeleWeapon[1].MeeleWeaponCD, meeleWeapon[1].MeeleWeaponAttackRange);
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
                    _axeMode = false;
                    _swordMode = false;
                    _daggerMode = true;
                    SetCharateristics(meeleWeapon[2].MeeleWeaponDamage, meeleWeapon[2].MeeleWeaponCD, meeleWeapon[2].MeeleWeaponAttackRange);
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
                    SetCharateristics(rangeWeapon[0].RangeWeaponDamage, rangeWeapon[0].RangeWeaponCD, rangeWeapon[0].RangeWeaponMovementSpeed, rangeWeapon[0].RangeWeaponDistanceToRB, rangeWeapon[0].RangeWeaponArrowCount);
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
                    SetCharateristics(rangeWeapon[1].RangeWeaponDamage, rangeWeapon[1].RangeWeaponCD, rangeWeapon[1].RangeWeaponMovementSpeed, rangeWeapon[1].RangeWeaponDistanceToRB, rangeWeapon[1].RangeWeaponArrowCount);
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
                    SetCharateristics(rangeWeapon[2].RangeWeaponDamage, rangeWeapon[2].RangeWeaponCD, rangeWeapon[2].RangeWeaponMovementSpeed, rangeWeapon[2].RangeWeaponDistanceToRB, rangeWeapon[2].RangeWeaponArrowCount);
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

    public void SetCharateristics(int damage, float CD, float range)
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
    public void SetCharateristics(int damage, float cd, float movementSpeed, float distanceToRb, int Count)
    {
        _bowCd = cd;
        _bowDamage = damage;
        arrowMovementSpeed = movementSpeed;
        arrowDistanceToRb = distanceToRb;
        arrowCount = Count;
        
    }
}

