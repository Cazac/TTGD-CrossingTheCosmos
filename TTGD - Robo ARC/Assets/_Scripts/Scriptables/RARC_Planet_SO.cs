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

    [Header("Rocky Planets - Sprites")]
    public List<Sprite> planetSpritesMain_Rocky;
    public List<Sprite> planetSpritesSecondary_Rocky;
    public List<Color> colorPaletteMain_Rocky;
    public List<Color> colorPaletteSecondary_Rocky;

    [Header("Rocky Planets - Resources")]
    public int minAmountOf_Scrap_Rocky;
    public int maxAmountOf_Scrap_Rocky;

    [Range(0, 100)]
    public int chanceOf_Copper_Rocky;
    public int minAmountOf_Copper_Rocky;
    public int maxAmountOf_Copper_Rocky;
    [Range(0, 100)]
    public int chanceOf_Silicon_Rocky;
    public int minAmountOf_Silicon_Rocky;
    public int maxAmountOf_Silicon_Rocky;

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
        newPlanet.primaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteMain_Rocky[Random.Range(0, colorPaletteMain_Rocky.Count)]);
        newPlanet.secondaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteSecondary_Rocky[Random.Range(0, colorPaletteSecondary_Rocky.Count)]);

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

        //Events / Resources
        newPlanet.planetEvent = GenerateAvalibleEvent_Rocky();
        newPlanet.planetResources_List = GenerateResources_Rocky();

        //Risk
        newPlanet.planetRiskFactor = Random.Range(0, 10);

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
        newMoon.primaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteMain_Moon[Random.Range(0, colorPaletteMain_Moon.Count)]);
        newMoon.secondaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteSecondary_Moon[Random.Range(0, colorPaletteSecondary_Moon.Count)]);

        //Visual Rotation
        //newMoon.planetRotation = Random.Range(0, 360);

        return newMoon;
    }

    /////////////////////////////////////////////////////////////////

    public List<System.Tuple<int, int, RARC_Resource>> GenerateResources_Rocky()
    {
        //List of Resources For the Planet
        List<System.Tuple<int, int, RARC_Resource>> resources_List = new List<System.Tuple<int, int, RARC_Resource>>();

        //Implement later - ???
        List<int> Chance_List = new List<int>();
        Chance_List.Add(30);
        Chance_List.Add(40);
        Chance_List.Add(50);
        Chance_List.Add(60);
        Chance_List.Add(70);
        Chance_List.Add(80);


        //Scrap
        int amountOfScrap = Random.Range(0, 100);
        System.Tuple<int, int, RARC_Resource> scrap_Resource = new System.Tuple<int, int, RARC_Resource>(100, amountOfScrap, RARC_DatabaseController.Instance.resources_DB.scrap_Resource);
        resources_List.Add(scrap_Resource);

        //Copper
        int chanceOfCopper = Random.Range(0, 100);
        if (chanceOfCopper <= chanceOf_Copper_Rocky)
        {
            //Get Copper
            int possiblityOfCopper = Chance_List[Random.Range(0, Chance_List.Count)];
            int amountOfCopper = Random.Range(0, 100);
            System.Tuple<int, int, RARC_Resource> copper_Resource = new System.Tuple<int, int, RARC_Resource>(possiblityOfCopper, amountOfCopper, RARC_DatabaseController.Instance.resources_DB.copper_Resource);
            resources_List.Add(copper_Resource);
        }

        //Silicon
        int chanceOfSilicon = Random.Range(0, 100);
        if (chanceOfSilicon <= chanceOf_Silicon_Rocky)
        {
            //Get Silicon
            int possiblityOfSilicon = Chance_List[Random.Range(0, Chance_List.Count)];
            int amountOfSilicon = Random.Range(0, 100);
            System.Tuple<int, int, RARC_Resource> silicon_Resource = new System.Tuple<int, int, RARC_Resource>(possiblityOfSilicon, amountOfSilicon, RARC_DatabaseController.Instance.resources_DB.silicon_Resource);
            resources_List.Add(silicon_Resource);
        }

        //Return
        return resources_List;
    }

    public RARC_Event GenerateAvalibleEvent_Rocky()
    {
        //New Event
        RARC_Event newEvent = null;

        //Check only if Event is Avaliable
        if (RARC_DatabaseController.Instance.ship_SaveData.shipAvaliblePlanetEvents_List.Count != 0)
        {
            //Get Chance Of Event
            int chanceOfEvent = Random.Range(0, 100);
            if (chanceOfEvent <= 30)
            {
                //Random Event
                newEvent = RARC_DatabaseController.Instance.ship_SaveData.shipAvaliblePlanetEvents_List[Random.Range(0, RARC_DatabaseController.Instance.ship_SaveData.shipAvaliblePlanetEvents_List.Count)];
            }
        }

        //Return
        return newEvent;
    }

    /////////////////////////////////////////////////////////////////
}
