using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public SaveSerial saveSystem;

    private void Awake()
    {
        SceneManager.sceneLoaded += Initialize;
        DontDestroyOnLoad(gameObject);
    }

    private void Initialize(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded GM");
        var playerInput = FindObjectOfType<Player>();
        if (playerInput != null)
            player = playerInput.gameObject;
        saveSystem = FindObjectOfType<SaveSerial>();
        if (player != null && saveSystem.saveData != null)
        {
            var hp = player.GetComponentInChildren<Player>();
            var dd = player.GetComponentInChildren<PlayerAttack>();
            hp._curentHealth = saveSystem.saveData.saveCurentHealth;
            dd._equipedMeeleWeaponNumber = saveSystem.saveData.saveMeeleWeaponNumber;
            dd._equipedRangeWeaponNumber = saveSystem.saveData.saveRangeWeaponNumber;
        }
    }

    public void LoadLevel()
    {
        if (saveSystem.saveData != null)
        {
            SceneManager.LoadScene(saveSystem.saveData.saveSceneIndex);
            return;
        }
    }

    public void LoadNextLevel(int gate)
    {
        SceneManager.LoadScene(gate);
    }

    public void SaveData(int gate)
    {
        if (player != null)
            saveSystem.SaveGame(player.GetComponentInChildren<PlayerAttack>()._equipedMeeleWeaponNumber, player.GetComponentInChildren<PlayerAttack>()._equipedRangeWeaponNumber,
                player.GetComponentInChildren<Player>()._curentHealth, gate);
    }
}