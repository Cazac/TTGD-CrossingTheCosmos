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
        if (resources_List.Count >= 1)
        {
            resourceSet1_Container.SetActive(true);

        }
        else
        {
            resourceSet1_Container.SetActive(false);
        }


        //Resource Set 2
        if (resources_List.Count >= 2)
        {
            resourceSet2_Container.SetActive(true);

        }
        else
        {
            resourceSet2_Container.SetActive(false);
        }

        //Resource Set 3
        if (resources_List.Count >= 3)
        {
            resourceSet3_Container.SetActive(true);

        }
        else
        {
            resourceSet3_Container.SetActive(false);
        }
    }

    /////////////////////////////////////////////////////////////////
}
