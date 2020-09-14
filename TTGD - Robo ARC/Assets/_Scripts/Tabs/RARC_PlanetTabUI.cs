using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RARC_PlanetTabUI : MonoBehaviour
{
    ////////////////////////////////

    [Header("Planet")]
    public Image planetSprite_Main;
    public Image planetSprite_Secondary;

    [Header("Moon 1")]
    public GameObject moonContainer1_GO;
    public Image moonSprite1_Main;
    public Image moonSprite1_Secondary;

    [Header("Moon 2")]
    public GameObject moonContainer2_GO;
    public Image moonSprite2_Main;
    public Image moonSprite2_Secondary;

    [Header("Resources")]
    public TextMeshProUGUI name_Text;
    public TextMeshProUGUI travelTime_Text;
    public TextMeshProUGUI resources1_Text;
    public TextMeshProUGUI resources2_Text;
    public TextMeshProUGUI resources3_Text;

    /////////////////////////////////////////////////////////////////

    public void SetPlanet(RARC_Planet planet)
    {
        //Planet
        planetSprite_Main.transform.rotation = Quaternion.Euler(0, 0, planet.planetRotation);
        planetSprite_Secondary.transform.rotation = Quaternion.Euler(0, 0, planet.planetRotation);

        //Sprite
        planetSprite_Main.sprite = GetPlanetSprite_Main(planet.planetSprite_Main, planet.planetType);
        planetSprite_Secondary.sprite = GetPlanetSprite_Secondary(planet.planetSprite_Secondary, planet.planetType);

        //Color
        planetSprite_Main.color = GetPlanetColor(planet.primaryColor);
        planetSprite_Secondary.color = GetPlanetColor(planet.secondaryColor);

        //Name
        if (name_Text != null)
        {
            name_Text.text = planet.planetName;
        }

        //Resources
        if (travelTime_Text != null)
        {
            travelTime_Text.text = "Travel Time " + planet.planetTravelTime + " Weeks.";

            resources1_Text.text = "- Iron (0% - 20%)";
            resources2_Text.text = "- Silicone (50% - 100%)";
            resources3_Text.text = "- Gems (0% - 20%)";
        }

        //Moon
        if (moonContainer1_GO != null)
        {
            if (planet.planetMoonPlanet_List.Count >= 1 && moonContainer1_GO != null)
            {
                RARC_Planet moon1 = planet.planetMoonPlanet_List[0];

                moonContainer1_GO.SetActive(true);

                moonSprite1_Main.transform.rotation = Quaternion.Euler(0, 0, moon1.planetRotation);
                moonSprite1_Secondary.transform.rotation = Quaternion.Euler(0, 0, moon1.planetRotation);

                moonSprite1_Main.sprite = GetPlanetSprite_Main(moon1.planetSprite_Main, moon1.planetType);
                moonSprite1_Secondary.sprite = GetPlanetSprite_Secondary(moon1.planetSprite_Secondary, moon1.planetType);

                moonSprite1_Main.color = GetPlanetColor(moon1.primaryColor);
                moonSprite1_Secondary.color = GetPlanetColor(moon1.secondaryColor);
            }
            else
            {
                moonContainer1_GO.SetActive(false);
            }
        }

        //Moon
        if (moonContainer2_GO != null)
        {
            if (planet.planetMoonPlanet_List.Count >= 2 && moonContainer2_GO != null)
            {
                RARC_Planet moon2 = planet.planetMoonPlanet_List[1];

                moonContainer2_GO.SetActive(true);

                moonSprite2_Main.transform.rotation = Quaternion.Euler(0, 0, moon2.planetRotation);
                moonSprite2_Secondary.transform.rotation = Quaternion.Euler(0, 0, moon2.planetRotation);

                moonSprite2_Main.sprite = GetPlanetSprite_Main(moon2.planetSprite_Main, moon2.planetType);
                moonSprite2_Secondary.sprite = GetPlanetSprite_Secondary(moon2.planetSprite_Secondary, moon2.planetType);

                moonSprite2_Main.color = GetPlanetColor(moon2.primaryColor);
                moonSprite2_Secondary.color = GetPlanetColor(moon2.secondaryColor);
            }
            else
            {
                moonContainer2_GO.SetActive(false);
            }
        }
    }

    public void ClearPlanet()
    {
        planetSprite_Main.sprite = null;
        planetSprite_Secondary.sprite = null;

        moonContainer1_GO.SetActive(false);
        moonContainer2_GO.SetActive(false);
    }

    /////////////////////////////////////////////////////////////////

    private Sprite GetPlanetSprite_Main(int spriteNo, RARC_Planet.PlanetType type)
    {
        Sprite planetSprite = null;

        switch (type)
        {
            case RARC_Planet.PlanetType.Moon:
                planetSprite = RARC_DatabaseController.Instance.planet_SO.planetSpritesMain_Moon[spriteNo];
                break;

            case RARC_Planet.PlanetType.Rocky:
                planetSprite = RARC_DatabaseController.Instance.planet_SO.planetSpritesMain_Rocky[spriteNo];
                break;

            case RARC_Planet.PlanetType.Gassy:
                break;

            default:
                break;
        }

        return planetSprite;
    }

    private Sprite GetPlanetSprite_Secondary(int spriteNo, RARC_Planet.PlanetType type)
    {
        Sprite planetSprite = null;

        switch (type)
        {
            case RARC_Planet.PlanetType.Moon:
                planetSprite = RARC_DatabaseController.Instance.planet_SO.planetSpritesSecondary_Moon[spriteNo];
                break;

            case RARC_Planet.PlanetType.Rocky:
                planetSprite = RARC_DatabaseController.Instance.planet_SO.planetSpritesSecondary_Rocky[spriteNo];
                break;

            case RARC_Planet.PlanetType.Gassy:
                break;

            default:
                break;
        }

        return planetSprite;
    }

    private Color GetPlanetColor(string planetStringColor)
    {
        //Parse String Color
        Color planetColor;
        if (!ColorUtility.TryParseHtmlString("#" + planetStringColor, out planetColor))
        {
            planetColor = Color.white;
        }

        return planetColor;
    }

    /////////////////////////////////////////////////////////////////
}
