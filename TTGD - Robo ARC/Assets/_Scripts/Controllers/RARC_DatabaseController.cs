using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_DatabaseController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_DatabaseController Instance;

    ////////////////////////////////

    [Header("Crew Database")]
    public RARC_CrewData crew_DB;


    ////////////////////////////////

    [Header("Current Ship Save Data")]
    public RARC_ShipSaveData ship_SaveData;

    //[Header("Account Save Data")]
    //public TM_SettingsSaveData settings_SaveData;
    //public TM_UnlocksSaveData unlock_SaveData;


    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;

        //Build Databases Value Lists
        BuildDatabase();

        //ship_SaveData = null;
    }

    /////////////////////////////////////////////////////////////////

    private void BuildDatabase()
    {
        //Build Databases
        //item_DB.BuildDatabase();

    }



    /////////////////////////////////////////////////////////////////
}
