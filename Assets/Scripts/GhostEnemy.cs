using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Animations;

public class GhostEnemy : Enemy
{
    
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] Transform _enemyPos;
    [SerializeField] float _rangeOfDetection;
    public Transform _playerPosition;
    public Transform _pointEnemyBase;
    [SerializeField] private float _agroDistance;


    protected override void Start()
    {
        base.Start();  
    }
    private void Update()
    {
        
        float distToPlayer = Vector2.Distance(transform.position, _playerPosition.position);

        if (distToPlayer < _agroDistance)
        {
            StartHunting();
        }
        else
        {
            StopHuntingAndBack();
        }
        Debug.Log(_curentHealth);
    }
    
   
    public void StartHunting()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerPosition.position, _movementSpeed * 2.5f * Time.deltaTime);
        if (transform.position.x > _playerPosition.position.x)
        {
            transform.localScale = new Vector2(0.1f, 0.1f);
        }
        else
        {
            transform.localScale = new Vector2(-0.1f, 0.1f);
        }
    }
    public void StopHuntingAndBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, _pointEnemyBase.position, _movementSpeed * Time.deltaTime);

    }
}

