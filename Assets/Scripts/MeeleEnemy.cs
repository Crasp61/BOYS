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
    [SerializeField] Transform GroubdCheckForStop;
    [SerializeField] Transform WallCheck;
    [SerializeField] LayerMask _groundLayer;
    public Transform _playerPosition;
    [SerializeField] private float _agroDistance;
    public bool movingRight = true;



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
            Patrol();
        }
    }


    public void StartHunting()
    {
        if (EndPlatfom())
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition.position, _movementSpeed * 2 * Time.deltaTime);
        }
        if (transform.position.x > _playerPosition.position.x)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }


    }
    public void Patrol()
    {
        transform.Translate(Vector2.right * _movementSpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(GroubdCheckForStop.position, Vector2.down, 2f);
        RaycastHit2D walldInfo = Physics2D.Raycast(WallCheck.position, Vector2.zero, 2f);
        if (groundInfo.collider == false || walldInfo.collider == true)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    public bool EndPlatfom()
    {
        return Physics2D.OverlapCircle(GroubdCheckForStop.position, 0.2f, _groundLayer);

    }
}
