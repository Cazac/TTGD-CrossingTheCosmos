using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_Planet
{
    ////////////////////////////////

    //Name
    public string planetName;

    //Sprites
    public int planetSprite_Main;
    public int planetSprite_Secondary;
    public List<RARC_Planet> planetMoonPlanet_List;

    //Rotation
    public int planetRotation;

    //Colors
    public string primaryColor;
    public string secondaryColor;

    //Traveling
    public int planetTravelTime;

    //Info
    public int planetRiskFactor;
    public RARC_Event planetEvent;
    public List<Tuple<int, int, RARC_Resource>> planetResources_List;

    //Enum
    public PlanetType planetType;

    public enum PlanetType
    {
        Rocky,
        Lava,
        Living,
        Icy,
        Gassy,

        Moon,
    }

    /////////////////////////////////////////////////////////////////
}