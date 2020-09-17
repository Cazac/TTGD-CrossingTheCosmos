using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RARC_ResourceTab : MonoBehaviour
{

    public Image icon_Image;
    public TextMeshProUGUI name_Text;

    [HideInInspector]
    public Tuple<int, int, RARC_Resource> currentResource;


    /////////////////////////////////////////////////////////////////

    public void SetResource_Storage(RARC_Resource resource)
    {
        //currentResource = resource;

        name_Text.text = resource.resourceName + " x" + resource.resourceCount;
        icon_Image.sprite = GetIcon(resource.resourceType);
    }

    public void SetResource_Navigating(Tuple<int, int, RARC_Resource> resourceTup)
    {
        currentResource = resourceTup;

        name_Text.text = currentResource.Item3.resourceName + " (" + resourceTup.Item1 + "%)";
        icon_Image.sprite = GetIcon(resourceTup.Item3.resourceType);
    }

    public void SetResource_Gathering(Tuple<int, int, RARC_Resource> resourceTup, float effectivenessValue)
    {
        float effectivenessRate = (effectivenessValue - 1);
        int bonusValue = (int)(resourceTup.Item2 * effectivenessRate);

        currentResource = resourceTup;

        if (resourceTup.Item2 == 0)
        {
            name_Text.text = currentResource.Item3.resourceName + ": NONE";
        }
        else
        {
            int value = resourceTup.Item2 + bonusValue;
            name_Text.text = currentResource.Item3.resourceName + ": x" + value;

            /*
            if (bonusValue > 0)
            {
                name_Text.text = currentResource.Item3.resourceName + ": " + value + " (+" + bonusValue + ")";
            }
            else if (bonusValue < 0)
            {
                name_Text.text = currentResource.Item3.resourceName + ": " + value + " (" + bonusValue + ")";
            }
            else
            {
                name_Text.text = currentResource.Item3.resourceName + ": " + value + " (" + bonusValue + ")";
            }
            */

        }

        icon_Image.sprite = GetIcon(resourceTup.Item3.resourceType);
    }

    private Sprite GetIcon(RARC_Resource.ResourceType resourceType)
    {
        Sprite sprite = null;

        switch (resourceType)
        {
            case RARC_Resource.ResourceType.Scrap:
                sprite = RARC_DatabaseController.Instance.icons_DB.ScrapIcon;
                break;

            case RARC_Resource.ResourceType.Fuel:
                sprite = RARC_DatabaseController.Instance.icons_DB.FuelIcon;
                break;

            case RARC_Resource.ResourceType.Food:
                sprite = RARC_DatabaseController.Instance.icons_DB.FoodIcon;
                break;

            case RARC_Resource.ResourceType.Medkit:
                sprite = RARC_DatabaseController.Instance.icons_DB.MedkitIcon;
                break;

            case RARC_Resource.ResourceType.Repairkit:
                sprite = RARC_DatabaseController.Instance.icons_DB.RepairkitIcon;
                break;

            case RARC_Resource.ResourceType.Copper:
                sprite = RARC_DatabaseController.Instance.icons_DB.CopperIcon;
                break;

            case RARC_Resource.ResourceType.Platinum:
                sprite = RARC_DatabaseController.Instance.icons_DB.PlatinumIcon;
                break;

            case RARC_Resource.ResourceType.Silicon:
                sprite = RARC_DatabaseController.Instance.icons_DB.SiliconIcon;
                break;

            case RARC_Resource.ResourceType.Carbon:
                sprite = RARC_DatabaseController.Instance.icons_DB.CarbonIcon;
                break;

            case RARC_Resource.ResourceType.Sulfur:
                sprite = RARC_DatabaseController.Instance.icons_DB.SulfurIcon;
                break;


            case RARC_Resource.ResourceType.Hydrogen:
                sprite = RARC_DatabaseController.Instance.icons_DB.HydrogenIcon;
                break;

            case RARC_Resource.ResourceType.Nitrogen:
                sprite = RARC_DatabaseController.Instance.icons_DB.NitrogenIcon;
                break;

            case RARC_Resource.ResourceType.Helium:
                sprite = RARC_DatabaseController.Instance.icons_DB.HeliumIcon;
                break;

            default:
                sprite = RARC_DatabaseController.Instance.icons_DB.ScrapIcon;
                break;
        }

        return sprite;
    }
    /////////////////////////////////////////////////////////////////
}
