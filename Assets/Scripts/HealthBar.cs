using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    private GameObject playerObj;

    public void Awake()
    {
        fill = 1f;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        bar.fillAmount = fill;
        fill = (float)playerObj.gameObject.GetComponent<Player>()._curentHealth / (float)playerObj.gameObject.GetComponent<Player>()._maxHealth;
    }
}
