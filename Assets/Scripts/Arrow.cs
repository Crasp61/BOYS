using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    private float _speed;
    private int _damage;
    private float rotationSpeed = 0.4f;
    Vector3 target;
    [SerializeField] private float radius;
    [SerializeField] LayerMask _groundLayer;

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        _damage = PlayerAttack._bowDamage;
        _speed = PlayerAttack.arrowMovementSpeed;
        StartCoroutine(Fall());
        if (HitGround())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.4f);
        Vector3 deltaposition = new Vector3(transform.position.x, transform.position.y - 50, 0);
        float angleZ = Mathf.Atan2(deltaposition.y, deltaposition.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0, 0, angleZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * rotationSpeed);
    }

    public bool HitGround()
    {
        return Physics2D.OverlapCircle(transform.position, radius, _groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}