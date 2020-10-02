using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_Resource
{
    ////////////////////////////////
   
    public string resourceName;
    public int resourceCount;
    public ResourceType resourceType;

    public RARC_Resource(string resourceName, int resourceCount, ResourceType resourceType)
    {
        this.resourceName = resourceName;
        this.resourceType = resourceType;
        this.resourceCount = resourceCount;
    }

    public enum ResourceType
    {
        NULL,

        ScrapMetal,
        Fuel,
        Food,

        Medkit,
        Titanium,
        Silicon,
        Carbon,
        Organics,

        Hydrogen,
        Nitrogen
    }

    /////////////////////////////////////////////////////////////////
}
