using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RARC_CraftingUITab : MonoBehaviour
{
    ////////////////////////////////

    //[HideInInspector]
    //public RARC_Rooms_SO roomSO;

    [Header("Tab Info")]
    public Image craftingIcon;
    public TextMeshProUGUI name_Text;
    public TextMeshProUGUI craftingResourceCurrent_Text;
    public TextMeshProUGUI craftingRoomsCurrent_Text;
    public TextMeshProUGUI craftingRemainingCurrent_Text;
    public RARC_RequirementsUITab requirementsTab;


    [Header("Held Info")]
    public List<RARC_Resource> resourcesRequired_List;
    public RARC_Resource.ResourceType resourceGiven_Type;
    public int resourceGiven_Count;

    /////////////////////////////////////////////////////////////////

    public void SetupTab(RARC_Crafting_SO newCraftingSO)
    {
        //Setup Info
        RARC_Resource newResource = new RARC_Resource(0, newCraftingSO.resourceType);

        name_Text.text = newResource.resourceName;
        craftingIcon.sprite = newCraftingSO.craftingSprite;

        //Setup List
        resourcesRequired_List = new List<RARC_Resource>();

        craftingRoomsCurrent_Text.text = newCraftingSO.roomRequired.ToString() + " Rooms: ???";

        //Capacity Remaining: (0)

        //Gather Resources
        RARC_Resource newResource1 = new RARC_Resource(newCraftingSO.resourceRequiredAmount_1, newCraftingSO.resourceRequired_1);
        resourcesRequired_List.Add(newResource1);
        RARC_Resource newResource2 = new RARC_Resource(newCraftingSO.resourceRequiredAmount_2, newCraftingSO.resourceRequired_2);
        resourcesRequired_List.Add(newResource2);
        RARC_Resource newResource3 = new RARC_Resource(newCraftingSO.resourceRequiredAmount_3, newCraftingSO.resourceRequired_3);
        resourcesRequired_List.Add(newResource3);
        RARC_Resource newResource4 = new RARC_Resource(newCraftingSO.resourceRequiredAmount_4, newCraftingSO.resourceRequired_4);
        resourcesRequired_List.Add(newResource4);
        RARC_Resource newResource5 = new RARC_Resource(newCraftingSO.resourceRequiredAmount_5, newCraftingSO.resourceRequired_5);
        resourcesRequired_List.Add(newResource5);

        //Setup Tab
        requirementsTab.SetupTab(resourcesRequired_List, RARC_ButtonController_Game.Instance.colorValues_Red, RARC_ButtonController_Game.Instance.colorValues_White);
    }

    public void Button_CraftResource()
    {

        print("Test Code: Craft!");

       // foreach

    }

    /////////////////////////////////////////////////////////////////
}