using System;
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
    public int ship_SaveSlot;
    public RARC_ShipSaveData ship_SaveData;

    public List<RARC_ShipSaveData> saveDataSet1_List = new List<RARC_ShipSaveData>();
    public List<RARC_ShipSaveData> saveDataSet2_List = new List<RARC_ShipSaveData>();
    public List<RARC_ShipSaveData> saveDataSet3_List = new List<RARC_ShipSaveData>();


    //[Header("Account Save Data")]
    //public TM_SettingsSaveData settings_SaveData;
    //public TM_UnlocksSaveData unlock_SaveData;


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
        }
        else
        {
            //Remove this database if another exists
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {

    }

    /////////////////////////////////////////////////////////////////

    private void LoadShipData()
    {
        try
        {
            //player_DB = RARC_Serializer.Load<TC_Player>("PlayerData.tc");

            //If The File Does Not Exist ??? (COULD BE INF LOAD RECURSION???)
           // if (player_DB == null)
            //{
                //NewPlayerData();
           // }
           // else
           // {
                //BackupPlayerData();
            //}
        }
        catch (Exception error)
        {
            //Error Message
            //UnityEditor.EditorUtility.DisplayDialog("Save System is Broken, Wrong Version?", e.ToString(), "Quit"); 
            Debug.Log("Save System is Broken, Wrong Version? " + error);
            Application.Quit();
        }
    }

    public List<RARC_ShipSaveData> FindGameData(int saveSlot)
    {
        //List Of Saves
        List<RARC_ShipSaveData> saveData_List = new List<RARC_ShipSaveData>();

        try
        {
            //Loop each possible week save file
            for (int i = 0; i < 52; i++)
            {
                string fileName = "ShipData " + saveSlot + " (Week " + i + ").rarc";
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
        string fileName = "ShipData " + ship_SaveSlot + " (Week " + ship_SaveData.shipInfo_WeeksSurvived + ").rarc";
        print("Test Code: Save As " + fileName);

        //Save the Data into a file
        RARC_Serializer.Save("ShipData " + ship_SaveSlot + " (Week " + ship_SaveData.shipInfo_WeeksSurvived + ").rarc", ship_SaveData);
    }


    /////////////////////////////////////////////////////////////////

    private void BuildDatabase()
    {
        //Build Databases
        //item_DB.BuildDatabase();


        //Save Player Data
        //SaveShipData();
    }

    /////////////////////////////////////////////////////////////////
}
