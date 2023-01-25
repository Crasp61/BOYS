using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed;
    public int _maxHealth;
    [SerializeField] protected Rigidbody2D rb;
    [HideInInspector]public int _curentHealth;
    [SerializeField] protected float _angleOffSet = 90f;
    [SerializeField] protected float rotationSpeed = 6f;
    private GameObject player;
    public bool isBleeding = false;
    public bool readyToTakeDamage = true;
    public int bleedCount = 0;
    protected GameObject playerObj;
    public bool isDying = false;
    protected virtual void Start()
    {
        _curentHealth = _maxHealth;
    }

    protected virtual void Update()
    {
        if (isBleeding)
        {
            StartCoroutine(Bleeding());
        }

        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    public  void TakeDamage(int damage)
    {
        _curentHealth -= damage;
        if (_curentHealth <= 0 && !isDying)
        {
            StartCoroutine(Dying());
        }
    }

    private IEnumerator Dying()
    {
        isDying = true;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    public IEnumerator Bleeding()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (isBleeding)
        {
            if (bleedCount < 10)
            {
                if (readyToTakeDamage)
                {
                    readyToTakeDamage = false;
                    TakeDamage(player.gameObject.GetComponent<PlayerAttack>()._playerDamage / 8);
                    yield return new WaitForSeconds(0.33f);
                    readyToTakeDamage = true;
                    bleedCount++;
                }
            }
            else
            {
                isBleeding = false;
                bleedCount = 0;
            }
        }
    }
}

public class Enemy : Creature
{
    [SerializeField] public int _damage;
    
}