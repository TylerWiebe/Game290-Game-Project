using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedPlayerStats : MonoBehaviour
{
    public PlayerStatistics savedPlayerData = new PlayerStatistics();

    public static SavedPlayerStats Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
