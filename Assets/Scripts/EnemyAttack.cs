using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private float _timerAfterAttack;
    public void Start()
    {
        _timerAfterAttack = 0;
    }
    public void Update()
    {
        _timerAfterAttack -= Time.deltaTime;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {

            if (_timerAfterAttack <= 0)
            {
                other.gameObject.GetComponent<Player>().TakeDamage(gameObject.GetComponent<GhostEnemy>()._damage);
                _timerAfterAttack = 1;
            }
        }
    }




}