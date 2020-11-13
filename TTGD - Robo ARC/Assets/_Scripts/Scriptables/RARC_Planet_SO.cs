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
    [Header("-----------------------------------------------------------------")]

    ////////////////////////////////

    public List<Sprite> planetSpritesMain_Rocky;
    public List<Sprite> planetSpritesSecondary_Rocky;
    public List<Color> colorPaletteMain_Rocky;
    public List<Color> colorPaletteSecondary_Rocky;

    [Header("Rocky Planets - Basic Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType BasicResource_Rocky;
    public int minAmountOf_BasicResource_Rocky;
    public int maxAmountOf_BasicResource_Rocky;

    [Header("Rocky Planets - Common Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType CommonResource_Rocky;
    [Range(0, 100)]
    public int chanceOf_CommonResource_Rocky;
    public int minAmountOf_CommonResource_Rocky;
    public int maxAmountOf_CommonResource_Rocky;

    [Header("Rocky Planets - Rare Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType RareResource_Rocky;
    [Range(0, 100)]
    public int chanceOf_RareResource_Rocky;
    public int minAmountOf_RareResource_Rocky;
    public int maxAmountOf_RareResource_Rocky;

    ////////////////////////////////

    [Header("Icy Planets - Sprites")]
    [Header("-----------------------------------------------------------------")]

    ////////////////////////////////

    public List<Sprite> planetSpritesMain_Lava;
    public List<Sprite> planetSpritesSecondary_Lava;
    public List<Color> colorPaletteMain_Lava;
    public List<Color> colorPaletteSecondary_Lava;

    [Header("Lava Planets - Basic Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType BasicResource_Lava;
    public int minAmountOf_BasicResource_Lava;
    public int maxAmountOf_BasicResource_Lava;

    [Header("Lava Planets - Common Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType CommonResource_Lava;
    [Range(0, 100)]
    public int chanceOf_CommonResource_Lava;
    public int minAmountOf_CommonResource_Lava;
    public int maxAmountOf_CommonResource_Lava;

    [Header("Lava Planets - Rare Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType RareResource_Lava;
    [Range(0, 100)]
    public int chanceOf_RareResource_Lava;
    public int minAmountOf_RareResource_Lava;
    public int maxAmountOf_RareResource_Lava;

    ////////////////////////////////

    [Header("Icy Planets - Sprites")]
    [Header("-----------------------------------------------------------------")]

    ////////////////////////////////

    public List<Sprite> planetSpritesMain_Icy;
    public List<Sprite> planetSpritesSecondary_Icy;
    public List<Color> colorPaletteMain_Icy;
    public List<Color> colorPaletteSecondary_Icy;

    [Header("Icy Planets - Basic Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType BasicResource_Icy;
    public int minAmountOf_BasicResource_Icy;
    public int maxAmountOf_BasicResource_Icy;

    [Header("Icy Planets - Common Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType CommonResource_Icy;
    [Range(0, 100)]
    public int chanceOf_CommonResource_Icy;
    public int minAmountOf_CommonResource_Icy;
    public int maxAmountOf_CommonResource_Icy;

    [Header("Icy Planets - Rare Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType RareResource_Icy;
    [Range(0, 100)]
    public int chanceOf_RareResource_Icy;
    public int minAmountOf_RareResource_Icy;
    public int maxAmountOf_RareResource_Icy;

    ////////////////////////////////

    [Header("Gassy Planets - Sprites")]
    [Header("-----------------------------------------------------------------")]

    ////////////////////////////////

    public List<Sprite> planetSpritesMain_Gassy;
    public List<Sprite> planetSpritesSecondary_Gassy;
    public List<Color> colorPaletteMain_Gassy;
    public List<Color> colorPaletteSecondary_Gassy;

    [Header("Gassy Planets - Basic Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType BasicResource_Gassy;
    public int minAmountOf_BasicResource_Gassy;
    public int maxAmountOf_BasicResource_Gassy;

    [Header("Gassy Planets - Common Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType CommonResource_Gassy;
    [Range(0, 100)]
    public int chanceOf_CommonResource_Gassy;
    public int minAmountOf_CommonResource_Gassy;
    public int maxAmountOf_CommonResource_Gassy;

    [Header("Gassy Planets - Rare Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType RareResource_Gassy;
    [Range(0, 100)]
    public int chanceOf_RareResource_Gassy;
    public int minAmountOf_RareResource_Gassy;
    public int maxAmountOf_RareResource_Gassy;

    ////////////////////////////////

    [Header("Living Planets - Sprites")]
    [Header("-----------------------------------------------------------------")]

    ////////////////////////////////

    public List<Sprite> planetSpritesMain_Living;
    public List<Sprite> planetSpritesSecondary_Living;
    public List<Color> colorPaletteMain_Living;
    public List<Color> colorPaletteSecondary_Living;

    [Header("Living Planets - Basic Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType BasicResource_Living;
    public int minAmountOf_BasicResource_Living;
    public int maxAmountOf_BasicResource_Living;

    [Header("Living Planets - Common Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType CommonResource_Living;
    [Range(0, 100)]
    public int chanceOf_CommonResource_Living;
    public int minAmountOf_CommonResource_Living;
    public int maxAmountOf_CommonResource_Living;

    [Header("Living Planets - Rare Resources (NULL ALLOWED)")]
    public RARC_Resource.ResourceType RareResource_Living;
    [Range(0, 100)]
    public int chanceOf_RareResource_Living;
    public int minAmountOf_RareResource_Living;
    public int maxAmountOf_RareResource_Living;

    ////////////////////////////////

    [Header("Moons")]
    [Header("-----------------------------------------------------------------")]

    ////////////////////////////////

    public List<Sprite> planetSpritesMain_Moon;
    public List<Sprite> planetSpritesSecondary_Moon;
    public List<Color> colorPaletteMain_Moon;
    public List<Color> colorPaletteSecondary_Moon;

    /////////////////////////////////////////////////////////////////

    public RARC_Planet GenerateAnyPlanet()
    {
        RARC_Planet newPlanet = null;
        int planetTypeChoice = Random.Range(0, 5);

        switch (planetTypeChoice)
        {
            case 0:
                newPlanet = GeneratePlanet_Rocky();
                break;

            case 1:
                newPlanet = GeneratePlanet_Lava();
                break;

            case 2:
                newPlanet = GeneratePlanet_Icy();
                break;

            case 3:
                newPlanet = GeneratePlanet_Gassy();
                break;

            case 4:
                newPlanet = GeneratePlanet_Living();
                break;
                
            default:
                newPlanet = GeneratePlanet_Rocky();
                break;
        }

        return newPlanet;
    }

    /////////////////////////////////////////////////////////////////

    public RARC_Planet GeneratePlanet_Rocky()
    {
        //Create New Planet
        RARC_Planet newPlanet = new RARC_Planet();
        newPlanet.planetType = RARC_Planet.PlanetType.Rocky;

        //Sprite / Colors
        newPlanet.planetSprite_Main = Random.Range(0, planetSpritesMain_Rocky.Count);
        newPlanet.planetSprite_Secondary = Random.Range(0, planetSpritesSecondary_Rocky.Count);
        newPlanet.primaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteMain_Rocky[Random.Range(0, colorPaletteMain_Rocky.Count)]);
        newPlanet.secondaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteSecondary_Rocky[Random.Range(0, colorPaletteSecondary_Rocky.Count)]);

        //Resources
        newPlanet.planetResources_List = GenerateResources(BasicResource_Rocky, minAmountOf_BasicResource_Rocky, maxAmountOf_BasicResource_Rocky, CommonResource_Rocky, chanceOf_CommonResource_Rocky, minAmountOf_CommonResource_Rocky, maxAmountOf_CommonResource_Rocky, RareResource_Rocky, chanceOf_RareResource_Rocky, minAmountOf_RareResource_Rocky, maxAmountOf_RareResource_Rocky);

        //Generic Info For All Planets
        newPlanet = GetGenericInfo(newPlanet);

        //Return New Planet
        return newPlanet;
    }

    public RARC_Planet GeneratePlanet_Lava()
    {
        //Create New Planet
        RARC_Planet newPlanet = new RARC_Planet();
        newPlanet.planetType = RARC_Planet.PlanetType.Lava;

        //Sprite / Colors
        newPlanet.planetSprite_Main = Random.Range(0, planetSpritesMain_Lava.Count);
        newPlanet.planetSprite_Secondary = Random.Range(0, planetSpritesSecondary_Lava.Count);
        newPlanet.primaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteMain_Lava[Random.Range(0, colorPaletteMain_Lava.Count)]);
        newPlanet.secondaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteSecondary_Lava[Random.Range(0, colorPaletteSecondary_Lava.Count)]);

        //Resources
        newPlanet.planetResources_List = GenerateResources(BasicResource_Lava, minAmountOf_BasicResource_Lava, maxAmountOf_BasicResource_Lava, CommonResource_Lava, chanceOf_CommonResource_Lava, minAmountOf_CommonResource_Lava, maxAmountOf_CommonResource_Lava, RareResource_Lava, chanceOf_RareResource_Lava, minAmountOf_RareResource_Lava, maxAmountOf_RareResource_Lava);

        //Generic Info For All Planets
        newPlanet = GetGenericInfo(newPlanet);

        //Return New Planet
        return newPlanet;
    }

    public RARC_Planet GeneratePlanet_Icy()
    {
        //Create New Planet
        RARC_Planet newPlanet = new RARC_Planet();
        newPlanet.planetType = RARC_Planet.PlanetType.Icy;

        //Sprite / Colors
        newPlanet.planetSprite_Main = Random.Range(0, planetSpritesMain_Icy.Count);
        newPlanet.planetSprite_Secondary = Random.Range(0, planetSpritesSecondary_Icy.Count);
        newPlanet.primaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteMain_Icy[Random.Range(0, colorPaletteMain_Icy.Count)]);
        newPlanet.secondaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteSecondary_Icy[Random.Range(0, colorPaletteSecondary_Icy.Count)]);

        //Resources
        newPlanet.planetResources_List = GenerateResources(BasicResource_Icy, minAmountOf_BasicResource_Icy, maxAmountOf_BasicResource_Icy, CommonResource_Icy, chanceOf_CommonResource_Icy, minAmountOf_CommonResource_Icy, maxAmountOf_CommonResource_Icy, RareResource_Icy, chanceOf_RareResource_Icy, minAmountOf_RareResource_Icy, maxAmountOf_RareResource_Icy);

        //Generic Info For All Planets
        newPlanet = GetGenericInfo(newPlanet);

        //Return New Planet
        return newPlanet;
    }

    public RARC_Planet GeneratePlanet_Gassy()
    {
        //Create New Planet
        RARC_Planet newPlanet = new RARC_Planet();
        newPlanet.planetType = RARC_Planet.PlanetType.Gassy;

        //Sprite / Colors
        newPlanet.planetSprite_Main = Random.Range(0, planetSpritesMain_Gassy.Count);
        newPlanet.planetSprite_Secondary = Random.Range(0, planetSpritesSecondary_Gassy.Count);
        newPlanet.primaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteMain_Gassy[Random.Range(0, colorPaletteMain_Gassy.Count)]);
        newPlanet.secondaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteSecondary_Gassy[Random.Range(0, colorPaletteSecondary_Gassy.Count)]);

        //Resources
        newPlanet.planetResources_List = GenerateResources(BasicResource_Gassy, minAmountOf_BasicResource_Gassy, maxAmountOf_BasicResource_Gassy, CommonResource_Gassy, chanceOf_CommonResource_Gassy, minAmountOf_CommonResource_Gassy, maxAmountOf_CommonResource_Gassy, RareResource_Gassy, chanceOf_RareResource_Gassy, minAmountOf_RareResource_Gassy, maxAmountOf_RareResource_Gassy);

        //Generic Info For All Planets
        newPlanet = GetGenericInfo(newPlanet);

        //Return New Planet
        return newPlanet;
    }

    public RARC_Planet GeneratePlanet_Living()
    {
        //Create New Planet
        RARC_Planet newPlanet = new RARC_Planet();
        newPlanet.planetType = RARC_Planet.PlanetType.Living;

        //Sprite / Colors
        newPlanet.planetSprite_Main = Random.Range(0, planetSpritesMain_Living.Count);
        newPlanet.planetSprite_Secondary = Random.Range(0, planetSpritesSecondary_Living.Count);
        newPlanet.primaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteMain_Living[Random.Range(0, colorPaletteMain_Living.Count)]);
        newPlanet.secondaryColor = ColorUtility.ToHtmlStringRGB(colorPaletteSecondary_Living[Random.Range(0, colorPaletteSecondary_Living.Count)]);

        //Resources
        newPlanet.planetResources_List = GenerateResources(BasicResource_Living, minAmountOf_BasicResource_Living, maxAmountOf_BasicResource_Living, CommonResource_Living, chanceOf_CommonResource_Living, minAmountOf_CommonResource_Living, maxAmountOf_CommonResource_Living, RareResource_Living, chanceOf_RareResource_Living, minAmountOf_RareResource_Living, maxAmountOf_RareResource_Living);

        //Generic Info For All Planets
        newPlanet = GetGenericInfo(newPlanet);

        //Return New Planet
        return newPlanet;
    }

    /////////////////////////////////////////////////////////////////

    public RARC_Planet GetGenericInfo(RARC_Planet newPlanet)
    {
        //Name
        newPlanet.planetName = allPlanetNames_List[Random.Range(0, allPlanetNames_List.Count)];
      
        //Visual Rotation
        newPlanet.planetRotation = Random.Range(-90, 90);

        //Time to reach planet
        newPlanet.planetTravelTime = Random.Range(2, 5);

        //Risk
        newPlanet.planetRiskFactor = Random.Range(0, 10);

        //Get Moons
        newPlanet = GetMoons(newPlanet);

        //Get Events
        newPlanet.planetEvent = GenerateAvalibleEvent();

        return newPlanet;
    }

    public RARC_Planet GetMoons(RARC_Planet newPlanet)
    {
        //Moon Generation
        newPlanet.planetMoonPlanet_List = new List<RARC_Planet>();
        int planetMoonCount = Random.Range(0, 3);
        for (int i = 0; i < planetMoonCount; i++)
        {
            newPlanet.planetMoonPlanet_List.Add(GenerateMoon());
        }

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
        int amountOfScrap = Random.Range(minAmountOf_BasicResource_Rocky, maxAmountOf_BasicResource_Rocky);
        System.Tuple<int, int, RARC_Resource> scrap_Resource = new System.Tuple<int, int, RARC_Resource>(100, amountOfScrap, RARC_DatabaseController.Instance.resources_DB.GetResource(BasicResource_Rocky));
        resources_List.Add(scrap_Resource);

        //Common Resource 
        if (CommonResource_Rocky != RARC_Resource.ResourceType.NULL)
        {
            int chanceOfCommonResource = Random.Range(0, 100);
            if (chanceOfCommonResource <= chanceOf_CommonResource_Rocky)
            {
                //Get Carbon
                int possiblityOfCommonResource = Chance_List[Random.Range(0, Chance_List.Count)];
                int amountOfCommonResource = Random.Range(minAmountOf_CommonResource_Rocky, maxAmountOf_CommonResource_Rocky);
                System.Tuple<int, int, RARC_Resource> Common_Resource = new System.Tuple<int, int, RARC_Resource>(possiblityOfCommonResource, amountOfCommonResource, RARC_DatabaseController.Instance.resources_DB.GetResource(CommonResource_Rocky));
                resources_List.Add(Common_Resource);
            }
        }

        //Rare Resource 
        if (RareResource_Rocky != RARC_Resource.ResourceType.NULL)
        {
            int chanceOfRareResource = Random.Range(0, 100);
            if (chanceOfRareResource <= chanceOf_RareResource_Rocky)
            {
                //Get Titanium
                int possiblityOfRareResource = Chance_List[Random.Range(0, Chance_List.Count)];
                int amountOfRareResource = Random.Range(minAmountOf_RareResource_Rocky, maxAmountOf_RareResource_Rocky);
                System.Tuple<int, int, RARC_Resource> Rare_Resource = new System.Tuple<int, int, RARC_Resource>(possiblityOfRareResource, amountOfRareResource, RARC_DatabaseController.Instance.resources_DB.GetResource(RareResource_Rocky));
                resources_List.Add(Rare_Resource);
            }
        }

        //Return
        return resources_List;
    }

    public List<System.Tuple<int, int, RARC_Resource>> GenerateResources(RARC_Resource.ResourceType basicResouce, int minAmountOf_BasicResource, int maxAmountOf_BasicResource, RARC_Resource.ResourceType commonResource, int chanceOf_CommonResource, int minAmountOf_CommonResource, int maxAmountOf_CommonResource, RARC_Resource.ResourceType rareResouce, int chanceOf_RareResource, int minAmountOf_RareResource, int maxAmountOf_RareResource)
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

        //Basic
        int amountOfBasic = Random.Range(minAmountOf_BasicResource, maxAmountOf_BasicResource);
        System.Tuple<int, int, RARC_Resource> scrap_Resource = new System.Tuple<int, int, RARC_Resource>(100, amountOfBasic, RARC_DatabaseController.Instance.resources_DB.GetResource(basicResouce));
        resources_List.Add(scrap_Resource);

        //Common Resource 
        if (commonResource != RARC_Resource.ResourceType.NULL)
        {
            int chanceOfCommonResource = Random.Range(0, 100);
            if (chanceOfCommonResource <= chanceOf_CommonResource)
            {
                //Get Resource
                int possiblityOfCommonResource = Chance_List[Random.Range(0, Chance_List.Count)];
                int amountOfCommonResource = Random.Range(minAmountOf_CommonResource, maxAmountOf_CommonResource);
                System.Tuple<int, int, RARC_Resource> Common_Resource = new System.Tuple<int, int, RARC_Resource>(possiblityOfCommonResource, amountOfCommonResource, RARC_DatabaseController.Instance.resources_DB.GetResource(CommonResource_Rocky));
                resources_List.Add(Common_Resource);
            }
        }

        //Rare Resource 
        if (rareResouce != RARC_Resource.ResourceType.NULL)
        {
            int chanceOfRareResource = Random.Range(0, 100);
            if (chanceOfRareResource <= chanceOf_RareResource)
            {
                //Get Resource
                int possiblityOfRareResource = Chance_List[Random.Range(0, Chance_List.Count)];
                int amountOfRareResource = Random.Range(minAmountOf_RareResource, maxAmountOf_RareResource);
                System.Tuple<int, int, RARC_Resource> Rare_Resource = new System.Tuple<int, int, RARC_Resource>(possiblityOfRareResource, amountOfRareResource, RARC_DatabaseController.Instance.resources_DB.GetResource(RareResource_Rocky));
                resources_List.Add(Rare_Resource);
            }
        }

        //Return
        return resources_List;
    }

    /////////////////////////////////////////////////////////////////

    public RARC_Event GenerateAvalibleEvent()
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
