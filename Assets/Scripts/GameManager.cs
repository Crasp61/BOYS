using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    private bool onPause = false;


    public GameObject menu;
    public GameObject mainMenuButton;

    public void StartTheGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void ToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        StopTheGame();
    }
    private void StopTheGame()
    {
        if (!onPause)
        {
            Time.timeScale = 0f;
            onPause = true;
            Debug.Log(onPause);
            menu.SetActive(true);
            mainMenuButton.SetActive(true); 

            return;
        }
        if (onPause)
        {
            Time.timeScale = 1f;
            onPause = false;
            Debug.Log(onPause);
            menu.SetActive(false);
            mainMenuButton.SetActive(false);
            return;
        }
    }
}

