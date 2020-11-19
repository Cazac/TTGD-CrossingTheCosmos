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

    public GameObject floatingTextGreen_Prefab;
    public GameObject floatingTextRed_Prefab;


    [HideInInspector]
    public Tuple<int, int, RARC_Resource> currentResource;

    /////////////////////////////////////////////////////////////////

    public void SpawnChangesText(int valueChanged)
    {
        if (valueChanged >= 0)
        {
            GameObject newText = Instantiate(floatingTextGreen_Prefab, gameObject.transform);
            newText.GetComponent<TextMeshProUGUI>().text = "+" + valueChanged;
            StartCoroutine(DeleteText(newText));
        }
        else
        {
            GameObject newText = Instantiate(floatingTextRed_Prefab, gameObject.transform);
            newText.GetComponent<TextMeshProUGUI>().text = valueChanged.ToString();
            StartCoroutine(DeleteText(newText));
        }
    }

    public IEnumerator DeleteText(GameObject newText)
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(newText);

        yield return null;
    }

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
            name_Text.text = resource.resourceName + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + resource.resourceCount + "</color>";
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
            name_Text.text = "Crew" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
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
            name_Text.text = "Bots" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.icons_DB.BotsIcon;
        }
    }

    public void SetResource_OutcomeChanges_RoomEmpty(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Random Room" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.EmptyRoom_SO.activeRoomSprite;
        }
        else if (value < 0)
        {
            name_Text.text = "Random Room" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.EmptyRoom_SO.activeRoomSprite;
        }
    }

    public void SetResource_OutcomeChanges_ASTROMETRICS(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Astromtreics" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.AstrometricsLabRoom_SO.activeRoomSprite;
        }
        else if (value < 0)
        {
            name_Text.text = "Astromtreics" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.AstrometricsLabRoom_SO.activeRoomSprite;
        }
    }

    public void SetResource_OutcomeChanges_CLONING(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Cloning" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.CloningLabRoom_SO.activeRoomSprite;
        }
        else if (value < 0)
        {
            name_Text.text = "Cloning" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.CloningLabRoom_SO.activeRoomSprite;
        }
    }

    public void SetResource_OutcomeChanges_FACTORY(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Factory" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.FactoryRoom_SO.activeRoomSprite;
        }
        else if (value < 0)
        {
            name_Text.text = "Factory" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.FactoryRoom_SO.activeRoomSprite;
        }
    }

    public void SetResource_OutcomeChanges_HYDROPONICS(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Hydroponics" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.HydroponicsLabRoom_SO.activeRoomSprite;
        }
        else if (value < 0)
        {
            name_Text.text = "Hydroponics" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.HydroponicsLabRoom_SO.activeRoomSprite;
        }
    }

    public void SetResource_OutcomeChanges_KITCHEN(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Kitchen" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.KitchenRoom_SO.activeRoomSprite;
        }
        else if (value < 0)
        {
            name_Text.text = "Kitchen" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.KitchenRoom_SO.activeRoomSprite;
        }
    }

    public void SetResource_OutcomeChanges_MEDBAY(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Medbay" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.MedbayRoom_SO.activeRoomSprite;
        }
        else if (value < 0)
        {
            name_Text.text = "Medbay" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.MedbayRoom_SO.activeRoomSprite;
        }
    }

    public void SetResource_OutcomeChanges_QUARTERS(int value)
    {
        if (value >= 0)
        {
            name_Text.text = "Quarters" + "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + " +" + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.CrewQuartersRoom_SO.activeRoomSprite;
        }
        else if (value < 0)
        {
            name_Text.text = "Quarters" + "<" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + " " + value + "</color>";
            icon_Image.sprite = RARC_DatabaseController.Instance.room_DB.CrewQuartersRoom_SO.activeRoomSprite;
        }
    }

    /////////////////////////////////////////////////////////////////
}
