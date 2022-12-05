using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected Rigidbody2D rb;
    [HideInInspector]public int _curentHealth;
    [SerializeField] protected float _angleOffSet = 90f;
    [SerializeField] protected float rotationSpeed = 6f;
    

    protected virtual void Start()
    {
        _curentHealth = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _curentHealth -= damage;
        if (_curentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private float _criticalChance = 0.5f;
    private double timeToBleed;
    private float TimeToHit;

    public void CriticalChanceMode(int damage)
    {
        float rand = Random.Range(0f, 1f);
        if (rand < _criticalChance)
        {
            TakeDamage(damage * 2);
        }
        else
            TakeDamage(damage);
    }
}

public class Enemy : Creature
{
    [SerializeField] public int _damage;
    
}