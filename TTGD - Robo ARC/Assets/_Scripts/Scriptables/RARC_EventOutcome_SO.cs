using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Event Outcome", menuName = "Scriptables/New Event Outcome")]
public class RARC_EventOutcome_SO : ScriptableObject
{
    ////////////////////////////////

    [Header("Additional Event")]
    public RARC_Event_SO outcomeNextEvent;

    [Header("Value Change - 1")]
    public OutcomeType outcomeType1;
    public int outcomeValue1;

    [Header("Value Change - 2")]
    public OutcomeType outcomeType2;
    public int outcomeValue2;

    [Header("Value Change - 3")]
    public OutcomeType outcomeType3;
    public int outcomeValue3;

    ////////////////////////////////

    public enum OutcomeType
    {
        NULL,

        HULL_CHANGE,

        CREW_CHANGE,
        ROBOT_CHANGE,

        SCRAPMETAL_CHANGE,
        FUEL_CHANGE,
        FOOD_CHANGE,

        TITANIUM_CHANGE,
        SILICON_CHANGE,
        CARBON_CHANGE,
        ORGANICS_CHANGE,
        MEDKITS_CHANGE,

        HYDROGEN_CHANGE,
        NITROGEN_CHANGE,

        GAMEOVER
    }

    /////////////////////////////////////////////////////////////////
}
