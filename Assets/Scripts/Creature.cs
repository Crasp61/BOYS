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
    protected void SetAngle(Vector3 target)
    {
        Vector3 deltaposition = target - transform.position;
        float angleZ = Mathf.Atan2(deltaposition.y, deltaposition.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0, 0, angleZ + _angleOffSet);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * rotationSpeed);
    }
}

public class Enemy : Creature
{
    [SerializeField] public int _damage;
}