using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed = 4f;
    [SerializeField] protected int _maxHealth = 30;
    [SerializeField] protected int _damage = 5;
    [SerializeField] protected Rigidbody2D rb;
    protected int _curentHealth;
    
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
