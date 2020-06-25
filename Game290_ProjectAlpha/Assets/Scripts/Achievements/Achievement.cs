using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{


    public Image Achieve_Killer;//kill a specified number of enemies
    public Image Achieve_Dying; //die a specified number of times
    public Image Achieve_Stats_Are_Important; //collect a specific number of statorbs
    public Image Achieve_You_Will_Get_Them_Next_Time; //attempt to beat the game a number of times
    public Image Achieve_too_Easy;//First Win
    public Image Achieve_No_life;//Win more than one time
    public Image Achieve_Thats_a_Start; //kill boss 1
    public Image Achieve_Almost_There; //Kill boss 2
    public Image Achieve_Veni_Vidi_Vici; //Kill boss 3
    public Image Achieve_pacifist;//reach boss 1 without attacking once

    public Sprite Sprite_Killer;
    public Sprite Sprite_Dying;
    public Sprite Sprite_Stats_Are_Important;
    public Sprite Sprite_You_Will_Get_Them_Next_Time;
    public Sprite Sprite_too_Easy;
    public Sprite Sprite_No_Life;
    public Sprite Sprite_Thats_A_Start;
    public Sprite Sprite_Almost_There;
    public Sprite Sprite_Veni_Vidi_Vici;
    public Sprite Sprite_Pacifist;

    public Sprite Sprite_Hidden;

    public int Requirements_Killer;
    public int Requirements_Dying;
    public int Requirements_Stats_Are_Important;
    public int Requirements_You_Will_Get_Them_Next_Time;
    public int Requirements_too_Easy = 1;
    public int Requirements_No_Life;

    void OnEnable()
    {
        deactivateAll();
        activateAchievements();
    }

    public void activateAchievements()
    {
        if (LocalPlayerStats.Instance.localPlayerData.enemiesKilled >= Requirements_Killer)
        {
            Achieve_Killer.sprite = Sprite_Killer;
        }
        if (LocalPlayerStats.Instance.localPlayerData.timesDied >= Requirements_Dying)
        {
            Achieve_Dying.sprite = Sprite_Dying;
        }
        if (LocalPlayerStats.Instance.localPlayerData.statOrbsUsed >= Requirements_Stats_Are_Important)
        {
            Achieve_Stats_Are_Important.sprite = Sprite_Stats_Are_Important;
        }
        if (LocalPlayerStats.Instance.localPlayerData.numberOfAttempts >= Requirements_You_Will_Get_Them_Next_Time)
        {
            Achieve_You_Will_Get_Them_Next_Time.sprite = Sprite_You_Will_Get_Them_Next_Time;
        }
        if (LocalPlayerStats.Instance.localPlayerData.timesWon >= Requirements_too_Easy)
        {
            Achieve_too_Easy.sprite = Sprite_too_Easy;
        }
        UnityEngine.Debug.Log("timesWon" + LocalPlayerStats.Instance.localPlayerData.timesWon + ": requires " + Requirements_No_Life);
        if (LocalPlayerStats.Instance.localPlayerData.timesWon >= Requirements_No_Life)
        {
            Achieve_No_life.sprite = Sprite_No_Life;
        }
        if (LocalPlayerStats.Instance.localPlayerData.hasBeatenBossOne)
        {
            Achieve_Thats_a_Start.sprite = Sprite_Thats_A_Start;
        }
        if (LocalPlayerStats.Instance.localPlayerData.hasBeatenBossTwo)
        {
            Achieve_Almost_There.sprite = Sprite_Almost_There;
        }
        if (LocalPlayerStats.Instance.localPlayerData.hasBeatenBossThree)
        {
            Achieve_Veni_Vidi_Vici.sprite = Sprite_Veni_Vidi_Vici;
        }
        if (LocalPlayerStats.Instance.localPlayerData.hasReachedBossOneWithoutAttacking)
        {
            Achieve_pacifist.sprite = Sprite_Pacifist;
        }
    }

    public void deactivateAll()
    {
        Achieve_Killer.sprite = Sprite_Hidden;
        Achieve_Dying.sprite = Sprite_Hidden;
        Achieve_Stats_Are_Important.sprite = Sprite_Hidden;
        Achieve_You_Will_Get_Them_Next_Time.sprite = Sprite_Hidden;
        Achieve_too_Easy.sprite = Sprite_Hidden;
        Achieve_No_life.sprite = Sprite_Hidden;
        Achieve_Thats_a_Start.sprite = Sprite_Hidden;
        Achieve_Almost_There.sprite = Sprite_Hidden;
        Achieve_Veni_Vidi_Vici.sprite = Sprite_Hidden;
        Achieve_pacifist.sprite = Sprite_Hidden;
    }

}
