using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveTheGAme : MonoBehaviour
{
    public Transform CurrentPlayerPosition;
    public bool onCheckPoint = false;
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.R) && onCheckPoint)
        {
            savePosition();
            Debug.Log("Bam");
        }


        if (Input.GetKeyDown(KeyCode.P))
            PlayerPrefs.DeleteAll();    // очистка всей информации для этого приложения
    }

    public void savePosition()
    {

        Transform CurrentPlayerPosition = this.gameObject.transform;

        PlayerPrefs.SetFloat("PosX", CurrentPlayerPosition.position.x); // т.к. автоматической работы 
        PlayerPrefs.SetFloat("PosY", CurrentPlayerPosition.position.y); // с массивами нет, разбиваем на
        PlayerPrefs.SetFloat("PosZ", CurrentPlayerPosition.position.z);  // отдельные float и записываем

        PlayerPrefs.SetFloat("AngX", CurrentPlayerPosition.eulerAngles.x);
        PlayerPrefs.SetFloat("AngY", CurrentPlayerPosition.eulerAngles.y);

    }

    public void loadPosition()
    {
        SceneManager.LoadScene(1);
        Transform CurrentPlayerPosition = this.gameObject.transform;

        Vector3 PlayerPosition = new Vector3(PlayerPrefs.GetFloat("PosX"),
                    PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        Vector3 PlayerDirection = new Vector3(PlayerPrefs.GetFloat("AngX"), // генерируем новые вектора 
                    PlayerPrefs.GetFloat("AngY"), 0);  // на основе загруженных данных

        CurrentPlayerPosition.position = PlayerPosition; // и применяем их
        CurrentPlayerPosition.eulerAngles = PlayerDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CheckPoint")
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
