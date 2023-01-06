using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TheSircleOFCurse : MonoBehaviour
{
    private bool playerTakeDamage = true;
    GameObject playerObj;
    private bool playerIn;
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
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIn = false; 
        }
    }

    private IEnumerator DoDamage()
    {
        playerTakeDamage = false;
        yield return new WaitForSeconds(1f);
        if (playerIn)
        {
            playerObj.gameObject.GetComponent<Player>().TakeDamage(playerObj.gameObject.GetComponent<Player>()._maxHealth / 10);
        }
        playerTakeDamage = true;
    }
}
