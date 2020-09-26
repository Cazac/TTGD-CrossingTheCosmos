using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_ShipSaveData
{
    ////////////////////////////////

    public int shipInfo_WeeksSurvived;

    ////////////////////////////////

    public List<RARC_Crew> shipData_Crew_List;
    public List<RARC_Crew> shipData_Bots_List;

    ////////////////////////////////

    public List<string> shipData_ResearchComplete_List;
    public List<string> shipData_ResearchAvalible_List;
    public List<string> shipData_ResearchUnavalible_List;

    ////////////////////////////////

    public string[] shipData_Rooms_Arr;

    ////////////////////////////////

    public RARC_Planet shipData_currentLocation;
    public RARC_Planet shipData_NavigationDestination;
    public int shipData_NavigationTripProgress;

    ////////////////////////////////


    public int shipHullHealth;

    public List<RARC_Event> shipCurrentEvents_List;
    public List<RARC_Event> shipAvaliblePlanetEvents_List;
    //public List<RARC_Event_SO> buildCurrentEvents_List;

    ////////////////////////////////

    //public List<RARC_Event_SO> shipPossibleEvents_Planets_List;
    //public List<RARC_Event_SO> shipPossibleEvents_Travel_List;
    //public List<RARC_Event_SO> shipPossibleEvents_Crew_List;

    ////////////////////////////////

    public RARC_Resource shipResource_Scrap;
    public RARC_Resource shipResource_Fuel;
    public RARC_Resource shipResource_Food;
    public List<RARC_Resource> shipStorage_List;

    /////////////////////////////////////////////////////////////////

    public void CreateNewSave()
    {
        //Set Weeks
        shipInfo_WeeksSurvived = 0;

        //Set New Resource Bases
        shipResource_Scrap = new RARC_Resource
        {
            resourceName = "Scrap",
            resourceCount = 10,
            resourceType = RARC_Resource.ResourceType.Scrap,
        };

        shipResource_Fuel = new RARC_Resource
        {
            resourceName = "Fuel",
            resourceCount = 5,
            resourceType = RARC_Resource.ResourceType.Fuel,
        };

        shipResource_Food = new RARC_Resource
        {
            resourceName = "Food",
            resourceCount = 50,
            resourceType = RARC_Resource.ResourceType.Food,
        };



        shipHullHealth = 100;


        shipStorage_List = new List<RARC_Resource>();

        //Events
        shipCurrentEvents_List = new List<RARC_Event>();
        shipAvaliblePlanetEvents_List = new List<RARC_Event>();



        //shipAvaliblePlanetEvents_List.Add();

        //Create a new crew
        shipData_Crew_List = new List<RARC_Crew>();
        shipData_Bots_List = new List<RARC_Crew>();

        RARC_Crew debugCrew1 = new RARC_Crew();
        RARC_Crew debugCrew2 = new RARC_Crew();
        RARC_Crew debugCrew3 = new RARC_Crew();
        RARC_Crew debugCrew4 = new RARC_Crew();
        RARC_Crew debugCrew5 = new RARC_Crew();
        RARC_Crew debugCrew6 = new RARC_Crew();
        RARC_Crew debugCrew7 = new RARC_Crew();
        RARC_Crew debugCrew8 = new RARC_Crew();
        RARC_Crew debugCrew9 = new RARC_Crew();
        RARC_Crew debugCrew10 = new RARC_Crew();

        //*
        shipData_Crew_List.Add(debugCrew1);
        shipData_Crew_List.Add(debugCrew2);
        shipData_Crew_List.Add(debugCrew3);
        shipData_Crew_List.Add(debugCrew4);
        shipData_Crew_List.Add(debugCrew5);
        shipData_Crew_List.Add(debugCrew6);
        shipData_Crew_List.Add(debugCrew7);
        shipData_Crew_List.Add(debugCrew8);
        shipData_Crew_List.Add(debugCrew9);
        //*/

        shipData_Crew_List.Add(debugCrew10);
    }

    /////////////////////////////////////////////////////////////////
}
