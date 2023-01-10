using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject panel;
    GameObject playerObj;
    private bool dethScreenExist;

    public void Update()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            DeathScreenSetActive();
        }
    }

    public void DeathScreenSetActive()
    {
        panel.SetActive(true);
    }
}
