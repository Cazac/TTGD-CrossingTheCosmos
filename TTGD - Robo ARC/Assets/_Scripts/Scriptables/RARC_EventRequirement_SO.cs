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

        TITANIUM_HIGHER,
        TITANIUM_LOWER,

        SILICON_HIGHER,
        SILICON_LOWER,

        CARBON_HIGHER,
        CARBON_LOWER,

        ORGANICS_HIGHER,
        ORGANIC_LOWER,

        HYDROGEN_HIGHER,
        HYDROGEN_LOWER,

        NITROGEN_HIGHER,
        NITROGEN_LOWER,

        ROOMCOUNT_ALL_HIGHER,
        ROOMCOUNT_ALL_LOWER,

        ROOMCOUNT_ASTROMETRICS_HIGHER,
        ROOMCOUNT_ASTROMETRICS_LOWER,

        ROOMCOUNT_CLONING_HIGHER,
        ROOMCOUNT_CLONING_LOWER,

        ROOMCOUNT_FACTORY_HIGHER,
        ROOMCOUNT_FACTORY_LOWER,

        ROOMCOUNT_HYDROPONICS_HIGHER,
        ROOMCOUNT_HYDROPONICS_LOWER,

        ROOMCOUNT_KITCHEN_HIGHER,
        ROOMCOUNT_KITCHEN_LOWER,

        ROOMCOUNT_MEDBAY_HIGHER,
        ROOMCOUNT_MEDBAY_LOWER,

        ROOMCOUNT_QUARTERS_HIGHER,
        ROOMCOUNT_QUARTERS_LOWER,

        ROOMCOUNT_STORAGE_HIGHER,
        ROOMCOUNT_STORAGE_LOWER,
        
    }

    /////////////////////////////////////////////////////////////////
}
