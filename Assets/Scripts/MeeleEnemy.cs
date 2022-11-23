using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Animations;

public class MeeleEnemy : Enemy
{
    [SerializeField]Transform GroubdCheckForStop;
    [SerializeField]LayerMask _groundLayer;
    public Transform _playerPosition;   
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
       /* else
        {
            StopHuntingAndBack();
        }*/
    }


    public void StartHunting()
    {
        if (EndPlatfom())
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition.position, _movementSpeed * Time.deltaTime);
        }
            if (transform.position.x > _playerPosition.position.x)
            {
                transform.localScale = new Vector2(0.5f, 0.5f);
            }
            else
            {
                transform.localScale = new Vector2(-0.5f, 0.5f);
            }
        
    }
    public bool EndPlatfom()
    {
        return Physics2D.OverlapCircle(GroubdCheckForStop.position, 0.2f, _groundLayer);

    }
}
