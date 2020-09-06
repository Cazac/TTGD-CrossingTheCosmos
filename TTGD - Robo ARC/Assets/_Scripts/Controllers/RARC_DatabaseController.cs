﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_DatabaseController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_DatabaseController Instance;

    ////////////////////////////////

    [Header("Access To Data With Scriptables")]
    public RARC_Crew_SO crew_DB;


    [Header("Access Directly to Scriptable")]
    public RARC_Planet_SO planet_SO;

    ////////////////////////////////

    [Header("Current Ship Save Data")]
    public int ship_SaveSlot;
    public RARC_ShipSaveData ship_SaveData;

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

    private void BuildDatabase()
    {
        //Build Databases
        //item_DB.BuildDatabase();


        //Save Player Data
        //SaveShipData();
    }

    /////////////////////////////////////////////////////////////////
}
