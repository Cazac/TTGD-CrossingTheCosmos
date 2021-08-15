using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_PlayerSaveData 
{
    /////////////////////////////////////////////////////////////////

    [Header("Volume")]
    public float settings_TotalVolume;
    public float settings_MusicVolume;
    public float settings_SFXVolume;

    [Header("Muting")]
    public bool settings_isMusicMuted;
    public bool settings_isSFXMuted;

    /////////////////////////////////////////////////////////////////

    public void CreateNewSave()
    {
        //Set Weeks
        settings_TotalVolume = 1;

        settings_MusicVolume = 0.5f;
        settings_SFXVolume = 0.5f;

        settings_isMusicMuted = false;
        settings_isSFXMuted = false;
    }

    /////////////////////////////////////////////////////////////////
}
