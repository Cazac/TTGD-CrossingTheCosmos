using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_ResourceData : MonoBehaviour
{
    [Header("Basics")]
    public RARC_Resource scrap_Resource;
    public RARC_Resource fuel_Resource;
    public RARC_Resource food_Resource;

    [Header("Crafted")]
    public RARC_Resource medkit_Resource;
    public RARC_Resource repairkit_Resource;

    [Header("Metals")]
    public RARC_Resource copper_Resource;
    public RARC_Resource platinum_Resource;
    public RARC_Resource silicon_Resource;
    public RARC_Resource carbon_Resource;
    public RARC_Resource sulfur_Resource;

    [Header("Gases")]
    public RARC_Resource hydrogen_Resource;
    public RARC_Resource nitrogen_Resource;
    public RARC_Resource helium_Resource;

    /////////////////////////////////////////////////////////////////

    public void BuildDatabase()
    {
        scrap_Resource = new RARC_Resource
        {
            resourceName = "Scrap",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Scrap,
        };

        fuel_Resource = new RARC_Resource
        {
            resourceName = "Fuel",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Fuel,
        };

        food_Resource = new RARC_Resource
        {
            resourceName = "Food",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Food,
        };





        medkit_Resource = new RARC_Resource
        {
            resourceName = "Medkit",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Medkit,
        };

        repairkit_Resource = new RARC_Resource
        {
            resourceName = "Repairkit",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Repairkit,
        };







        copper_Resource = new RARC_Resource
        {
            resourceName = "Copper",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Copper,
        };

        platinum_Resource = new RARC_Resource
        {
            resourceName = "Platinum",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Platinum,
        };

        silicon_Resource = new RARC_Resource
        {
            resourceName = "Silicon",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Silicon,
        };

        carbon_Resource = new RARC_Resource
        {
            resourceName = "Carbon",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Carbon,
        };

        sulfur_Resource = new RARC_Resource
        {
            resourceName = "Sulfur",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Sulfur,
        };




        hydrogen_Resource = new RARC_Resource
        {
            resourceName = "Hydrogen",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Hydrogen,
        };

        nitrogen_Resource = new RARC_Resource
        {
            resourceName = "Nitrogen",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Nitrogen,
        };

        helium_Resource = new RARC_Resource
        {
            resourceName = "Helium",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Helium,
        };
        
    }

}
