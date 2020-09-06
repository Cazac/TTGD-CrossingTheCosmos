using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet", menuName = "Scriptables/New Planet")]
public class RARC_Planet_SO : ScriptableObject
{
    ////////////////////////////////

    [Header("All Planets")]
    public List<string> allPlanetNames_List;

    ////////////////////////////////

    [Header("Rocky Planets")]
    public List<Sprite> planetSpritesMain_Rocky;
    public List<Sprite> planetSpritesSecondary_Rocky;
    public List<Color> colorPaletteMain_Rocky;
    public List<Color> colorPaletteSecondary_Rocky;

    [Header("Moons")]
    public List<Sprite> planetSpritesMain_Moon;
    public List<Sprite> planetSpritesSecondary_Moon;
    public List<Color> colorPaletteMain_Moon;
    public List<Color> colorPaletteSecondary_Moon;



    /////////////////////////////////////////////////////////////////

    public RARC_Planet GeneratePlanet_Rocky()
    {
        //Create New Planet
        RARC_Planet newPlanet = new RARC_Planet();

        newPlanet.planetType = RARC_Planet.PlanetType.Rocky;

        //Name
        newPlanet.planetName = allPlanetNames_List[Random.Range(0, allPlanetNames_List.Count)];

        //Sprite
        newPlanet.planetSprite_Main = Random.Range(0, planetSpritesMain_Rocky.Count);
        newPlanet.planetSprite_Secondary = Random.Range(0, planetSpritesSecondary_Rocky.Count);

        //Colors
        newPlanet.primaryColor = colorPaletteMain_Rocky[Random.Range(0, colorPaletteMain_Rocky.Count)];
        newPlanet.secondaryColor = colorPaletteSecondary_Rocky[Random.Range(0, colorPaletteSecondary_Rocky.Count)];

        //Moon Generation
        newPlanet.planetMoonPlanet_List = new List<RARC_Planet>();
        int planetMoonCount = Random.Range(0, 3);
        for (int i = 0; i < planetMoonCount; i++)
        {
            newPlanet.planetMoonPlanet_List.Add(GenerateMoon());
        }


        //Visual Rotation
        newPlanet.planetRotation = Random.Range(-90, 90);

        //Time to reach planet
        newPlanet.planetTravelTime = Random.Range(1, 4);


        //newPlanet.planetEvents_List = new List<string>();
        //newPlanet.planetResources_List;

        RARC_Resource newResource = new RARC_Resource
        {
            resourceName = "Scrap",
            resourceCount = 0,
            resourceType = RARC_Resource.ResourceType.Food
        };


        //Return New Planet
        return newPlanet;
    }

    public RARC_Planet GenerateMoon()
    {
        //Create New Planet
        RARC_Planet newMoon = new RARC_Planet();

        newMoon.planetType = RARC_Planet.PlanetType.Moon;

        //Sprite
        newMoon.planetSprite_Main = Random.Range(0, planetSpritesMain_Moon.Count);
        newMoon.planetSprite_Secondary = Random.Range(0, planetSpritesSecondary_Moon.Count);

        //Colors
        newMoon.primaryColor = colorPaletteMain_Moon[Random.Range(0, colorPaletteMain_Moon.Count)];
        newMoon.secondaryColor = colorPaletteSecondary_Moon[Random.Range(0, colorPaletteSecondary_Moon.Count)];

        //Visual Rotation
        //newMoon.planetRotation = Random.Range(0, 360);

        return newMoon;
    }

    /////////////////////////////////////////////////////////////////

    public List<RARC_Resource> GenerateResources_Rocky()
    {

        List<RARC_Resource> liwt = new List<RARC_Resource>();

        int chanceOfScrap = Random.Range(0, 100);


        //int chanceOfScrap = Random.RandomRange();


        return liwt;
    }

    /////////////////////////////////////////////////////////////////
}
