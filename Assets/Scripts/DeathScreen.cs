using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject panel;
    GameObject playerObj;

    public void Update()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj != null)
        if (playerObj.gameObject.GetComponent<Player>()._curentHealth<=0)
        {
            DeathScreenSetActive();
        }
    }

    public void DeathScreenSetActive()
    {
        panel.SetActive(true);
    }
}
