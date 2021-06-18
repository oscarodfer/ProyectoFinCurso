using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public CharacterStats saveStats;
    public void SaveStats(int slot)
    {
        SaveStatsPlayer.SaveStats(saveStats, slot);
    }

    public void LoadStats(int slot) 
    {
        SaveStatsPlayer.LoadStats(saveStats, slot);
    }

}
