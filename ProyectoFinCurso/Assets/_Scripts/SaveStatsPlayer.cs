using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStatsPlayer : MonoBehaviour
{
    public static void SaveStats(MonoBehaviour componente, int slot = 1) 
    {
        PlayerPrefs.SetString("slot " + slot, JsonUtility.ToJson(componente));
        Debug.Log(JsonUtility.ToJson(componente));
    }

    public static void LoadStats(MonoBehaviour componente, int slot = 1) 
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("slot " + slot),componente);
    }
}
