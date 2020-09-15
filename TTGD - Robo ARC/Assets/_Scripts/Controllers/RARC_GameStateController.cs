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
    public bool isReady_Explore;
    public bool isReady_Event;
    public bool isReady_Research;
    public bool isReady_Contruction;
    public bool isReady_Crew;

    [Header("Interatablity Cover")]
    public Image blackoutCurtain_Image;

    [Header("Animators")]
    public Animator blacokoutCurtain_Animator;
    public Animator ship_Animator;
    public Animator cutScene_Animator;

    public IEnumerator currentCutscene_IEnum;

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

        //Refresh All UI On Startup As Well
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
        RARC_ButtonController_Game.Instance.RefreshUI_NavigationDestination();
        RARC_ButtonController_Game.Instance.RefreshUI_UrgentIcons();
        RARC_ButtonController_Game.Instance.RefreshUI_Resources();
        RARC_ButtonController_Game.Instance.RefreshUI_ButtonInteractablity();


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
        //Temp Bypass Bools
        isReady_Navigation = true;
        isReady_Event = true;
        isReady_Crew = true;
        isReady_Explore = true;
        isReady_Research = true;
        isReady_Contruction = true;

        isReady_Launch = false;



        //Check For New Save File
        if (isFirstWeek)
        {
            //Play Cutscene As Enum
            currentCutscene_IEnum = Player_StartCutscene();
            StartCoroutine(Player_StartCutscene()); 
 
            //Give First Backstory Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_ANewHope));

            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.ClearPlanet();
            RARC_ButtonController_Game.Instance.space_Tab.SetSpace_Black();
        }
        else
        {
            //Progress Time
            RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived++;
            RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress++;


            //Generate Possible Events

        }



        //Remove Old Current Location
        RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation = null;


        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination != null)
        {
            if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress >= RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination.planetTravelTime)
            {
                //Set Location
                RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation = RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination;

                //Set Explore Requirement
                isReady_Explore = false;

                //Reset Counter
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress = 0;

                //Remove Planet Destination
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination = null;
            }
        }







        //Reset Navigation Planets
        navigationPossiblePlanets_List = new List<RARC_Planet>();
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
    }

    /////////////////////////////////////////////////////////////////

    public IEnumerator Player_StartCutscene()
    {
        cutScene_Animator.gameObject.SetActive(true);

        yield return new WaitForSeconds(18.5f);

        if (currentCutscene_IEnum == null)
        {
            yield break;
        }

        print("Test Code: Ending Cutscene");

        cutScene_Animator.gameObject.SetActive(false);

        blacokoutCurtain_Animator.Play("Fade Out");

        currentCutscene_IEnum = null;
    }

    public IEnumerator Player_StartWeek()
    {
        //Do Visuals Here


        yield return new WaitForSeconds(2f);

        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation != null)
        {
            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.SetPlanet(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation);
            RARC_ButtonController_Game.Instance.space_Tab.PlayPlanetSpace();


            //Set Text


     
        }
        else
        {
            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.ClearPlanet();
            RARC_ButtonController_Game.Instance.space_Tab.SetSpace_Black();
        }


        //Refresh All UI
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
        RARC_ButtonController_Game.Instance.RefreshUI_NavigationDestination();
        RARC_ButtonController_Game.Instance.RefreshUI_UrgentIcons();
        RARC_ButtonController_Game.Instance.RefreshUI_Resources();
        RARC_ButtonController_Game.Instance.RefreshUI_ButtonInteractablity();

        yield return new WaitForSeconds(2f);

        //print("Test Code: Ready");

        //Add Interatablity
        blackoutCurtain_Image.raycastTarget = false;

        yield break;
    }

    public void Player_FinishWeek()
    {
        //Remove Interatablity
        RARC_ButtonController_Game.Instance.LaunchButton_Main.interactable = false;
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
