using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RARC_RequirementsUITab : MonoBehaviour
{
    ////////////////////////////////

    [Header("Resource Set 1")]
    public GameObject resourceSet1_Container;
    public TextMeshProUGUI resourceSet1_Count;
    public Image resourceSet1_Image;

    [Header("Resource Set 1")]
    public GameObject resourceSet2_Container;
    public TextMeshProUGUI resourceSet2_Count;
    public Image resourceSet2_Image;

    [Header("Resource Set 3")]
    public GameObject resourceSet3_Container;
    public TextMeshProUGUI resourceSet3_Count;
    public Image resourceSet3_Image;

    /////////////////////////////////////////////////////////////////

    public void SetupTab(List<RARC_Resource> resources_List)
    {
        //Resource Set 1
        if (resources_List[0].resourceType != RARC_Resource.ResourceType.NULL)
        {
            resourceSet1_Container.SetActive(true);
            resourceSet1_Count.text = "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + "x" + resources_List[0].resourceCount + "</color>";
            resourceSet1_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resources_List[0].resourceType); 
        }
        else
        {
            resourceSet1_Container.SetActive(false);
        }

        //Resource Set 2
        if (resources_List[1].resourceType != RARC_Resource.ResourceType.NULL)
        {
            resourceSet2_Container.SetActive(true);
            resourceSet2_Count.text = "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + "x" + resources_List[1].resourceCount + "</color>";
            resourceSet2_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resources_List[1].resourceType);
        }
        else
        {
            resourceSet2_Container.SetActive(false);
        }

        //Resource Set 3
        if (resources_List[2].resourceType != RARC_Resource.ResourceType.NULL)
        {
            resourceSet3_Container.SetActive(true);
            resourceSet3_Count.text = "<" + RARC_ButtonController_Game.Instance.colorValues_Yellow + ">" + "x" + resources_List[2].resourceCount + "</color>";
            resourceSet3_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resources_List[2].resourceType);
        }
        else
        {
            resourceSet3_Container.SetActive(false);
        }
    }

    /////////////////////////////////////////////////////////////////
}
