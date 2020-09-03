using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_Planet_SO : ScriptableObject
{
    ////////////////////////////////

    [Header("All Planets")]
    public List<string> allPlanetNames_List;

    ////////////////////////////////

    [Header("Rocky Planets")]
    public List<Sprite> planetSprites_Rocky;

    public List<Color> colorPaletteMain_Rocky;
    public List<Color> colorPaletteSecondary_Rocky;









    /////////////////////////////////////////////////////////////////

    public void GeneratePlanet()
    {








    }



   


    public RARC_Planet GeneratePlanet_Rocky()
    {
        //Create New Planet
        RARC_Planet newPlanet = new RARC_Planet();

        //Name
        newPlanet.planetName = allPlanetNames_List[Random.Range(0, allPlanetNames_List.Count)];

        //Colors
        newPlanet.primaryColor = colorPaletteMain_Rocky[Random.Range(0, colorPaletteMain_Rocky.Count)];
        newPlanet.secondaryColor = colorPaletteSecondary_Rocky[Random.Range(0, colorPaletteSecondary_Rocky.Count)];

        //Sprite
        newPlanet.planetSprite = planetSprites_Rocky[Random.Range(0, planetSprites_Rocky.Count)];

        //Moon Generation
        int planetMoonCount = Random.Range(0, 4);
        for (int i = 0; i < planetMoonCount; i++)
        {
            newPlanet.planetMoonSprite_List.Add(GenerateMoon()); 
        }


        //Visual Rotation
        newPlanet.planetRotation = Random.Range(0, 360);

        //Time to reach planet
        newPlanet.planetTravelTime = Random.Range(1, 4);


        //newPlanet.planetEvents_List = new List<string>();
        //newPlanet.planetResources_List;

        //Return New Planet
        return newPlanet;
    }

    public RARC_Planet GenerateMoon()
    {
        //Create New Planet
        RARC_Planet newMoon = new RARC_Planet();


        return newMoon;
    }

    /////////////////////////////////////////////////////////////////
}
