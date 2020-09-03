using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_ShipSaveData
{
    ////////////////////////////////

    public int shipInfo_WeeksSurvived;

    public int shipStat_Iron;
    public int shipStat_Water;
    public int shipStat_Fuel;
    public int shipStat_Food;

    ////////////////////////////////

    public List<string> shipData_Crew_List;

    public List<string> shipData_ResearchComplete_List;
    public List<string> shipData_ResearchAvalible_List;
    public List<string> shipData_ResearchUnavalible_List;


    public string[] shipData_Rooms_Arr;


    public string shipData_NavigationDestination;
    public string shipData_NavigationTripTime;
    public string shipData_NavigationTripProgress;


    /////////////////////////////////////////////////////////////////


    public void CreateNewSave()
    {
        shipInfo_WeeksSurvived = 0;




        //Create a new crew




    }
}
