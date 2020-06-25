using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class AchieveManager : MonoBehaviour
{
    public PlayerStatistics LocalCopyOfData;

    public void SaveData()
    {

        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");

        LocalCopyOfData = LocalPlayerStats.Instance.localPlayerData;

        formatter.Serialize(saveFile, LocalCopyOfData);

        saveFile.Close();
    }

    public void ResetAchievements()
    {

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");

        LocalCopyOfData = new PlayerStatistics();

        formatter.Serialize(saveFile, LocalCopyOfData);

        saveFile.Close();
        LocalPlayerStats.Instance.localPlayerData = LocalCopyOfData;
    }

    public void LoadData()
    {

        UnityEngine.Debug.Log("LoadedGame");
        if (!File.Exists("Saves/save.binary"))
        {
            this.SaveData();
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream loadFile = File.Open("Saves/save.binary", FileMode.Open);

        LocalCopyOfData = (PlayerStatistics)formatter.Deserialize(loadFile);

        loadFile.Close();
    }
}
