using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RARC_ButtonController_Game : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_ButtonController_Game Instance;

    ////////////////////////////////

    [Header("Menu Buttons")]
    public Button LaunchButton_Main;
    public Button EventButton_Main;
    public Button FabricationButton_Main;
    public Button NavigationButton_Main;
    public Button ExploreButton_Main;

    [Header("Menu Panels")]
    public GameObject EventMenu_Main;
    public GameObject FabricationMenu_Main;
    public GameObject NavigationMenu_Main;
    public GameObject ExploreMenu_Main;
    public GameObject ConstructionMenu_Main;
    public GameObject PauseMenu_Main;

    /////////////////////////////////////////////////////////////////

    [Header("Navigation Desination")]
    public TextMeshProUGUI navigationDesination_Text;
    public TextMeshProUGUI navigationTravelTime_Text;

    [Header("Urgent Icons")]
    public GameObject urgentIcon_Navigation;
    public GameObject urgentIcon_Contruction;
    public GameObject urgentIcon_EventLog;
    public GameObject urgentIcon_Research;
    public GameObject urgentIcon_Crew;
    public GameObject urgentIcon_Explore;

    /////////////////////////////////////////////////////////////////

    [Header("Navigation - Planet Tabs")]
    public RARC_PlanetTabUI navigationPlanet1_Tab;
    public RARC_PlanetTabUI navigationPlanet2_Tab;
    public RARC_PlanetTabUI navigationPlanet3_Tab;

    /////////////////////////////////////////////////////////////////

    [Header("Event Confirm Menus")]
    public GameObject eventOutcomeMenu_GO;

    public GameObject eventOutcomeNothingResource_GO;
    public RARC_ResourceTab eventOutcomeResource1_Tab;
    public RARC_ResourceTab eventOutcomeResource2_Tab;
    public RARC_ResourceTab eventOutcomeResource3_Tab;
    public RARC_ResourceTab eventOutcomeResource4_Tab;
    public RARC_ResourceTab eventOutcomeResource5_Tab;
    public RARC_ResourceTab eventOutcomeResource6_Tab;
    public RARC_ResourceTab eventOutcomeResource7_Tab;
    public RARC_ResourceTab eventOutcomeResource8_Tab;


    public List<RARC_Resource> eventOutcomeChanges_Resources_List = new List<RARC_Resource>();
    public int eventOutcomeChanges_Hull;
    public int eventOutcomeChanges_Crew;
    public int eventOutcomeChanges_Bots;

    /////////////////////////////////////////////////////////////////

    [Header("Exploring")]
    public RARC_PlanetTabUI explorePlanet_Tab;
    public TextMeshProUGUI explorePlanetName_Text;
    public TextMeshProUGUI explorationRate_Text;
    public TextMeshProUGUI explorationRiskFactor_Text;


    public TextMeshProUGUI exploringHumans_Text;
    public TextMeshProUGUI exploringBots_Text;
    public Slider exploringHumans_Slider;
    public Slider exploringBots_Slider;
    public Image explorationRiskFactor_FillImage;
    public Image explorationRate_FillImage;

    public Button explorePlanet_Button;
    public Button avoidPlanet_Button;

    public RARC_ResourceTab exploringResource1_Tab;
    public RARC_ResourceTab exploringResource2_Tab;
    public RARC_ResourceTab exploringResource3_Tab;



    private bool hasShowoffEventCleared = false;

    public GameObject exploringShowoff_GO;

    public GameObject FinishExploringButton_GO;
    public GameObject exploringShowoffInfoContainer_Go;


    public float exploringShowoffProgress;
    public Image exploringShowoffPercent_FillImage;
    public TextMeshProUGUI exploringShowoffPercent_Text;
    public TextMeshProUGUI exploringShowoffInfo_Text;

    public RARC_ResourceTab exploringShowoffResource1_Tab;
    public RARC_ResourceTab exploringShowoffResource2_Tab;
    public RARC_ResourceTab exploringShowoffResource3_Tab;

    /////////////////////////////////////////////////////////////////

    [Header("Contruction")]
    public GameObject constructionRoomsContainer_GO;
    public GameObject constructionRoom_Prefab;

    /////////////////////////////////////////////////////////////////

    [Header("Event UI")]
    public TextMeshProUGUI eventTitle_Text;
    public TextMeshProUGUI eventDesc_Text;
    public Image eventIcon_Image;

    [Header("Event Buttons")]
    public GameObject eventOption1_GO;
    public GameObject eventOption2_GO;
    public GameObject eventOption3_GO;
    public TextMeshProUGUI eventOption1_Text;
    public TextMeshProUGUI eventOption2_Text;
    public TextMeshProUGUI eventOption3_Text;

    ////////////////////////////////

    [Header("Space / Planet BG")]
    public RARC_SpaceTab space_Tab;

    [Header("Weeks In Space")]
    public TextMeshProUGUI weeksAtSpace_Text;

    [Header("Crew Counters")]
    public TextMeshProUGUI humansAlive_Text;
    public TextMeshProUGUI botsAlive_Text;

    [Header("Storage Tabs")]
    public List<RARC_ResourceTab> storageResourceTabs_List;

    [Header("Launch Requirements")]
    public TextMeshProUGUI launchFuelNeeded_Text;
    public TextMeshProUGUI launchFoodNeeded_Text;

    [Header("Pause Menu Settings")]
    public Slider volumeMusic_Slider;
    public Slider volumeSFX_Slider;
    public TextMeshProUGUI volumeMusic_Text;
    public TextMeshProUGUI volumeSFX_Text;

    ////////////////////////////////

    [Header("Gameover Screens")]
    public GameObject gameoverContainer_GO;
    public GameObject gameoverImage_Crew;
    public GameObject gameoverImage_Fuel;
    public GameObject gameoverImage_Food;
    public GameObject gameoverImage_Hull;

    ////////////////////////////////

    [HideInInspector]
    public readonly string colorValues_Yellow = "#FFEA00";
    public readonly string colorValues_Red = "#FF2200";
    public readonly string colorValues_Black = "#000000";
    public readonly string colorValues_White = "#FFFFFF";
    public readonly string colorValues_Green = "#089800";

    [HideInInspector]
    public GameObject currentSelectedRoom;

    [HideInInspector]
    public RARC_RoomTab currentConstructionRoom;

    [HideInInspector]
    public bool isEventType_Travel;
    public bool isEventType_Planet;

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;

        currentSelectedRoom = null;
    }

    /////////////////////////////////////////////////////////////////

    public void Button_GameLaunch()
    {
        //Remove Resources
        RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount -= RARC_GameStateController.Instance.fuelRequired;
        RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount -= RARC_GameStateController.Instance.foodRequired;

        if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount <= 0)
        {
            //Create Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_EmptyTank));
            Button_EventTravel();
            return;
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount <= 0)
        {
            //Create Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_Starvation));
            Button_EventTravel();
            return;
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List.Count <= 0)
        {
            //Create Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_EveryoneIsGone));
            Button_EventTravel();
            return;
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipHullHealth <= 0)
        {
            //Create Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_CatastrophicBreakdown));
            Button_EventTravel();
            return;
        }

        //Finish Week and Animation
        RARC_GameStateController.Instance.Player_FinishWeek();

        //Create a New Week
        RARC_GameStateController.Instance.System_GenerateNewWeek(false, false);

        //Start new Week
        StartCoroutine(RARC_GameStateController.Instance.Player_StartWeek());

        //Save Next Weeks Data
        RARC_DatabaseController.Instance.SaveShipData();
    }

    /////////////////////////////////////////////////////////////////

    public void Button_Navigate()
    {
        //Open Navigation Menu
        NavigationMenu_Main.SetActive(true);
        RARC_GameStateController.Instance.EnableRaycastBlocker();

        //Load Planet UI
        navigationPlanet1_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[0]);
        navigationPlanet2_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[1]);
        navigationPlanet3_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[2]);
    }

    public void Button_Navigate_Close()
    {
        //Close Navigation Menu
        NavigationMenu_Main.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();
    }

    public void Button_Navigate_SetDestination(int planetNo)
    {
        //Select Tab
        switch (planetNo)
        {
            case 1:
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination = RARC_GameStateController.Instance.navigationPossiblePlanets_List[0];
                break;

            case 2:
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination = RARC_GameStateController.Instance.navigationPossiblePlanets_List[1];
                break;

            case 3:
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination = RARC_GameStateController.Instance.navigationPossiblePlanets_List[2];
                break;

            default:
                break;
        }

        //Close Menu
        Button_Navigate_Close();

        //Refresh UI
        RefreshUI_NavigationDestination();
        RefreshUI_UrgentIcons();
        RefreshUI_ButtonInteractablity();
    }

    public void Button_Navigate_RefreshOption(int planetNo)
    {
        switch (planetNo)
        {
            case 1:
                RARC_GameStateController.Instance.navigationPossiblePlanets_List[0] = RARC_DatabaseController.Instance.planet_SO.GenerateAnyPlanet();
                navigationPlanet1_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[0]);
                break;

            case 2:
                RARC_GameStateController.Instance.navigationPossiblePlanets_List[1] = RARC_DatabaseController.Instance.planet_SO.GenerateAnyPlanet();
                navigationPlanet2_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[1]);
                break;

             case 3:
                RARC_GameStateController.Instance.navigationPossiblePlanets_List[2] = RARC_DatabaseController.Instance.planet_SO.GenerateAnyPlanet();
                navigationPlanet3_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[2]);
                break;
        }
    }

    /////////////////////////////////////////////////////////////////

    public void Button_Construction(RARC_RoomTab roomToBeBuilt)
    {
        //Open Build Menu
        ConstructionMenu_Main.SetActive(true);
        RARC_GameStateController.Instance.EnableRaycastBlocker();

        //Current selected room is passed and saved
        currentConstructionRoom = roomToBeBuilt;

        List<RARC_Rooms_SO> allRooms_List = new List<RARC_Rooms_SO>();


        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.CrewQuartersRoom_SO);
        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.MedbayRoom_SO);
        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.FactoryRoom_SO);
        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.StorageBayRoom_SO);
        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.KitchenRoom_SO);
        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.HydroponicsLabRoom_SO);
        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.AstrometricsLabRoom_SO);
        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.CloningLabRoom_SO);


        foreach (Transform child in constructionRoomsContainer_GO.transform)
        {
            //Remove Children
            Destroy(child.gameObject);
        }

        foreach (RARC_Rooms_SO roomSO in allRooms_List)
        {
            //Refresh All Rooms
            GameObject newRoom_GO = Instantiate(constructionRoom_Prefab, constructionRoomsContainer_GO.transform);
            newRoom_GO.GetComponent<RARC_ConstructionRoomUITab>().SetupTab(roomSO);
        }
    }

    public void Button_Construction_Close()
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();

        //Current selected room is none
        currentConstructionRoom = null;
    }

    public void Button_Construction_BuildRoom(RARC_ConstructionRoomUITab constructionRoomTab)
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();

        //Build Room
        currentConstructionRoom.BuildRoom(constructionRoomTab.roomSO);
    }

    /////////////////////////////////////////////////////////////////

    public void Button_EventOutcome()
    {
        //Close Other Menu
        Button_Event_Close();


        if (exploringShowoff_GO.activeSelf == true)
        {
            return;
        }

        //Turn On Menu
        eventOutcomeMenu_GO.SetActive(true);
        RARC_GameStateController.Instance.EnableRaycastBlocker();

        //Clear Exploration Bool
        hasShowoffEventCleared = true;


        //Show Resources Levels dynmically
        switch (eventOutcomeChanges_Resources_List.Count)
        {
            case 5:
                eventOutcomeNothingResource_GO.SetActive(false);
                eventOutcomeResource1_Tab.gameObject.SetActive(true);
                eventOutcomeResource2_Tab.gameObject.SetActive(true);
                eventOutcomeResource3_Tab.gameObject.SetActive(true);
                eventOutcomeResource4_Tab.gameObject.SetActive(true);
                eventOutcomeResource5_Tab.gameObject.SetActive(true);
                eventOutcomeResource1_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[0]);
                eventOutcomeResource2_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[1]);
                eventOutcomeResource3_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[2]);
                eventOutcomeResource4_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[3]);
                eventOutcomeResource5_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[4]);
                break;

            case 4:
                eventOutcomeNothingResource_GO.SetActive(false);
                eventOutcomeResource1_Tab.gameObject.SetActive(true);
                eventOutcomeResource2_Tab.gameObject.SetActive(true);
                eventOutcomeResource3_Tab.gameObject.SetActive(true);
                eventOutcomeResource4_Tab.gameObject.SetActive(true);
                eventOutcomeResource5_Tab.gameObject.SetActive(false);
                eventOutcomeResource1_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[0]);
                eventOutcomeResource2_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[1]);
                eventOutcomeResource3_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[2]);
                eventOutcomeResource4_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[3]);
                break;

            case 3:
                eventOutcomeNothingResource_GO.SetActive(false);
                eventOutcomeResource1_Tab.gameObject.SetActive(true);
                eventOutcomeResource2_Tab.gameObject.SetActive(true);
                eventOutcomeResource3_Tab.gameObject.SetActive(true);
                eventOutcomeResource4_Tab.gameObject.SetActive(false);
                eventOutcomeResource5_Tab.gameObject.SetActive(false);
                eventOutcomeResource1_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[0]);
                eventOutcomeResource2_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[1]);
                eventOutcomeResource3_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[2]);
                break;

            case 2:
                eventOutcomeNothingResource_GO.SetActive(false);
                eventOutcomeResource1_Tab.gameObject.SetActive(true);
                eventOutcomeResource2_Tab.gameObject.SetActive(true);
                eventOutcomeResource3_Tab.gameObject.SetActive(false);
                eventOutcomeResource4_Tab.gameObject.SetActive(false);
                eventOutcomeResource5_Tab.gameObject.SetActive(false);
                eventOutcomeResource1_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[0]);
                eventOutcomeResource2_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[1]);
                break;

            case 1:
                eventOutcomeNothingResource_GO.SetActive(false);
                eventOutcomeResource1_Tab.gameObject.SetActive(true);
                eventOutcomeResource2_Tab.gameObject.SetActive(false);
                eventOutcomeResource3_Tab.gameObject.SetActive(false);
                eventOutcomeResource4_Tab.gameObject.SetActive(false);
                eventOutcomeResource5_Tab.gameObject.SetActive(false);
                eventOutcomeResource1_Tab.SetResource_OutcomeChanges(eventOutcomeChanges_Resources_List[0]);
                break;

            default:
                eventOutcomeNothingResource_GO.SetActive(true);
                eventOutcomeResource1_Tab.gameObject.SetActive(false);
                eventOutcomeResource2_Tab.gameObject.SetActive(false);
                eventOutcomeResource3_Tab.gameObject.SetActive(false);
                eventOutcomeResource4_Tab.gameObject.SetActive(false);
                eventOutcomeResource5_Tab.gameObject.SetActive(false);
                break;
        }

        if (eventOutcomeChanges_Hull != 0)
        {
            eventOutcomeNothingResource_GO.SetActive(false);
            eventOutcomeResource6_Tab.gameObject.SetActive(false);
            eventOutcomeResource6_Tab.SetResource_OutcomeChanges_Hull(eventOutcomeChanges_Hull);
        }
        else
        {
            eventOutcomeResource6_Tab.gameObject.SetActive(false);
        }

        if (eventOutcomeChanges_Crew != 0)
        {
            eventOutcomeNothingResource_GO.SetActive(false);
            eventOutcomeResource7_Tab.gameObject.SetActive(false);
            eventOutcomeResource7_Tab.SetResource_OutcomeChanges_Hull(eventOutcomeChanges_Crew);
        }
        else
        {
            eventOutcomeResource7_Tab.gameObject.SetActive(false);
        }

        if (eventOutcomeChanges_Bots != 0)
        {
            eventOutcomeNothingResource_GO.SetActive(false);
            eventOutcomeResource8_Tab.gameObject.SetActive(false);
            eventOutcomeResource8_Tab.SetResource_OutcomeChanges_Hull(eventOutcomeChanges_Bots);
        }
        else
        {
            eventOutcomeResource8_Tab.gameObject.SetActive(false);
        }
    }

    public void Button_EventOutcome_Close()
    {
        //Turn On Menu
        eventOutcomeMenu_GO.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();

        //Add Values
        foreach (RARC_Resource resource in eventOutcomeChanges_Resources_List)
        {
            RARC_GameStateController.Instance.ChangeResources(resource.resourceName, resource.resourceType, resource.resourceCount);
        }

        RARC_GameStateController.Instance.ChangeHull(eventOutcomeChanges_Hull);
        RARC_GameStateController.Instance.ChangeCrew(eventOutcomeChanges_Crew);
        RARC_GameStateController.Instance.ChangeBots(eventOutcomeChanges_Bots);


        //Clear LIst
        eventOutcomeChanges_Resources_List.Clear();
        eventOutcomeChanges_Hull = 0;
        eventOutcomeChanges_Crew = 0;
        eventOutcomeChanges_Bots = 0;
    }

    /////////////////////////////////////////////////////////////////

    public void Button_EventTravel()
    {
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Count != 0)
        {
            isEventType_Travel = true;
            isEventType_Planet = false;

            //Open Event Menu
            EventMenu_Main.SetActive(true);
            RARC_GameStateController.Instance.EnableRaycastBlocker();

            //Load Event 0
            Event_LoadEvent(RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List[0]);
        }
    }

    public void Button_EventPlanet()
    {
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.Count != 0)
        {
            isEventType_Travel = false;
            isEventType_Planet = true;

            //Open Event Menu
            EventMenu_Main.SetActive(true);
            RARC_GameStateController.Instance.EnableRaycastBlocker();

            //Load Event 0
            Event_LoadEvent(RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List[0]);
        }
    }

    public void Button_Event_Close()
    {
        //Open Event Menu
        EventMenu_Main.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();


        //Clear Exploration Bool
        hasShowoffEventCleared = true;
    }
   
    public void Button_Event_Option(int optionNo)
    {
        //Convert to non savable data
        RARC_Event eventScript;
        RARC_Event_SO eventSO = null;

        //Check Type
        if (isEventType_Travel)
        {
            //Convert to non savable data
            eventScript = RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List[0];
            eventSO = eventScript.GetEventSO();
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.RemoveAt(0);
        }
        else if (isEventType_Planet)
        {
            //Convert to non savable data
            eventScript = RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List[0];
            eventSO = eventScript.GetEventSO();
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.RemoveAt(0);
        }




        //Find Option Choice
        switch (optionNo)
        {
            case 1:

                if (eventSO.eventOption1_Outcome != null)
                {
                    //Add Next Event If Avalible
                    if (eventSO.eventOption1_Outcome.outcomeNextEvent != null)
                    {
                        //Check Type
                        if (isEventType_Travel)
                        {
                            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Add(new RARC_Event(eventSO.eventOption1_Outcome.outcomeNextEvent));
                        }
                        else if (isEventType_Planet)
                        {
                            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.Add(new RARC_Event(eventSO.eventOption1_Outcome.outcomeNextEvent));
                        }
                    }

                    //Apply Event Changes
                    Event_AddOutcomeChanges(eventSO.eventOption1_Outcome.outcomeType1, eventSO.eventOption1_Outcome.outcomeValue1);
                    Event_AddOutcomeChanges(eventSO.eventOption1_Outcome.outcomeType2, eventSO.eventOption1_Outcome.outcomeValue2);
                    Event_AddOutcomeChanges(eventSO.eventOption1_Outcome.outcomeType3, eventSO.eventOption1_Outcome.outcomeValue3);
                }

                break;

            case 2:

                if (eventSO.eventOption2_Outcome != null)
                {
                    //Add Next Event If Avalible
                    if (eventSO.eventOption2_Outcome.outcomeNextEvent != null)
                    {
                        //Check Type
                        if (isEventType_Travel)
                        {
                            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Add(new RARC_Event(eventSO.eventOption2_Outcome.outcomeNextEvent));
                        }
                        else if (isEventType_Planet)
                        {
                            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.Add(new RARC_Event(eventSO.eventOption2_Outcome.outcomeNextEvent));
                        }
                    }

                    //Apply Event Changes
                    Event_AddOutcomeChanges(eventSO.eventOption2_Outcome.outcomeType1, eventSO.eventOption2_Outcome.outcomeValue1);
                    Event_AddOutcomeChanges(eventSO.eventOption2_Outcome.outcomeType2, eventSO.eventOption2_Outcome.outcomeValue2);
                    Event_AddOutcomeChanges(eventSO.eventOption2_Outcome.outcomeType3, eventSO.eventOption2_Outcome.outcomeValue3);
                }

                break;

            case 3:

                if (eventSO.eventOption2_Outcome != null)
                {
                    //Add Next Event If Avalible
                    if (eventSO.eventOption3_Outcome.outcomeNextEvent != null)
                    {
                        //Check Type
                        if (isEventType_Travel)
                        {
                            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Add(new RARC_Event(eventSO.eventOption3_Outcome.outcomeNextEvent));
                        }
                        else if (isEventType_Planet)
                        {
                            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.Add(new RARC_Event(eventSO.eventOption3_Outcome.outcomeNextEvent));
                        }
                    }

                    //Apply Event Changes
                    Event_AddOutcomeChanges(eventSO.eventOption3_Outcome.outcomeType1, eventSO.eventOption3_Outcome.outcomeValue1);
                    Event_AddOutcomeChanges(eventSO.eventOption3_Outcome.outcomeType2, eventSO.eventOption3_Outcome.outcomeValue2);
                    Event_AddOutcomeChanges(eventSO.eventOption3_Outcome.outcomeType3, eventSO.eventOption3_Outcome.outcomeValue3);
                }

                break;
            }


        //Refresh UI
        RefreshUI_UrgentIcons();
        RefreshUI_ButtonInteractablity();

        //Check Type
        if (isEventType_Travel)
        {
            if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Count != 0)
            {
                //Start again
                Event_LoadEvent(RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List[0]);
            }
            else
            {
                Button_EventOutcome();
            }
        }
        else if (isEventType_Planet)
        {
            if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.Count != 0)
            {
                //Start again
                Event_LoadEvent(RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List[0]);
            }
            else
            {
                Button_EventOutcome();
            }
        }
    }

    public void Event_LoadEvent(RARC_Event eventInfo)
    {
        //Basic Info
        eventTitle_Text.text = eventInfo.eventTitle;
        eventDesc_Text.text = eventInfo.eventDescription;
        eventIcon_Image.sprite = eventInfo.GetEventSO().eventIcon;

        if (eventInfo.eventOption1_Choice != "")
        {
            eventOption1_GO.SetActive(true);
            eventOption1_Text.text = eventInfo.eventOption1_Choice;
        }
        else
        {
            eventOption1_GO.SetActive(false);
        }


        if (eventInfo.eventOption2_Choice != "")
        {
            eventOption2_GO.SetActive(true);
            eventOption2_Text.text = eventInfo.eventOption2_Choice;
        }
        else
        {
            eventOption2_GO.SetActive(false);
        }


        if (eventInfo.eventOption3_Choice != "")
        {
            eventOption3_GO.SetActive(true);
            eventOption3_Text.text = eventInfo.eventOption3_Choice;
        }
        else
        {
            eventOption3_GO.SetActive(false);
        }








    }

    public void Event_AddOutcomeChanges(RARC_EventOutcome_SO.OutcomeType eventOutcomeType, int value)
    {
        //Find Type
        switch (eventOutcomeType)
        {
            case RARC_EventOutcome_SO.OutcomeType.NULL:
                break;

            case RARC_EventOutcome_SO.OutcomeType.HULL_CHANGE:
                eventOutcomeChanges_Hull += value;
                break;

            case RARC_EventOutcome_SO.OutcomeType.CREW_CHANGE:
                eventOutcomeChanges_Crew += value;
                break;

            case RARC_EventOutcome_SO.OutcomeType.ROBOT_CHANGE:
                eventOutcomeChanges_Bots += value;
                break;

            case RARC_EventOutcome_SO.OutcomeType.SCRAP_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.FUEL_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.FOOD_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.TITANIUM_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.SILICON_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.CARBON_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.ORGANICS_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.HYDROGEN_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.NITROGEN_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.MEDKITS_CHANGE:
                eventOutcomeChanges_Resources_List.Add(new RARC_Resource(value, RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType)));
                break;

            case RARC_EventOutcome_SO.OutcomeType.GAMEOVER:
                RARC_GameStateController.Instance.System_Gameover("death");
                break;
        }
    }

    /////////////////////////////////////////////////////////////////

    public void Button_Fabrication()
    {
        //Open Event Menu
        FabricationMenu_Main.SetActive(true);
        RARC_GameStateController.Instance.EnableRaycastBlocker();

        //Load Event

    }

    public void Button_Fabrication_Close()
    {
        //Open Event Menu
        FabricationMenu_Main.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();

        //Load Event

    }

    /////////////////////////////////////////////////////////////////

    public void Button_Explore()
    {
        //Open Event Menu
        ExploreMenu_Main.SetActive(true);
        RARC_GameStateController.Instance.EnableRaycastBlocker();

        //Load Event
        explorePlanet_Tab.SetPlanet(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation);
        explorePlanetName_Text.text = RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetName;

        //Risk Factor
        explorationRiskFactor_Text.text = "Expedition Risk: " + RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetRiskFactor;
        explorationRiskFactor_FillImage.fillAmount = (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetRiskFactor * 0.1f);

        
       

        explorationRate_Text.text = "Exploration Rate (0%)";
        exploringHumans_Slider.maxValue = Mathf.Clamp(RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List.Count, 0, 5);
        exploringBots_Slider.maxValue = Mathf.Clamp(RARC_DatabaseController.Instance.ship_SaveData.shipData_Bots_List.Count, 0, 5);


        //exploringHumans_Slider.maxValue = 5;
        //exploringBots_Slider.maxValue = 5;

        exploringHumans_Slider.value = 0;
        exploringBots_Slider.value = 0;

        Slider_Explore_PeopleBotsChange();
    }

    public void Slider_Explore_PeopleBotsChange()
    {
        //exploringHumans_Slider;
        //exploringBots_Slider;


        exploringHumans_Text.text = "Exploring Crew " + exploringHumans_Slider.value  + "/" + exploringHumans_Slider.maxValue;
        exploringBots_Text.text = "Exploring Bots " + exploringBots_Slider.value + "/" + exploringBots_Slider.maxValue;

        float humanEffectivness = 0.50f;
        float botEffectivness = 0.25f;
        float effectiveTotal = Mathf.Clamp((exploringHumans_Slider.value * humanEffectivness) + (exploringBots_Slider.value * botEffectivness), 0, 1);


        explorationRate_Text.text = "Exploration Rate (" + (effectiveTotal * 100) + " %)";
        explorationRate_FillImage.fillAmount = effectiveTotal;

        if (effectiveTotal == 0)
        {
            explorePlanet_Button.interactable = false;
        }
        else
        {
            explorePlanet_Button.interactable = true;
        }

        //Resources
        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List.Count >= 3)
        {
            exploringResource1_Tab.gameObject.SetActive(true);
            exploringResource2_Tab.gameObject.SetActive(true);
            exploringResource3_Tab.gameObject.SetActive(true);

            exploringResource1_Tab.SetResource_Gathering(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[0], effectiveTotal);
            exploringResource2_Tab.SetResource_Gathering(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[1], effectiveTotal);
            exploringResource3_Tab.SetResource_Gathering(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[2], effectiveTotal);
        }
        else if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List.Count >= 2)
        {
            exploringResource1_Tab.gameObject.SetActive(true);
            exploringResource2_Tab.gameObject.SetActive(true);
            exploringResource3_Tab.gameObject.SetActive(false);

            exploringResource1_Tab.SetResource_Gathering(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[0], effectiveTotal);
            exploringResource2_Tab.SetResource_Gathering(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[1], effectiveTotal);
        }
        else if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List.Count >= 1)
        {
            exploringResource1_Tab.gameObject.SetActive(true);
            exploringResource2_Tab.gameObject.SetActive(false);
            exploringResource3_Tab.gameObject.SetActive(false);

            exploringResource1_Tab.SetResource_Gathering(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[0], effectiveTotal);
        }
        else
        {
            exploringResource1_Tab.gameObject.SetActive(false);
            exploringResource2_Tab.gameObject.SetActive(false);
            exploringResource3_Tab.gameObject.SetActive(false);
        }
    }

    public void Button_Explore_Close()
    {
        //Close Event Menu
        ExploreMenu_Main.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();
    }

    public void Button_Explore_StartExploring()
    {
        //Get Slider Values
        float humanEffectivness = 0.50f;
        float botEffectivness = 0.25f;

        //Calculate Rate
        float effectiveTotal = Mathf.Clamp((exploringHumans_Slider.value * humanEffectivness) + (exploringBots_Slider.value * botEffectivness), 0, 1);
        float effectivenessRate = (effectiveTotal - 1);





        //Calculate Event



        //Remove Tag
        RARC_GameStateController.Instance.isReady_Explore = true;

        //Refresh UI
        RefreshUI_UrgentIcons();
        RefreshUI_ResourcesAndStorage();
        RefreshUI_ButtonInteractablity();

        //Close Event Menu
        ExploreMenu_Main.SetActive(false);


        exploringShowoff_GO.SetActive(true);




        //Try For a Planet Event
        RARC_Event eventOnPlanet = null;
        RARC_GameStateController.Instance.GetPossibleNewEvent_Planet();
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.Count != 0)
        {
            eventOnPlanet = RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List[0];
        }



        //Start Animation Of Exploring
        StartCoroutine(IExplorePlanet(eventOnPlanet, effectiveTotal));
    }

    public void Button_Explore_AbandonExploring()
    {
        //Remove Tag
        RARC_GameStateController.Instance.isReady_Explore = true;

        //Refresh UI
        RefreshUI_UrgentIcons();
        RefreshUI_ButtonInteractablity();

        //Close Event Menu
        ExploreMenu_Main.SetActive(false);
    }

    public void Button_Explore_FinishExploring()
    {
        //Remove Menu
        exploringShowoff_GO.SetActive(false);

        //Get Slider Values
        float humanEffectivness = 0.50f;
        float botEffectivness = 0.25f;

        //Calculate Rate
        float effectiveTotal = Mathf.Clamp((exploringHumans_Slider.value * humanEffectivness) + (exploringBots_Slider.value * botEffectivness), 0, 1);
        float effectivenessRate = (effectiveTotal - 1);

        //Add Resources
        foreach (Tuple<int, int, RARC_Resource> tuple in RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List)
        {
            //Calculate Bonus 
            int bonusValue = (int)(tuple.Item2 * effectivenessRate);
            int value = tuple.Item2 + bonusValue;

            //Set Value
            tuple.Item3.resourceCount = value;
            eventOutcomeChanges_Resources_List.Add(tuple.Item3);
        }


        Button_Event_Close();
        Button_EventOutcome();
    }

    public IEnumerator IExplorePlanet(RARC_Event eventUsed, float effectiveTotal)
    {
        //Turn On Menus
        //FinishExploringButton_GO.SetActive(false);
        exploringShowoffInfoContainer_Go.SetActive(true);

        //Setup Values
        int currentTextNo = 0;
        int percentWhenEventOccurs = 999;
        bool hasEventOccured = false;
        hasShowoffEventCleared = false;

        //Check If Event In Present and set Values
        if (eventUsed != null)
        {
            //Find Random Value In Range
            percentWhenEventOccurs = UnityEngine.Random.Range(25, 75);
        }

   


        //Loop While Waiting For Percent to max
        while (exploringShowoffProgress < 100)
        {
            //Generate Percent
            int progressValue = (int)exploringShowoffProgress;
            exploringShowoffPercent_Text.text = progressValue + "%";
            exploringShowoffPercent_FillImage.fillAmount = (exploringShowoffProgress / 100);

            if (currentTextNo == 0 && exploringShowoffProgress > 0)
            {
                exploringShowoffInfo_Text.text = "<color=" + colorValues_White + ">" + RARC_DatabaseController.Instance.expolringText_List[UnityEngine.Random.Range(0, RARC_DatabaseController.Instance.expolringText_List.Count)] + "</color>";
                currentTextNo++;
            }
            else if (currentTextNo == 1 && exploringShowoffProgress > 33)
            {
                exploringShowoffInfo_Text.text = "<color=" + colorValues_White + ">" + RARC_DatabaseController.Instance.expolringText_List[UnityEngine.Random.Range(0, RARC_DatabaseController.Instance.expolringText_List.Count)] + "</color>";
                currentTextNo++;
            }
            else if (currentTextNo == 2 && exploringShowoffProgress > 66)
            {
                exploringShowoffInfo_Text.text = "<color=" + colorValues_White + ">" + RARC_DatabaseController.Instance.expolringText_List[UnityEngine.Random.Range(0, RARC_DatabaseController.Instance.expolringText_List.Count)] + "</color>";
                currentTextNo++;
            }

            //Check If Event has happened Yet
            if (hasEventOccured == false)
            {
                //Event has matched percentage complete of exploration
                if (progressValue > percentWhenEventOccurs)
                {
                    //Change Textand stop progress
                    exploringShowoffInfo_Text.text = "<color=" + colorValues_Red + ">Anomaly detected</color>";

                    //Wait for dramitic effect
                    yield return new WaitForSeconds(0.60f);

                    //Open Event and Stop Time From counting up the percent
                    //RARC_DatabaseController.Instance.ship_SaveData.shipCurrentPlanetEvents_List.Add(eventUsed);
                    Button_EventPlanet();
                    hasEventOccured = true;

                    //If even has yet to be "Confirmed" loop wait for frame
                    if (hasShowoffEventCleared == false)
                    {
                        while (hasShowoffEventCleared == false)
                        {
                            //Constantly wait for a closed menu
                            yield return new WaitForEndOfFrame();
                        }

                        //Set Desc Text
                        exploringShowoffInfo_Text.text = "<color=" + colorValues_White + ">Oiling The Bots</color>";

                        //Wait for dramitic effect After menu is closed
                        yield return new WaitForSeconds(0.60f);
                    }
                }
            }
      
            //Show Resources Levels dynmically
            switch (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List.Count)
            {
                case 3:
                    exploringShowoffResource1_Tab.gameObject.SetActive(true);
                    exploringShowoffResource2_Tab.gameObject.SetActive(true);
                    exploringShowoffResource3_Tab.gameObject.SetActive(true);
                    exploringShowoffResource1_Tab.SetResource_Collecting(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[0], effectiveTotal * (exploringShowoffProgress / 100));
                    exploringShowoffResource2_Tab.SetResource_Collecting(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[1], effectiveTotal * (exploringShowoffProgress / 100));
                    exploringShowoffResource3_Tab.SetResource_Collecting(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[2], effectiveTotal * (exploringShowoffProgress / 100));
                    break;

                case 2:
                    exploringShowoffResource1_Tab.gameObject.SetActive(true);
                    exploringShowoffResource2_Tab.gameObject.SetActive(true);
                    exploringShowoffResource3_Tab.gameObject.SetActive(false);
                    exploringShowoffResource1_Tab.SetResource_Collecting(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[0], effectiveTotal * (exploringShowoffProgress / 100));
                    exploringShowoffResource2_Tab.SetResource_Collecting(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[1], effectiveTotal * (exploringShowoffProgress / 100));
                    break;

                case 1:
                    exploringShowoffResource1_Tab.gameObject.SetActive(true);
                    exploringShowoffResource2_Tab.gameObject.SetActive(false);
                    exploringShowoffResource3_Tab.gameObject.SetActive(false);
                    exploringShowoffResource1_Tab.SetResource_Collecting(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetResources_List[0], effectiveTotal * (exploringShowoffProgress / 100));
                    break;

                default:
                    exploringShowoffResource1_Tab.gameObject.SetActive(false);
                    exploringShowoffResource2_Tab.gameObject.SetActive(false);
                    exploringShowoffResource3_Tab.gameObject.SetActive(false);
                    break;
            }

            //Wait For next frame and bumb up progress
            yield return new WaitForEndOfFrame();
            exploringShowoffProgress += Time.deltaTime * 40;
        }

        //Set to 100%
        exploringShowoffPercent_Text.text = "<color=" + colorValues_Green + ">100%</color>";
        exploringShowoffPercent_FillImage.fillAmount = 1;


        yield return new WaitForSeconds(0.60f);
        Button_Explore_FinishExploring();


        //Reset and Show Exit Button
        //FinishExploringButton_GO.SetActive(true);
        //exploringShowoffInfoContainer_Go.SetActive(false);
        exploringShowoffProgress = 0;

        //Finish
        yield break;
    }

    /////////////////////////////////////////////////////////////////

    public void Button_Pause()
    {
        PauseMenu_Main.SetActive(true);
        RARC_GameStateController.Instance.EnableRaycastBlocker();

        //Total
        //volumeTotal_Slider.value = RARC_DatabaseController.Instance.player_SaveData.settings_TotalVolume;

        //Music
        volumeMusic_Slider.value = RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume * 100;
        volumeMusic_Text.text = volumeMusic_Slider.value + "%";

        //SFX
        volumeSFX_Slider.value = RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume * 100;
        volumeSFX_Text.text = volumeSFX_Slider.value + "%";
    }

    public void Button_Pause_Resume()
    {
        PauseMenu_Main.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();
    }

    public void Button_Pause_Close()
    {
        PauseMenu_Main.SetActive(false);
        RARC_GameStateController.Instance.DisableRaycastBlocker();
    }

    public void Button_Pause_Quit()
    {
        SceneManager.LoadScene("RARC_Title");
    }

    public void Slider_Pause_MusicVolume()
    {
        RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume = volumeMusic_Slider.value / 100;
        volumeMusic_Text.text = volumeMusic_Slider.value + "%";
        RARC_MusicController.Instance.VolumeLevels_UpdateAll(RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isMusicMuted);
    }

    public void Slider_Pause_SFXVolume()
    {
        RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume = volumeSFX_Slider.value / 100;
        volumeSFX_Text.text = volumeSFX_Slider.value + "%";
        RARC_SFXController.Instance.VolumeLevels_UpdateAll(RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isSFXMuted);
    }

    /////////////////////////////////////////////////////////////////

    public void Button_Gameover_ReturnToTitle()
    {
        //Delete Save File
        RARC_DatabaseController.Instance.DeleteShipData(RARC_DatabaseController.Instance.ship_SaveSlot);

        //Load Scene
        SceneManager.LoadScene("RARC_Title");
    }

    /////////////////////////////////////////////////////////////////

    public void RefreshUI_WeeksInSpace()
    {
        weeksAtSpace_Text.text = "Week in Space: " + RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived + "/52";
    }

    public void RefreshUI_NavigationDestination()
    {
        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination == null)
        {
            navigationDesination_Text.text = "<" + colorValues_Red + ">" + "No Planet</color>";
            navigationTravelTime_Text.text = "<" + colorValues_Red + ">" + "0</color>";
        }
        else
        {
            navigationDesination_Text.text = "<" + colorValues_Yellow + "> " + RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination.planetName + "</color>";
            navigationTravelTime_Text.text = "<" + colorValues_Yellow + "> " + RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress + " / " + RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination.planetTravelTime + "</color>"; ;
        }
    }

    public void RefreshUI_UrgentIcons()
    {
        //Navigation
        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination == null)
        {
            RARC_GameStateController.Instance.isReady_Navigation = false;
        }
        else
        {
            RARC_GameStateController.Instance.isReady_Navigation = true;
        }

        //Events
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Count != 0)
        {
            RARC_GameStateController.Instance.isReady_Event = false;
        }
        else
        {
            RARC_GameStateController.Instance.isReady_Event = true; 
        }






        //Set Urgent Icons as Opposites of the current Ready State
        urgentIcon_Navigation.SetActive(!RARC_GameStateController.Instance.isReady_Navigation);
        urgentIcon_EventLog.SetActive(!RARC_GameStateController.Instance.isReady_Event);
        urgentIcon_Research.SetActive(!RARC_GameStateController.Instance.isReady_Research);
        urgentIcon_Contruction.SetActive(!RARC_GameStateController.Instance.isReady_Contruction);
        urgentIcon_Crew.SetActive(!RARC_GameStateController.Instance.isReady_Crew);
        urgentIcon_Explore.SetActive(!RARC_GameStateController.Instance.isReady_Explore);


        //Check If launchable
        if (RARC_GameStateController.Instance.isReady_Navigation 
            && RARC_GameStateController.Instance.isReady_Event 
            && RARC_GameStateController.Instance.isReady_Crew
            && RARC_GameStateController.Instance.isReady_Research 
            && RARC_GameStateController.Instance.isReady_Contruction
            && RARC_GameStateController.Instance.isReady_Explore)
        {
            RARC_GameStateController.Instance.isReady_Launch = true;
        }

        if (RARC_GameStateController.Instance.isReady_Launch)
        {
            LaunchButton_Main.interactable = true;
        }
        else
        {
            LaunchButton_Main.interactable = false;
        }
    }

    public void RefreshUI_ResourcesAndStorage()
    {
        //Info On Humans and Bots
        humansAlive_Text.text = "<" + colorValues_White + ">" + "x" + RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List.Count + "</color>";
        botsAlive_Text.text = "<" + colorValues_White + ">" + "x" + RARC_DatabaseController.Instance.ship_SaveData.shipData_Bots_List.Count + "</color>";

        foreach (RARC_ResourceTab tab in storageResourceTabs_List)
        {
            //Turn Off All Current Tabs
            tab.gameObject.SetActive(false);
        }

        //Activate 3 Major Tabs always
        storageResourceTabs_List[0].gameObject.SetActive(true);
        storageResourceTabs_List[0].SetResource_Storage(RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel);
        storageResourceTabs_List[1].gameObject.SetActive(true);
        storageResourceTabs_List[1].SetResource_Storage(RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food);
        storageResourceTabs_List[2].gameObject.SetActive(true);
        storageResourceTabs_List[2].SetResource_Storage(RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap);

        //Activate All Other Tabs
        for (int i = 3; i < RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List.Count + 3; i++)
        {
            storageResourceTabs_List[i].gameObject.SetActive(true);
            storageResourceTabs_List[i].SetResource_Storage(RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List[i - 3]);
        }
    }

    public void RefreshUI_ButtonInteractablity()
    {
        //Fabrication
        FabricationButton_Main.gameObject.transform.parent.gameObject.SetActive(true);
        FabricationButton_Main.interactable = true;

        //Event
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Count != 0)
        {
            //Interactable
            EventButton_Main.gameObject.transform.parent.gameObject.SetActive(true);
            EventButton_Main.interactable = true;
        }
        else
        {
            //Not
            EventButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
            EventButton_Main.interactable = false;
        }

        //Navigation
        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress == 0)
        {
            //Interactable
            NavigationButton_Main.gameObject.transform.parent.gameObject.SetActive(true);
            NavigationButton_Main.interactable = true;
        }
        else
        {
            //Not
            NavigationButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
            NavigationButton_Main.interactable = false;
        }

        //Explore
        if (RARC_GameStateController.Instance.isReady_Explore == false)
        {
            //Interactable
            ExploreButton_Main.gameObject.transform.parent.gameObject.SetActive(true);
            ExploreButton_Main.interactable = true;
        }
        else
        {
            //Not
            ExploreButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
            ExploreButton_Main.interactable = false;
        }
    }

    public void RefreshUI_ButtonAvailability_Off()
    {
        FabricationButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
        NavigationButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
        EventButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
        ExploreButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void RefreshUI_ButtonAvailability_On()
    {
        //List of animators In order
        List<Animator> buttonAnimators_List = new List<Animator>();

        //Fabirtation
        FabricationButton_Main.gameObject.transform.parent.gameObject.SetActive(true);
        FabricationButton_Main.interactable = true;
        FabricationButton_Main.gameObject.GetComponent<Animator>().Play("Animation - Button Wait");
        buttonAnimators_List.Add(FabricationButton_Main.gameObject.GetComponent<Animator>());
        
        //Navigation
        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress == 0)
        {
            //Interactable
            NavigationButton_Main.gameObject.transform.parent.gameObject.SetActive(true);
            NavigationButton_Main.interactable = true;
            NavigationButton_Main.gameObject.GetComponent<Animator>().Play("Animation - Button Wait");
            buttonAnimators_List.Add(NavigationButton_Main.gameObject.GetComponent<Animator>());
        }
        else
        {
            //Not
            NavigationButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
            NavigationButton_Main.interactable = false;
        }

        //Explore
        if (RARC_GameStateController.Instance.isReady_Explore == false)
        {
            //Interactable
            ExploreButton_Main.gameObject.transform.parent.gameObject.SetActive(true);
            ExploreButton_Main.interactable = true;
            ExploreButton_Main.gameObject.GetComponent<Animator>().Play("Animation - Button Wait");
            buttonAnimators_List.Add(ExploreButton_Main.gameObject.GetComponent<Animator>());
        }
        else
        {
            //Not
            ExploreButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
            ExploreButton_Main.interactable = false;
        }

        //Event
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentTravelEvents_List.Count != 0)
        {
            //Interactable
            EventButton_Main.gameObject.transform.parent.gameObject.SetActive(true);
            EventButton_Main.interactable = true;
            EventButton_Main.gameObject.GetComponent<Animator>().Play("Animation - Button Wait");
            buttonAnimators_List.Add(EventButton_Main.gameObject.GetComponent<Animator>());
        }
        else
        {
            //Not
            EventButton_Main.gameObject.transform.parent.gameObject.SetActive(false);
            EventButton_Main.interactable = false;
        }

        float time = 0f;
        foreach (Animator animator in buttonAnimators_List)
        {
            //Play All Animators In Orders
            StartCoroutine(IAnimateButtons(time, animator));
            time += 0.1f;
        }
    }

    public void RefreshUI_LaunchResources()
    {
        int requiredFuel = RARC_GameStateController.Instance.fuelRequired;
        int requiredFood = RARC_GameStateController.Instance.foodRequired;

        if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount < requiredFuel)
        {
            launchFuelNeeded_Text.text = "<" + colorValues_Red + ">" + "x" + requiredFuel + "</color>";
        }
        else
        {
            launchFuelNeeded_Text.text = "<" + colorValues_White + ">" + "x" + requiredFuel + "</color>";
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount < requiredFood)
        {
            launchFoodNeeded_Text.text = "<" + colorValues_Red + ">" + "x" + requiredFood + "</color>";
        }
        else
        {
            launchFoodNeeded_Text.text = "<" + colorValues_White + ">" + "x" + requiredFood + "</color>";
        }
    }

    /////////////////////////////////////////////////////////////////

    public IEnumerator IAnimateButtons(float TimeToWait, Animator animator)
    {
        yield return new WaitForSeconds(TimeToWait);

        animator.Play("Animation - Button Slide");


        yield break;
    }

    /////////////////////////////////////////////////////////////////
}