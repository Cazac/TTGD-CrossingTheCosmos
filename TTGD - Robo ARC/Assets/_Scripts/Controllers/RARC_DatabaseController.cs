using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_DatabaseController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_DatabaseController Instance;

    ////////////////////////////////

    [Header("Access Directly to Scriptable")]
    public RARC_Crew_SO crew_DB;

    [Header("Access Directly to Scriptable")]
    public RARC_Planet_SO planet_SO;

    [Header("Access Container of Event Scriptables")]
    public RARC_EventData events_DB;

    [Header("Access Container of Resources")]
    public RARC_ResourceData resources_DB;

    [Header("Access Container of Icons")]
    public RARC_IconData icons_DB;

    [Header("Access Container of Music")]
    public RARC_MusicData music_DB;

    ////////////////////////////////

    [Header("Current Ship Save Data")]
    public int ship_SaveSlot;
    public RARC_ShipSaveData ship_SaveData;
    public RARC_PlayerSaveData player_SaveData;

    [Header("All Weeks Save Data Files")]
    public List<RARC_ShipSaveData> saveDataSet1_List = new List<RARC_ShipSaveData>();
    public List<RARC_ShipSaveData> saveDataSet2_List = new List<RARC_ShipSaveData>();
    public List<RARC_ShipSaveData> saveDataSet3_List = new List<RARC_ShipSaveData>();

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        if (Instance == null)
        {
            //Setup Singleton
            Instance = this;

            //Set the Gameobject To Not Self Destruct On Scene Change
            DontDestroyOnLoad(gameObject);

            //Call all Build Database Methods
            BuildDatabase();

            //Load Player Settings
            LoadPlayerData();
        }
        else
        {
            //Remove this database if another exists
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SavePlayerData();
    }

    /////////////////////////////////////////////////////////////////

    public List<RARC_ShipSaveData> FindGameData(int saveSlot)
    {
        //List Of Saves
        List<RARC_ShipSaveData> saveData_List = new List<RARC_ShipSaveData>();

        try
        {
            //Loop each possible week save file
            for (int i = 0; i < 53; i++)
            {
                string fileName = "Saves/ShipData " + saveSlot + " (Week " + i + ").rarc";
                ship_SaveData = RARC_Serializer.Load<RARC_ShipSaveData>(fileName);

                //If The File Does Not Exist ??? (COULD BE INF LOAD RECURSION???)
                if (ship_SaveData != null)
                {
                    saveData_List.Add(ship_SaveData);
                }
                else
                {
                    //print("Test Code: BLANK");
                }
            }
        }
        catch (Exception error)
        {
            //Error Message
            Debug.Log("Save System is Broken, Wrong Version? " + error);
            Application.Quit();
        }

        return saveData_List;
    }

    public void SaveShipData()
    {
        string fileName = "Saves/ShipData " + ship_SaveSlot + " (Week " + ship_SaveData.shipInfo_WeeksSurvived + ").rarc";

        //Save the Data into a file
        RARC_Serializer.Save("Saves/ShipData " + ship_SaveSlot + " (Week " + ship_SaveData.shipInfo_WeeksSurvived + ").rarc", ship_SaveData);
    }

    public void DeleteShipData(int saveSlot)
    {
        //Loop each possible week save file
        for (int i = 0; i < 53; i++)
        {
            string fileName = "Saves/ShipData " + saveSlot + " (Week " + i + ").rarc";
            RARC_Serializer.DeleteFile(fileName);
        }
    }

    /////////////////////////////////////////////////////////////////

    public void SavePlayerData()
    {
        print("Test Code: Saving Player");

        string fileName = "Saves/GameSettingsData.ctc";

        //Save the Data into a file
        RARC_Serializer.Save(fileName, player_SaveData);
    }

    public void LoadPlayerData()
    {
        print("Test Code: Loading Player");

        try
        {
            //Get File 
            string fileName = "Saves/GameSettingsData.ctc";
            player_SaveData = RARC_Serializer.Load<RARC_PlayerSaveData>(fileName);

            //Check If Created Yet
            if (player_SaveData == null)
            {
                player_SaveData = new RARC_PlayerSaveData();
                player_SaveData.CreateNewSave();
            }
        }
        catch (Exception error)
        {
            //Error Message
            Debug.Log("Save System is Broken, Wrong Version? " + error);
            Application.Quit();
        }
    }

    /////////////////////////////////////////////////////////////////

    private void BuildDatabase()
    {
        //Build Databases
        resources_DB.BuildDatabase();
    }

    /////////////////////////////////////////////////////////////////
}
