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

    ////////////////////////////////



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
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_EmptyTank));
            Button_Event();
            return;
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount <= 0)
        {
            //Create Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_Starvation));
            Button_Event();
            return;
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List.Count <= 0)
        {
            //Create Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_EveryoneIsGone));
            Button_Event();
            return;
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipHullHealth <= 0)
        {
            //Create Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_CatastrophicBreakdown));
            Button_Event();
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


        allRooms_List.Add(RARC_DatabaseController.Instance.room_DB.CloningLabRoom_SO);
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

    public void Button_Event()
    {
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Count != 0)
        {
            //Open Event Menu
            EventMenu_Main.SetActive(true);
            RARC_GameStateController.Instance.EnableRaycastBlocker();

            //Load Event 0
            Event_LoadEvent(RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List[0]);
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
        //Close Event Menu
        //EventMenu_Main.SetActive(false);

        //Convert to non savable data
        RARC_Event eventScript = RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List[0];
        RARC_Event_SO eventSO = eventScript.GetEventSO();

        RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.RemoveAt(0);


        //Find Option Choice
        switch (optionNo)
        {
            case 1:

                if (eventSO.eventOption1_Outcome != null)
                {
                    //Add Next Event If Avalible
                    if (eventSO.eventOption1_Outcome.outcomeNextEvent != null)
                    {
                        RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(eventSO.eventOption1_Outcome.outcomeNextEvent));
                    }

                    //Apply Event Changes
                    Event_ApplyOutcomeReal(eventSO.eventOption1_Outcome.outcomeType1, eventSO.eventOption1_Outcome.outcomeValue1);
                    Event_ApplyOutcomeReal(eventSO.eventOption1_Outcome.outcomeType2, eventSO.eventOption1_Outcome.outcomeValue2);
                    Event_ApplyOutcomeReal(eventSO.eventOption1_Outcome.outcomeType3, eventSO.eventOption1_Outcome.outcomeValue3);
                }

                break;

            case 2:

                if (eventSO.eventOption2_Outcome != null)
                {
                    //Add Next Event If Avalible
                    if (eventSO.eventOption2_Outcome.outcomeNextEvent != null)
                    {
                        RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(eventSO.eventOption2_Outcome.outcomeNextEvent));
                    }

                    //Apply Event Changes
                    Event_ApplyOutcomeReal(eventSO.eventOption2_Outcome.outcomeType1, eventSO.eventOption2_Outcome.outcomeValue1);
                    Event_ApplyOutcomeReal(eventSO.eventOption2_Outcome.outcomeType2, eventSO.eventOption2_Outcome.outcomeValue2);
                    Event_ApplyOutcomeReal(eventSO.eventOption2_Outcome.outcomeType3, eventSO.eventOption2_Outcome.outcomeValue3);
                }

                break;

            case 3:

                if (eventSO.eventOption2_Outcome != null)
                {
                    //Add Next Event If Avalible
                    if (eventSO.eventOption3_Outcome.outcomeNextEvent != null)
                    {
                        RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(eventSO.eventOption3_Outcome.outcomeNextEvent));
                    }

                    //Apply Event Changes
                    Event_ApplyOutcomeReal(eventSO.eventOption3_Outcome.outcomeType1, eventSO.eventOption3_Outcome.outcomeValue1);
                    Event_ApplyOutcomeReal(eventSO.eventOption3_Outcome.outcomeType2, eventSO.eventOption3_Outcome.outcomeValue2);
                    Event_ApplyOutcomeReal(eventSO.eventOption3_Outcome.outcomeType3, eventSO.eventOption3_Outcome.outcomeValue3);
                }

                break;
            }


        //Refresh UI
        RefreshUI_UrgentIcons();
        RefreshUI_ButtonInteractablity();

        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Count != 0)
        {
            //Start again
            Event_LoadEvent(RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List[0]);
        }
        else
        {
            Button_Event_Close();
        }
    }

    public void Event_LoadEvent(RARC_Event eventInfo)
    {
        //Basic Info
        eventTitle_Text.text = eventInfo.eventTitle;
        eventDesc_Text.text = eventInfo.eventDescription;


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





        eventIcon_Image.sprite = eventInfo.GetEventSO().eventIcon;











    }

    public void Event_ApplyOutcomeReal(RARC_EventOutcome_SO.OutcomeType eventOutcomeType, int value)
    {
        //Find Type
        switch (eventOutcomeType)
        {
            case RARC_EventOutcome_SO.OutcomeType.NULL:
                break;

            case RARC_EventOutcome_SO.OutcomeType.HULL_CHANGE:
                RARC_GameStateController.Instance.ChangeHull(value);
                break;

            case RARC_EventOutcome_SO.OutcomeType.CREW_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainCrew(value); }
                else { RARC_GameStateController.Instance.LoseCrew(value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.ROBOT_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainBot(value); }
                else { RARC_GameStateController.Instance.LoseBot(value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.SCRAP_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Scrap", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.FUEL_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Fuel", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.FOOD_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Food", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.TITANIUM_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Titanium", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.SILICON_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Silicon", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.CARBON_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Carbon", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.ORGANICS_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Organics", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.HYDROGEN_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Hydrogen", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.NITROGEN_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Nitrogen", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                break;

            case RARC_EventOutcome_SO.OutcomeType.MEDKITS_CHANGE:
                if (value > 0) { RARC_GameStateController.Instance.GainResources("Medkit", RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
                else { RARC_GameStateController.Instance.LoseResources(RARC_GameStateController.Instance.ConvertTypes(eventOutcomeType), value); }
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

        StartCoroutine(IExplorePlanet(null, effectiveTotal));


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
            RARC_GameStateController.Instance.GainResources(tuple.Item3.resourceName, tuple.Item3.resourceType, tuple.Item3.resourceCount);
        }
    }

    public IEnumerator IExplorePlanet(RARC_Event eventUsed, float effectiveTotal)
    {
        //Turn On Menus
        FinishExploringButton_GO.SetActive(false);
        exploringShowoffInfoContainer_Go.SetActive(true);

        int eventTiming;
        hasShowoffEventCleared = false;
        bool hasEventOccured = false;


        eventUsed = new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_AbandonedShip);

        if (eventUsed != null)
        {
            //Find Random Value In Range
            eventTiming = UnityEngine.Random.Range(25, 75);
        }
        else
        {
            //Never Found
            eventTiming = 999;
        }

        //Loop While Waiting
        while (exploringShowoffProgress < 100)
        {



            int progressValue = (int)exploringShowoffProgress;
            exploringShowoffPercent_Text.text = progressValue + "%";
            exploringShowoffPercent_FillImage.fillAmount = (exploringShowoffProgress / 100);


            if (hasEventOccured == false)
            {
                if (progressValue > eventTiming)
                {

                    print("Test Code: Event!");

                    //Change Text
                    exploringShowoffInfo_Text.text = "<color=" + colorValues_Red + ">Anomaly detected</color>";

                    yield return new WaitForSeconds(0.75f);



                    //Open Event
                    RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(eventUsed);
                    Button_Event();



                    hasEventOccured = true;

                    if (hasShowoffEventCleared == false)
                    {
                        while (hasShowoffEventCleared == false)
                        {
                            yield return new WaitForEndOfFrame();
                        }

                        exploringShowoffInfo_Text.text = "<color=" + colorValues_White + ">Oiling The Bots</color>";

                        yield return new WaitForSeconds(0.75f);
                    }
                }
            }
            else
            {

            }





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







            yield return new WaitForEndOfFrame();
            exploringShowoffProgress += Time.deltaTime * 40;
        }




        //Set to 100%
        exploringShowoffPercent_Text.text = "<color=" + colorValues_Green + ">100%</color>";
        exploringShowoffPercent_FillImage.fillAmount = 1;

        //Reset and Show Exit Button
        FinishExploringButton_GO.SetActive(true);
        exploringShowoffInfoContainer_Go.SetActive(false);
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
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Count != 0)
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
        //resourcesFuel_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount;
        //resourcesFood_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount;
        //resourcesScrap_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount;

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
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Count != 0)
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
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Count != 0)
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
            launchFuelNeeded_Text.text = "<" + colorValues_Black + ">" + "x" + requiredFuel + "</color>";
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount < requiredFood)
        {
            launchFoodNeeded_Text.text = "<" + colorValues_Red + ">" + "x" + requiredFood + "</color>";
        }
        else
        {
            launchFoodNeeded_Text.text = "<" + colorValues_Black + ">" + "x" + requiredFood + "</color>";
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