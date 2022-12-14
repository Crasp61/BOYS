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
    private float distToPlayer;



    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (playerObj != null)
        {
            float distToPlayer = Vector2.Distance(transform.position, playerObj.transform.position);
           
            if (distToPlayer < _agroDistance)
            {
                StartHunting();
            }
            else
            {
                Patrol();
            }
        }
    }


    public void StartHunting()
    {

            if (EndPlatfom())
            {
                transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, _movementSpeed * 2 * Time.deltaTime);
            }
            if (transform.position.x > playerObj.transform.position.x)
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

        Collider2D groundInfo = Physics2D.OverlapCircle(GroubdCheckForStop.position, 0.2f, _groundLayer);
        Collider2D wallInfo = Physics2D.OverlapCircle(WallCheck.position, 0.2f, _groundLayer);
        if (groundInfo == false || wallInfo == true)
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
        return Physics2D.OverlapCircle(GroubdCheckForStop.position, 0.2f, _groundLayer) && !Physics2D.OverlapCircle(WallCheck.position, 0.2f, _groundLayer);
        

    }
}
