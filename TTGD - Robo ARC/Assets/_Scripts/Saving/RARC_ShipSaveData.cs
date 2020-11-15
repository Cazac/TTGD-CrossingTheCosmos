using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_ShipSaveData
{
    ////////////////////////////////

    [Header("Ship Info")]
    public int shipInfo_WeeksSurvived;

    ////////////////////////////////

    [Header("Ship Crew")]
    public List<RARC_Crew> shipData_Crew_List;
    public List<RARC_Crew> shipData_Bots_List;

    ////////////////////////////////

    [Header("Ship Rooms")]
    public RARC_Room.RoomType[] shipData_Rooms_Arr;

    ////////////////////////////////

    [Header("Location Info")]
    public RARC_Planet shipData_currentLocation;
    public RARC_Planet shipData_NavigationDestination;
    public int shipData_NavigationTripProgress;

    ////////////////////////////////

    [Header("Events Possible - Travel")]
    public List<RARC_Event> shipCurrentTravelEvents_List;
    public List<RARC_Event> shipAvalibleTravelEvents_List;
    public List<RARC_Event> shipBlacklistTravelEvents_List;

    [Header("Events Possible - Planets")]
    public List<RARC_Event> shipCurrentPlanetEvents_List;
    public List<RARC_Event> shipAvaliblePlanetEvents_List;
    public List<RARC_Event> shipBlacklistPlanetEvents_List;

    [Header("Event Info")]
    public int travelEventsMissed;
    public int planetEventsMissed;

    ////////////////////////////////

    [Header("Ship Resources")]
    public int shipHullHealth;
    public RARC_Resource shipResource_Scrap;
    public RARC_Resource shipResource_Fuel;
    public RARC_Resource shipResource_Food;
    public List<RARC_Resource> shipStorage_List;

    [Header("Navigation Refrsh Times")]
    public int navigationRefreshTimes_Left;
    public int navigationRefreshTimes_Middle;
    public int navigationRefreshTimes_Right;

    /////////////////////////////////////////////////////////////////

    public void CreateNewSave()
    {
        //Set Weeks
        shipInfo_WeeksSurvived = 0;

        //Ship Rooms
        shipData_Rooms_Arr = new RARC_Room.RoomType[16];
        shipData_Rooms_Arr[0] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[1] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[2] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[3] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[4] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[5] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[6] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[7] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[8] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[9] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[10] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[11] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[12] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[13] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[14] = RARC_Room.RoomType.EMPTY;
        shipData_Rooms_Arr[15] = RARC_Room.RoomType.EMPTY;

        //SET FIRST LOCATION AS EASTH????
        shipData_currentLocation = null;
        shipData_NavigationDestination = null;
        shipData_NavigationTripProgress = 0;

        //Events
        shipCurrentTravelEvents_List = new List<RARC_Event>();
        shipAvalibleTravelEvents_List = new List<RARC_Event>();
        shipBlacklistTravelEvents_List = new List<RARC_Event>();
        shipCurrentPlanetEvents_List = new List<RARC_Event>();
        shipAvaliblePlanetEvents_List = new List<RARC_Event>();
        shipBlacklistPlanetEvents_List = new List<RARC_Event>();

        //Intro Event
        shipCurrentTravelEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_ANewHope));

        //Travel Events
        shipAvalibleTravelEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_AbandonedShip));

        //Planet Events
        shipAvaliblePlanetEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_AncientRuins));

        //Event Missfires
        travelEventsMissed = 1;
        planetEventsMissed = 1;

        //Navigation Refreshs
        navigationRefreshTimes_Left = 0;
        navigationRefreshTimes_Middle = 0;
        navigationRefreshTimes_Right = 0;

        //Set New Resource Bases and Storage
        shipHullHealth = 100;
        shipResource_Scrap = new RARC_Resource("Scrap", 30, RARC_Resource.ResourceType.Scrap);
        shipResource_Fuel = new RARC_Resource("Fuel", 50, RARC_Resource.ResourceType.Fuel);
        shipResource_Food = new RARC_Resource("Food", 50, RARC_Resource.ResourceType.Food);
        shipStorage_List = new List<RARC_Resource>();

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
        int startingBotCount = 0;
        for (int i = 0; i < startingBotCount; i++)
        {
            //RARC_Crew newCrewMember = new RARC_Crew();
            //shipData_Bots_List.Add(newCrewMember);
        }




        // /*

        //Debug Items

        //shipResource_Scrap = new RARC_Resource("Scrap", 999, RARC_Resource.ResourceType.Scrap);
        //shipResource_Fuel = new RARC_Resource("Fuel", 999, RARC_Resource.ResourceType.Fuel);
        //shipResource_Food = new RARC_Resource("Food", 999, RARC_Resource.ResourceType.Food);
        //shipStorage_List.Add(new RARC_Resource("Titanium", 999, RARC_Resource.ResourceType.Titanium));
        shipStorage_List.Add(new RARC_Resource("Organics", 20, RARC_Resource.ResourceType.Organics));
        //shipStorage_List.Add(new RARC_Resource("Carbon", 999, RARC_Resource.ResourceType.Carbon));
        //shipStorage_List.Add(new RARC_Resource("Silicon", 999, RARC_Resource.ResourceType.Silicon));
        shipStorage_List.Add(new RARC_Resource("Hydrogen", 20, RARC_Resource.ResourceType.Hydrogen));

        // */

    }

    /////////////////////////////////////////////////////////////////
}
