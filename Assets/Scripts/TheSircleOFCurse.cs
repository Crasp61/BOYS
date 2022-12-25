using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TheSircleOFCurse : MonoBehaviour
{
    private bool playerTakeDamage = true;
    GameObject playerObj;
    private void Awake()
    {
        StartCoroutine(TheTimeOfLife());
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator TheTimeOfLife()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && playerTakeDamage)
        {
            StartCoroutine(DoDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine(DoDamage());
        }
    }

    private IEnumerator DoDamage()
    {
        playerTakeDamage = false;
        yield return new WaitForSeconds(1f);
        playerObj.gameObject.GetComponent<Player>().TakeDamage(playerObj.gameObject.GetComponent<Player>()._maxHealth / 10);
        playerTakeDamage = true;
    }
}
