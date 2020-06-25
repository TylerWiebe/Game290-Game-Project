using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerStats : MonoBehaviour
{
    public static LocalPlayerStats Instance;

    public PlayerStatistics localPlayerData = new PlayerStatistics();

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

    }

    //Save data to SavedPlayerStats   
    public void SavePlayer()
    {
        SavedPlayerStats.Instance.savedPlayerData = localPlayerData;
    }

    void start()
    {
        localPlayerData = SavedPlayerStats.Instance.savedPlayerData;
    }

}
