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

    public enum ResourceType
    {
        Scrap,
        Fuel,
        Food,
    }

    /////////////////////////////////////////////////////////////////
}
