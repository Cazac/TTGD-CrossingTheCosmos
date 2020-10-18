using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Event Requirement", menuName = "Scriptables/New Event Requirement")]
public class RARC_EventRequirement_SO : ScriptableObject
{
    ////////////////////////////////

    [Header("Event Requirement 1")]
    public RequirementType requirementType1;
    public int requirementValue1;

    [Header("Event Requirement 2")]
    public RequirementType requirementType2;
    public int requirementValue2;

    [Header("Event Requirement 3")]
    public RequirementType requirementType3;
    public int requirementValue3;

    ////////////////////////////////

    public enum RequirementType
    {
        NULL,

        CREW_HIGHER,
        CREW_LOWER,

        ROBOT_HIGHER,
        ROBOT_LOWER,

        HULL_HIGHER,
        HULL_LOWER,

        SCRAP_HIGHER,
        SCRAP_LOWER,

        FUEL_HIGHER,
        FUEL_LOWER,

        FOOD_HIGHER,
        FOOD_LOWER,

        HASROOM_MEDBAY,
        HASROOM_KITCHEN,
    }

    /////////////////////////////////////////////////////////////////
}
