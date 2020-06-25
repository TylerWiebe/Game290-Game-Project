using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatistics
{
    //record the number of eneimes killed
    public int enemiesKilled = 0;
    //record the number of times the player has died
    public int timesDied = 0;
    //record the number of statorbs picked up
    public int statOrbsUsed = 0;
    //record the number of attempts at the game
    public int numberOfAttempts = 0;
    //record the number of times the game has been beaten
    public int timesWon = 0;
    //record if the player has beaten the first boss
    public bool hasBeatenBossOne = false;
    //record if the player has beaten boss two
    public bool hasBeatenBossTwo = false;
    //record if the player has beaten boss three;
    public bool hasBeatenBossThree = false;
    //record if the player has gotten to boss 1 without attacking once
    public bool hasReachedBossOneWithoutAttacking = false;
}
