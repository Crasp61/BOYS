using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int _speed;
    private int _damage;


    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        _damage = PlayerAttack._bowDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }
}
