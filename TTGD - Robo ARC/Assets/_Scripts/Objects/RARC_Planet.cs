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
    public Color32 primaryColor;
    public Color32 secondaryColor;

    //Traveling
    public int planetTravelTime;

    //Lists
    public List<string> planetEvents_List;
    public Tuple<float, string> planetResources_List;

    public PlanetType planetType;

    public enum PlanetType
    {
        Moon,
        Rocky,
        Gassy,

    }

    /////////////////////////////////////////////////////////////////
}