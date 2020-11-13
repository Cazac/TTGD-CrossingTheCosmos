using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RARC_RequirementsUITab : MonoBehaviour
{
    ////////////////////////////////

    [Header("Button")]
    public Button confirm_Button;

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

    [Header("Resource Set 4")]
    public GameObject resourceSet4_Container;
    public TextMeshProUGUI resourceSet4_Count;
    public Image resourceSet4_Image;

    [Header("Resource Set 5")]
    public GameObject resourceSet5_Container;
    public TextMeshProUGUI resourceSet5_Count;
    public Image resourceSet5_Image;

    /////////////////////////////////////////////////////////////////

    public void SetupTab(List<RARC_Resource> resources_List, string colorStringWrong, string colorStringAvalible)
    {
        confirm_Button.interactable = true;

        //Resource Set 1
        if (resources_List[0].resourceType != RARC_Resource.ResourceType.NULL)
        {
            resourceSet1_Container.SetActive(true);
            resourceSet1_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resources_List[0].resourceType);

            if (RARC_GameStateController.Instance.CheckForResources(resources_List[0].resourceType, resources_List[0].resourceCount))
            {
                resourceSet1_Count.text = "<" + colorStringAvalible + ">" + "x" + resources_List[0].resourceCount + "</color>";
            }
            else
            {
                resourceSet1_Count.text = "<" + colorStringWrong + ">" + "x" + resources_List[0].resourceCount + "</color>";
                confirm_Button.interactable = false;
            }
        }
        else
        {
            resourceSet1_Container.SetActive(false);
        }

        //Resource Set 2
        if (resources_List[1].resourceType != RARC_Resource.ResourceType.NULL)
        {
            resourceSet2_Container.SetActive(true);
            resourceSet2_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resources_List[1].resourceType);

            if (RARC_GameStateController.Instance.CheckForResources(resources_List[1].resourceType, resources_List[1].resourceCount))
            {
                resourceSet2_Count.text = "<" + colorStringAvalible + ">" + "x" + resources_List[1].resourceCount + "</color>";
            }
            else
            {
                resourceSet2_Count.text = "<" + colorStringWrong + ">" + "x" + resources_List[1].resourceCount + "</color>";
                confirm_Button.interactable = false;
            }
        }
        else
        {
            resourceSet2_Container.SetActive(false);
        }

        //Resource Set 3
        if (resources_List[2].resourceType != RARC_Resource.ResourceType.NULL)
        {
            resourceSet3_Container.SetActive(true);
            resourceSet3_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resources_List[2].resourceType);

            if (RARC_GameStateController.Instance.CheckForResources(resources_List[2].resourceType, resources_List[2].resourceCount))
            {
                resourceSet3_Count.text = "<" + colorStringAvalible + ">" + "x" + resources_List[2].resourceCount + "</color>";
            }
            else
            {
                resourceSet3_Count.text = "<" + colorStringWrong + ">" + "x" + resources_List[2].resourceCount + "</color>";
                confirm_Button.interactable = false;
            }
        }
        else
        {
            resourceSet3_Container.SetActive(false);
        }


        if (resourceSet4_Container != null)
        {
            if (resources_List[3].resourceType != RARC_Resource.ResourceType.NULL)
            {
                resourceSet4_Container.SetActive(true);
                resourceSet4_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resources_List[3].resourceType);

                if (RARC_GameStateController.Instance.CheckForResources(resources_List[3].resourceType, resources_List[3].resourceCount))
                {
                    resourceSet4_Count.text = "<" + colorStringAvalible + ">" + "x" + resources_List[3].resourceCount + "</color>";
                }
                else
                {
                    resourceSet4_Count.text = "<" + colorStringWrong + ">" + "x" + resources_List[3].resourceCount + "</color>";
                    confirm_Button.interactable = false;
                }
            }
            else
            {
                resourceSet4_Container.SetActive(false);
            }
        }

        if (resourceSet5_Container != null)
        {
            if (resources_List[4].resourceType != RARC_Resource.ResourceType.NULL)
            {
                resourceSet5_Container.SetActive(true);
                resourceSet5_Image.sprite = RARC_DatabaseController.Instance.resources_DB.GetIcon(resources_List[4].resourceType);

                if (RARC_GameStateController.Instance.CheckForResources(resources_List[4].resourceType, resources_List[4].resourceCount))
                {
                    resourceSet5_Count.text = "<" + colorStringAvalible + ">" + "x" + resources_List[4].resourceCount + "</color>";
                }
                else
                {
                    resourceSet5_Count.text = "<" + colorStringWrong + ">" + "x" + resources_List[4].resourceCount + "</color>";
                    confirm_Button.interactable = false;
                }
            }
            else
            {
                resourceSet5_Container.SetActive(false);
            }
        }
    }

    public void RemoveRequirementsFromPlayer(List<RARC_Resource> resources_List)
    {
        //Resource Set 1
        if (resources_List[0].resourceType != RARC_Resource.ResourceType.NULL)
        {
            RARC_GameStateController.Instance.ChangeResources(resources_List[0].resourceName, resources_List[0].resourceType, resources_List[0].resourceCount * -1);
        }

        //Resource Set 2
        if (resources_List[1].resourceType != RARC_Resource.ResourceType.NULL)
        {
            RARC_GameStateController.Instance.ChangeResources(resources_List[1].resourceName, resources_List[1].resourceType, resources_List[1].resourceCount * -1);
        }

        //Resource Set 3
        if (resources_List[2].resourceType != RARC_Resource.ResourceType.NULL)
        {
            RARC_GameStateController.Instance.ChangeResources(resources_List[2].resourceName, resources_List[2].resourceType, resources_List[2].resourceCount * -1);
        }

        //Resource Set 4
        if (resourceSet4_Container != null)
        {
            if (resources_List[3].resourceType != RARC_Resource.ResourceType.NULL)
            {
                RARC_GameStateController.Instance.ChangeResources(resources_List[3].resourceName, resources_List[3].resourceType, resources_List[3].resourceCount * -1);
            }
        }

        //Resource Set 5
        if (resourceSet5_Container != null)
        {
            if (resources_List[4].resourceType != RARC_Resource.ResourceType.NULL)
            {
                RARC_GameStateController.Instance.ChangeResources(resources_List[4].resourceName, resources_List[4].resourceType, resources_List[4].resourceCount * -1);
            }
        }
    }

    /////////////////////////////////////////////////////////////////
}
