using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_SpaceTab : MonoBehaviour
{
    ////////////////////////////////

    [Header("Bottom Space Containers")]
    public GameObject spaceTypeBlack_Container;

    [Header("Top Space Containers")]
    public GameObject spaceTypeRed_Container;
    public GameObject spaceTypeGreen_Container;
    public GameObject spaceTypePurple_Container;
    public GameObject spaceTypeBlue_Container;

    [Header("BLANKVAR")]
    public Animator spaceTop_Animator;
    public Animator spaceBottom_Animator;

    [Header("BLANKVAR")]
    public RARC_PlanetTab planet;

    /////////////////////////////////////////////////////////////////

    public void PlayRandomSpace()
    {
        int randomNo = Random.Range(0,5);

        switch (randomNo)
        {
            case 0:
                SetSpace_Black();
                break;

            case 1:
                SetSpace_Red();
                break;

            case 2:
                SetSpace_Green();
                break;

            case 3:
                SetSpace_Purple();
                break;

            case 4:
                SetSpace_Blue();
                break;
        }
    }

    public void PlaySpecificSpace()
    {
        //int random = Random.Range(0,);
    }


    /////////////////////////////////////////////////////////////////

    public void SetSpace_Black()
    {
        TurnOffSpaceOverlay();
    }

    public void SetSpace_Red()
    {
        TurnOffSpaceOverlay();
        spaceTypeRed_Container.SetActive(true);
        spaceTop_Animator.Play("Space Fade in");
    }

    public void SetSpace_Green()
    {
        TurnOffSpaceOverlay();
        spaceTypeGreen_Container.SetActive(true);
        spaceTop_Animator.Play("Space Fade in");
    }

    public void SetSpace_Purple()
    {
        TurnOffSpaceOverlay();
        spaceTypePurple_Container.SetActive(true);
        spaceTop_Animator.Play("Space Fade in");
    }

    public void SetSpace_Blue()
    {
        TurnOffSpaceOverlay();
        spaceTypeBlue_Container.SetActive(true);
        spaceTop_Animator.Play("Space Fade in");
    }

    /////////////////////////////////////////////////////////////////

    public void TurnOffSpaceOverlay()
    {
        spaceTypeRed_Container.SetActive(false);
        spaceTypeGreen_Container.SetActive(false);
        spaceTypePurple_Container.SetActive(false);
        spaceTypeBlue_Container.SetActive(false);
    }


    /////////////////////////////////////////////////////////////////
}
