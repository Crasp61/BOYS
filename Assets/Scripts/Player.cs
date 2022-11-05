using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    Rigidbody2D rb;
    [SerializeField] private bool isGround = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            isGround = true;
            print("Проверка");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Floor")
            isGround = false;
    }

    void Update()
    {
         if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


    }

    



}
