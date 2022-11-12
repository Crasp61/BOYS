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
    public List<GameObject> curentWeapon =  new List<GameObject>();


    
    [SerializeField] private GameObject _axeGameObject;
    [SerializeField] private GameObject _swordGameObject;
    [SerializeField] private GameObject _daggerGameObject;
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

    

    private void Update()
    {
        Attack();
        if (curentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && (curentWeapon[0].gameObject.tag != "Sword" || curentWeapon[0].gameObject.tag != "Dagger"  || curentWeapon[0].gameObject.tag == null))
            {
                SetCharateristics(weapon[0].WeaponCD, weapon[0].WeaponDamage, weapon[0].WeaponAttackrange);
                
                Destroy(GameObject.FindGameObjectWithTag("Axe"));
            }
            if (Input.GetKeyDown(KeyCode.E) && (curentWeapon[0].gameObject.tag != "Axe" || curentWeapon[0].gameObject.tag != "Dagger" || curentWeapon[0].gameObject.tag == null))
            {
                SetCharateristics(weapon[1].WeaponCD, weapon[1].WeaponDamage, weapon[1].WeaponAttackrange);

                Destroy(GameObject.FindGameObjectWithTag("Sword"));
            }           
            if (Input.GetKeyDown(KeyCode.E) && (curentWeapon[0].gameObject.tag != "Sword" || curentWeapon[0].gameObject.tag != "Axe" || curentWeapon[0].gameObject.tag == null))
            {
                SetCharateristics(weapon[2].WeaponCD, weapon[2].WeaponDamage, weapon[2].WeaponAttackrange);
                
                Destroy(GameObject.FindGameObjectWithTag("Dagger"));
            }
        }
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
    
    public void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Axe")
        {
                curentWeapon.Clear();
                curentWeapon.Add(_axeGameObject);
            
        }
        if (other.gameObject.tag == "Sword")
        {
            curentWeapon.Clear();
            curentWeapon.Add(_swordGameObject);
        }
        if (other.gameObject.tag == "Dagger")
        {
            curentWeapon.Clear();
            curentWeapon.Add(_daggerGameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Axe")
        {
            curentWeapon.Clear();
        }
        if (other.gameObject.tag == "Sword")
        {
            curentWeapon.Clear();
        }
        if (other.gameObject.tag == "Dagger")
        {
            curentWeapon.Clear();
        }
    }
}
