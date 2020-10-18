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
        (
            "Scrap",
            30,
            RARC_Resource.ResourceType.Scrap
        );

        shipResource_Fuel = new RARC_Resource
        (
            "Fuel",
            50,
            RARC_Resource.ResourceType.Fuel
        );

        shipResource_Food = new RARC_Resource
        (
            "Food",
            80,
            RARC_Resource.ResourceType.Food
        );


        shipHullHealth = 100;


        shipStorage_List = new List<RARC_Resource>();

        //Events
        shipCurrentEvents_List = new List<RARC_Event>();
        shipAvaliblePlanetEvents_List = new List<RARC_Event>();



        //Create new Lists
        shipData_Crew_List = new List<RARC_Crew>();
        shipData_Bots_List = new List<RARC_Crew>();

        //Loop Spawning New Crew
        int startingCrewCount = 12;
        for (int i = 0; i < startingCrewCount; i++)
        {
            RARC_Crew newCrewMember = new RARC_Crew();
            shipData_Crew_List.Add(newCrewMember);
        }

        //Loop Spawning New Bots
        int startingBotCount = 1;
        for (int i = 0; i < startingBotCount; i++)
        {
            //RARC_Crew newCrewMember = new RARC_Crew();
            //shipData_Bots_List.Add(newCrewMember);
        }

        //shipAvaliblePlanetEvents_List.Add();


    }

    /////////////////////////////////////////////////////////////////
}
