using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed = 4f;
    [SerializeField] public int _maxHealth;
    [SerializeField] protected Rigidbody2D rb;
    [HideInInspector]public int _curentHealth;
    
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
}

public class Enemy : Creature
{
    [SerializeField] public int _damage;
}