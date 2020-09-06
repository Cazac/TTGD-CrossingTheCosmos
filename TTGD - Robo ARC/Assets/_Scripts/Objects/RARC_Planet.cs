using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_Planet
{
    ////////////////////////////////

    [Header("Name")]
    public string planetName;

    [Header("Sprites")]
    public int planetSprite_Main;
    public int planetSprite_Secondary;
    public List<RARC_Planet> planetMoonPlanet_List;

    [Header("Rotation")]
    public int planetRotation;

    [Header("Colors")]
    public Color32 primaryColor;
    public Color32 secondaryColor;

    [Header("Traveling")]
    public int planetTravelTime;

    [Header("Lists")]
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