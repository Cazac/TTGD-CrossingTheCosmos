using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RARC_GameStateController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_GameStateController Instance;

    ////////////////////////////////

    [Header("Cursor State")]
    public CursorState currentCursorState;
    public enum CursorState
    {
        NORMAL,
        BUILD_RESEARCH, BUILD_MEDICAL, BUILD_FOOD, BUILD_RECREATION, BUILD_FACTORY, BUILD_STORAGE
    }

    ////////////////////////////////

    [Header("System Ready States")]
    public bool isReady_Launch;

    public bool isReady_Navigation;
    public bool isReady_EventLog;
    public bool isReady_Research;
    public bool isReady_Contruction;
    public bool isReady_Storage;

    [Header("Interatablity Cover")]
    public Image blackoutCurtain_Image;

    [Header("Animators")]
    public Animator blacokoutCurtain_Animator;
    public Animator ship_Animator;

    [Header("Navigation Options")]
    public List<RARC_Planet> navigationPossiblePlanets_List;

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    private void Start()
    {
        //Check if its the first week On load
        if (RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived == 0)
        {
            System_GenerateNewWeek(true);
        }
        else
        {
            System_GenerateNewWeek(false);
        }

        //Load System Data

        //set cursor state
        currentCursorState = CursorState.NORMAL;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(1) && currentCursorState != CursorState.NORMAL)
        {
            currentCursorState = CursorState.NORMAL;
        }
    }

    /////////////////////////////////////////////////////////////////

    public void System_GenerateNewWeek(bool isFirstWeek)
    {

        //Generate Events

        //Progress Time

        if (isFirstWeek)
        {
            print("Test Code: Welcome New Player!");


            //Play Cutscene




        }
        else
        {
            //Progress Time
            RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived++;
            RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress++;



            //Generate Events


        }





        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination != null)
        {
            if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress >= RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination.planetTravelTime)
            {
                //Set BG

                //Reset Counter

                //Remove Planet Dersintation




                isReady_Navigation = false;
            }
            else
            {
                isReady_Navigation = true;
            }
        }
        else
        {
            isReady_Navigation = false;
        }











        //Reset Navigation Planets
        navigationPossiblePlanets_List = new List<RARC_Planet>();
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());

        //Refresh All UI
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
        RARC_ButtonController_Game.Instance.RefreshUI_NavigationDestination();
        RARC_ButtonController_Game.Instance.RefreshUI_UrgentIcons();
        RARC_ButtonController_Game.Instance.RefreshUI_Resources();
    }

    /////////////////////////////////////////////////////////////////

    public IEnumerator Player_StartWeek()
    {
        print("Test Code: Waiting");

        yield return new WaitForSeconds(4f);

        print("Test Code: Ready");

        //Add Interatablity
        blackoutCurtain_Image.raycastTarget = false;

        yield break;
    }

    public void Player_FinishWeek()
    {
        //Remove Interatablity
        blackoutCurtain_Image.raycastTarget = true;

        //Play ANimaitons
        ship_Animator.Play("Travel");

        blacokoutCurtain_Animator.Play("Fade In");
    }

    /////////////////////////////////////////////////////////////////

    public void LoadBackground()
    {

        //if ()
        {

        }


    }

    /////////////////////////////////////////////////////////////////
}
