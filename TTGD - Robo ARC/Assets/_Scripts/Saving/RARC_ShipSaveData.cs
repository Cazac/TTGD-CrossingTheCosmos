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


    public List<RARC_Event> shipCurrentEvents_List;
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
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Scrap,
        };

        shipResource_Fuel = new RARC_Resource
        {
            resourceName = "Fuel",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Fuel,
        };

        shipResource_Food = new RARC_Resource
        {
            resourceName = "Food",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Food,
        };


        shipCurrentEvents_List = new List<RARC_Event>();

        //Create a new crew
        shipData_Crew_List = new List<RARC_Crew>();
        shipData_Bots_List = new List<RARC_Crew>();
        


    }

    /////////////////////////////////////////////////////////////////
}
