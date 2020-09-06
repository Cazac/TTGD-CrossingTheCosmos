using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_Planet : MonoBehaviour
{
    ////////////////////////////////

    [Header("Name")]
    public string planetName;

    [Header("Sprites")]
    public Sprite planetSprite;
    public List<RARC_Planet> planetMoonSprite_List;

    [Header("Rotation")]
    public int planetRotation;

    [Header("Colors")]
    public Color primaryColor;
    public Color secondaryColor;

    [Header("Traveling")]
    public int planetTravelTime;

    [Header("Lists")]
    public List<string> planetEvents_List;
    public Tuple<float, string> planetResources_List;

    /////////////////////////////////////////////////////////////////
}
