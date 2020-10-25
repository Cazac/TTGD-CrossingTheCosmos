﻿using System.Collections;
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

        name_Text.text = resource.resourceName + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " x" + resource.resourceCount + "</color>"; 
        icon_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resource.resourceType);
    }

    public void SetResource_Navigating(Tuple<int, int, RARC_Resource> resourceTup)
    {
        currentResource = resourceTup;

        name_Text.text = currentResource.Item3.resourceName + " (" + resourceTup.Item1 + "%)";
        icon_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resourceTup.Item3.resourceType);
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
            name_Text.text = currentResource.Item3.resourceName + " +" + value;

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

        icon_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resourceTup.Item3.resourceType);
    }

    public void SetResource_Collecting(Tuple<int, int, RARC_Resource> resourceTup, float effectivenessValue)
    {
        float effectivenessRate = (effectivenessValue - 1);
        int bonusValue = (int)(resourceTup.Item2 * effectivenessRate);

        currentResource = resourceTup;

        if (resourceTup.Item2 == 0)
        {
            name_Text.text = currentResource.Item3.resourceName + " <" + RARC_ButtonController_Game.Instance.colorValues_Red + "> NONE";
        }
        else
        {
            int value = resourceTup.Item2 + bonusValue;
            name_Text.text = currentResource.Item3.resourceName + " <" + RARC_ButtonController_Game.Instance.colorValues_White + ">+" + value;

        }

        icon_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resourceTup.Item3.resourceType);
    }

    public void SetResource_OutcomeChanges(RARC_Resource resource)
    {
        if (resource.resourceCount >= 0)
        {
            name_Text.text = resource.resourceName + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + resource.resourceCount + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resource.resourceType);
        }
        else if (resource.resourceCount < 0)
        {
            name_Text.text = resource.resourceName + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " -" + resource.resourceCount + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resource.resourceType);
        }     
    }

    public void SetResource_OutcomeChanges_Hull(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Hull" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.icons_DB.ShipBaseIcon;
        }
        else if (value < 0)
        {
            name_Text.text = "Hull" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " -" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.icons_DB.ShipBaseIcon;
        }
    }

    public void SetResource_OutcomeChanges_Crew(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Crew" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.icons_DB.CrewIcon;
        }
        else if (value < 0)
        {
            name_Text.text = "Crew" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " -" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.icons_DB.CrewIcon;
        }
    }

    public void SetResource_OutcomeChanges_Bots(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Bots" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.icons_DB.BotsIcon;
        }
        else if (value < 0)
        {
            name_Text.text = "Bots" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " -" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.icons_DB.BotsIcon;
        }
    }

    /////////////////////////////////////////////////////////////////
}
