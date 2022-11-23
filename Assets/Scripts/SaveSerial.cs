using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSerial : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public SaveData saveData { get; set; }

    public int _meeleWeaponNumber;
    public int _rangeWeaponNumber;
    public int _curentHealth;
    public int _sceneIndex;

    public void SaveGame(int meeleWeaponNumber, int rangeWeaponNumber, int curentHealth, int sceneIndex)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.saveMeeleWeaponNumber = meeleWeaponNumber;
        data.saveRangeWeaponNumber = rangeWeaponNumber;
        data.saveCurentHealth = curentHealth;
        data.saveSceneIndex = sceneIndex;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _meeleWeaponNumber = data.saveMeeleWeaponNumber;
            _rangeWeaponNumber = data.saveRangeWeaponNumber;
            _curentHealth = data.saveRangeWeaponNumber;
            _sceneIndex = data.saveSceneIndex;
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveData.dat");
            _meeleWeaponNumber = 0;
            _rangeWeaponNumber = 0;
            _curentHealth = 0;
            _sceneIndex = 1;
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}

[Serializable]
public class SaveData
{
    public int saveMeeleWeaponNumber;
    public int saveRangeWeaponNumber;
    public int saveCurentHealth;
    public int saveSceneIndex;
}
