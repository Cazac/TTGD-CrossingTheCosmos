using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Event Outcome", menuName = "Scriptables/New Event Outcome")]
public class RARC_EventOutcome_SO : ScriptableObject
{
    ////////////////////////////////

    [Header("Event Outcome 1")]
    public RARC_Event_SO outcomeNextEvent1;
    public OutcomeType outcomeType1;
    public int outcomeValue1;

    [Header("Event Outcome 2")]
    public RARC_Event_SO outcomeNextEvent2;
    public OutcomeType outcomeType2;
    public int outcomeValue2;

    [Header("Event Outcome 3")]
    public RARC_Event_SO outcomeNextEvent3;
    public OutcomeType outcomeType3;
    public int outcomeValue3;

    ////////////////////////////////

    public enum OutcomeType
    {
        NULL,
        CREW_CHANGE,
        ROBOT_CHANGE,
        HULL_CHANGE,

        SCRAP_CHANGE,
        FUEL_CHANGE,
        FOOD_CHANGE,
    }

    /////////////////////////////////////////////////////////////////
}
