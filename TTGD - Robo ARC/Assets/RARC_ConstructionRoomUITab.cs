using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RARC_ConstructionRoomUITab : MonoBehaviour
{
    ////////////////////////////////

    [HideInInspector]
    public RARC_Rooms_SO roomSO;

    [Header("Tab Info")]
    public Image roomIcon;
    public TextMeshProUGUI name_Text;
    public RARC_RequirementsUITab requirementsTab;

    /////////////////////////////////////////////////////////////////

    public void SetupTab(RARC_Rooms_SO newRoomSO)
    {
        name_Text.text = newRoomSO.roomName;

        //roomIcon


        List<RARC_Resource> newResource_List = new List<RARC_Resource>();

        RARC_Resource newResource1 = new RARC_Resource(newRoomSO.resourceRequiredAmount_1, newRoomSO.resourceRequired_1);
        newResource_List.Add(newResource1);
        RARC_Resource newResource2 = new RARC_Resource(newRoomSO.resourceRequiredAmount_2, newRoomSO.resourceRequired_2);
        newResource_List.Add(newResource2);
        RARC_Resource newResource3 = new RARC_Resource(newRoomSO.resourceRequiredAmount_3, newRoomSO.resourceRequired_3);
        newResource_List.Add(newResource3);






        requirementsTab.SetupTab(newResource_List);
    }

    public void Button_ContructRoom()
    {
        RARC_ButtonController_Game.Instance.Button_Construction_BuildRoom(this);
    }

    /////////////////////////////////////////////////////////////////
}
