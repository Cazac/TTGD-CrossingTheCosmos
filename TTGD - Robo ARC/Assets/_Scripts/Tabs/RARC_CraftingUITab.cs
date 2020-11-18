using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RARC_CraftingUITab : MonoBehaviour
{
    ////////////////////////////////

    [HideInInspector]
    public RARC_Crafting_SO craftingSO;

    [Header("Tab Info")]
    public Image craftingIcon;
    public TextMeshProUGUI name_Text;
    public TextMeshProUGUI craftingResourceCurrent_Text;
    public TextMeshProUGUI craftingRoomsCurrent_Text;
    public TextMeshProUGUI craftingRemainingCurrent_Text;
    public RARC_RequirementsUITab requirementsTab;

    /////////////////////////////////////////////////////////////////

    public void SetupTab(RARC_Crafting_SO newCraftingSO)
    {
        //Setup Info
        craftingSO = newCraftingSO;

        if (newCraftingSO.resourceType != RARC_Resource.ResourceType.NULL)
        {
            name_Text.text = craftingSO.resourceType.ToString(); ;
        }
        else if (craftingSO.crewPerCraft != 0)
        {
            name_Text.text = "Crewmate";
        }
        else if (craftingSO.botsPerCraft != 0)
        {
            name_Text.text = "Robot";
        }


        //print("Test Code: " + "Fix Robots and Humans!!!");


        craftingIcon.sprite = craftingSO.craftingSprite;


        //Setup Tab
        List<RARC_Resource> resourcesRequired_List = GetResourcesList();
        requirementsTab.SetupTab(resourcesRequired_List, RARC_ButtonController_Game.Instance.colorValues_Red, RARC_ButtonController_Game.Instance.colorValues_White);

        //Text
        craftingResourceCurrent_Text.text = "Current: " + RARC_GameStateController.Instance.GetResoucesCount(craftingSO.resourceType) + "\n(+" + craftingSO.resourcePerCraft + ")";

        if (RARC_GameStateController.Instance.CountRoomsOnShip(craftingSO.roomRequired) <= 0)
        {
            gameObject.GetComponent<Button>().interactable = false;
            craftingRoomsCurrent_Text.text = craftingSO.roomRequired.ToString() + " Rooms: " + "<color=" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + RARC_GameStateController.Instance.CountRoomsOnShip(craftingSO.roomRequired) + "</color>";
        }
        else
        {
            craftingRoomsCurrent_Text.text = craftingSO.roomRequired.ToString() + " Rooms: " + "<color=" + RARC_ButtonController_Game.Instance.colorValues_White + ">" + RARC_GameStateController.Instance.CountRoomsOnShip(craftingSO.roomRequired) + "</color>";
        }

        if (GetAmountCraftable(craftingSO) <= 0)
        {
            gameObject.GetComponent<Button>().interactable = false;
            craftingRemainingCurrent_Text.text = "Capacity Remaining: " + "<color=" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + GetAmountCraftable(craftingSO).ToString() + "</color>";
        }
        else
        {
            craftingRemainingCurrent_Text.text = "Capacity Remaining: " + "<color=" + RARC_ButtonController_Game.Instance.colorValues_White + ">" + GetAmountCraftable(craftingSO).ToString() + "</color>";
        }
    }


    public void Button_CraftResource()
    {
        //Convert Item to get name
        RARC_Resource convertedResouce = RARC_DatabaseController.Instance.resources_DB.GetResource(craftingSO.resourceType);

        if (craftingSO.resourceType == RARC_Resource.ResourceType.NULL)
        {
            if (craftingSO.crewPerCraft != 0)
            {
                RARC_GameStateController.Instance.ChangeCrew(1);
            }
            else if (craftingSO.botsPerCraft != 0)
            {
                print("Test Code: FIX THIS WHEN CREATING BOTS");
                RARC_GameStateController.Instance.ChangeBots(1);
            }
        }
        else
        {
            //Add item
            RARC_GameStateController.Instance.ChangeResources(convertedResouce.resourceName, convertedResouce.resourceType, craftingSO.resourcePerCraft);
        }

        //Setup Tab / Remove items
        List<RARC_Resource> resourcesRequired_List = GetResourcesList();
        requirementsTab.RemoveRequirementsFromPlayer(resourcesRequired_List);

        //Lower Amount allowed per turn
        switch (convertedResouce.resourceType)
        {
            case RARC_Resource.ResourceType.Food:
                RARC_GameStateController.Instance.currentCraftingPerTurn_Food--;
                break;

            case RARC_Resource.ResourceType.Fuel:
                RARC_GameStateController.Instance.currentCraftingPerTurn_Fuel--;
                break;

            case RARC_Resource.ResourceType.Organics:
                RARC_GameStateController.Instance.currentCraftingPerTurn_Organics--;
                break;

            default:
                if (craftingSO.crewPerCraft != 0)
                {
                    RARC_GameStateController.Instance.currentCraftingPerTurn_Crew--;
                }
                else if (craftingSO.botsPerCraft != 0)
                {
                    RARC_GameStateController.Instance.currentCraftingPerTurn_Bots--;
                }
                break;
        }

        //Update Crafting Nodes
        RARC_ButtonController_Game.Instance.Button_Fabrication();
    }

    /////////////////////////////////////////////////////////////////

    public void RefreshRequirementsUI()
    {
        //Setup Tab and allow this button to be clicked or not based off the critrea given
        List<RARC_Resource> newResource_List = GetResourcesList();
        requirementsTab.SetupTab(newResource_List, RARC_ButtonController_Game.Instance.colorValues_Red, RARC_ButtonController_Game.Instance.colorValues_White);
    }

    private List<RARC_Resource> GetResourcesList()
    {
        //Setup List
        List<RARC_Resource> resourcesRequired_List = new List<RARC_Resource>();

        //Gather Resources
        RARC_Resource newResource1 = new RARC_Resource(craftingSO.resourceRequiredAmount_1, craftingSO.resourceRequired_1);
        resourcesRequired_List.Add(newResource1);
        RARC_Resource newResource2 = new RARC_Resource(craftingSO.resourceRequiredAmount_2, craftingSO.resourceRequired_2);
        resourcesRequired_List.Add(newResource2);
        RARC_Resource newResource3 = new RARC_Resource(craftingSO.resourceRequiredAmount_3, craftingSO.resourceRequired_3);
        resourcesRequired_List.Add(newResource3);
        RARC_Resource newResource4 = new RARC_Resource(craftingSO.resourceRequiredAmount_4, craftingSO.resourceRequired_4);
        resourcesRequired_List.Add(newResource4);
        RARC_Resource newResource5 = new RARC_Resource(craftingSO.resourceRequiredAmount_5, craftingSO.resourceRequired_5);
        resourcesRequired_List.Add(newResource5);

        return resourcesRequired_List;
    }

    private int GetAmountCraftable(RARC_Crafting_SO craftingSO)
    {
        int amount = 0;

        switch (craftingSO.resourceType)
        {
            case RARC_Resource.ResourceType.Food:
                amount = RARC_GameStateController.Instance.currentCraftingPerTurn_Food;
                break;

            case RARC_Resource.ResourceType.Fuel:
                amount = RARC_GameStateController.Instance.currentCraftingPerTurn_Fuel;
                break;

            case RARC_Resource.ResourceType.Organics:
                amount = RARC_GameStateController.Instance.currentCraftingPerTurn_Organics;
                break;

            default:
                if (craftingSO.crewPerCraft != 0)
                {
                    amount = RARC_GameStateController.Instance.currentCraftingPerTurn_Crew;
                }
                else if (craftingSO.botsPerCraft != 0)
                {
                    amount = RARC_GameStateController.Instance.currentCraftingPerTurn_Bots;
                }
                break;
        }

        return amount;


    }

    /////////////////////////////////////////////////////////////////
}