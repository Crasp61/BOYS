using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected float _movementSpeed = 4f;
    [SerializeField] protected int _health = 30;
    [SerializeField] protected int _damage = 5;
    protected Rigidbody2D rb;
    
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected abstract void Move();
}
