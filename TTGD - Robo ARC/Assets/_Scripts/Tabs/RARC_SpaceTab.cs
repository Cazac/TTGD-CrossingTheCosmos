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
    public RARC_PlanetTabUI planet;

    /////////////////////////////////////////////////////////////////

    public void PlayTravelingSpace()
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

    public void PlayPlanetSpace()
    {
        int randomNo = Random.Range(0, 5);

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

        print("Test Code: Make This Work Later");

        spaceTop_Animator.Play("Idle");
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
        //spaceBottom_Animator.Play("Space Fade in");
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
