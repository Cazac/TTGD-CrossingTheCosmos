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

        if ()
        {
            RARC_Resource newResource = new RARC_Resource(0, craftingSO.resourceType);
            name_Text.text = newResource.resourceName;
            craftingIcon.sprite = craftingSO.craftingSprite;
        }
        else if (craftingSO.crewPerCraft != 0)
        {

        }
        else if (craftingSO.botsPerCraft != 0)
        {

        }

        RARC_Resource newResource = new RARC_Resource(0, craftingSO.resourceType);
        name_Text.text = newResource.resourceName;
        craftingIcon.sprite = craftingSO.craftingSprite;


        //Text
        craftingRoomsCurrent_Text.text = craftingSO.roomRequired.ToString() + " Rooms: ???";


        //Setup Tab
        List<RARC_Resource> resourcesRequired_List = GetResourcesList();
        requirementsTab.SetupTab(resourcesRequired_List, RARC_ButtonController_Game.Instance.colorValues_Red, RARC_ButtonController_Game.Instance.colorValues_White);
    }


    public void Button_CraftResource()
    {

        print("Test Code: Craft!");

        // foreach

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

    /////////////////////////////////////////////////////////////////
}