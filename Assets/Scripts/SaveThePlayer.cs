using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveThePlayer : MonoBehaviour
{
    public Transform CurrentPlayerPosition;
    public bool onCheckPoint = false;
    public GameObject playerObj;
    public GameObject deathPanel;
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.R) && onCheckPoint)
        {
            savePosition();
            Debug.Log("Bam");
        }

    }

    public void savePosition()
    {

        Transform CurrentPlayerPosition = this.gameObject.transform;

        PlayerPrefs.SetFloat("PosX", CurrentPlayerPosition.position.x);
        PlayerPrefs.SetFloat("PosY", CurrentPlayerPosition.position.y);

        PlayerPrefs.SetFloat("AngX", CurrentPlayerPosition.eulerAngles.x);
        PlayerPrefs.SetFloat("AngY", CurrentPlayerPosition.eulerAngles.y);


        PlayerPrefs.SetInt("Health", playerObj.gameObject.GetComponent<Player>()._curentHealth);
    }

    public void loadPosition()
    {
        deathPanel.SetActive(false);   

        Transform CurrentPlayerPosition = this.gameObject.transform;

        Vector2 PlayerPosition = new Vector2(PlayerPrefs.GetFloat("PosX"),PlayerPrefs.GetFloat("PosY"));

        Vector2 PlayerDirection = new Vector2(PlayerPrefs.GetFloat("AngX"),PlayerPrefs.GetFloat("AngY"));

        playerObj.gameObject.GetComponent<Player>()._curentHealth = PlayerPrefs.GetInt("Health");

        CurrentPlayerPosition.position = PlayerPosition;
        CurrentPlayerPosition.eulerAngles = PlayerDirection;

        playerObj.SetActive(true);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            onCheckPoint = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            onCheckPoint = false;
        }
    }
}
