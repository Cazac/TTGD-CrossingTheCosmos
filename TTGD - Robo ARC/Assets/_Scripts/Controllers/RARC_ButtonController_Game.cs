using System;
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
    public Button NavigationButton_Main;
    public Button ConstructionButton_Main;
    public Button CrewButton_Main;
    public Button EventButton_Main;
    public Button ExploreButton_Main;
    public Button ResearchButton_Main;


    [Header("Menu Panels")]
    public GameObject NavigationMenu_Main;
    public GameObject ConstructionMenu_Main;
    public GameObject CrewMenu_Main;
    public GameObject EventMenu_Main;
    public GameObject ExploreMenu_Main;
    public GameObject ResearchMenu_Main;

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

    [Header("Planet Tabs")]
    public RARC_PlanetTabUI navigationPlanet1_Tab;
    public RARC_PlanetTabUI navigationPlanet2_Tab;
    public RARC_PlanetTabUI navigationPlanet3_Tab;


    [Header("Resources")]
    public TextMeshProUGUI resourcesFuel_Text;
    public TextMeshProUGUI resourcesFood_Text;
    public TextMeshProUGUI resourcesScrap_Text;

    /////////////////////////////////////////////////////////////////

    [Header("Explore")]
    public RARC_PlanetTabUI explorePlanet_Tab;
    public TextMeshProUGUI explorePlanetName_Text;
    public TextMeshProUGUI explorationRate_Text;
    public TextMeshProUGUI explorationRiskFactor_Text;
    public Image explorationRiskFactor_FillImage;
    public Image explorationRate_FillImage;

    public TextMeshProUGUI exploringHumans_Text;
    public TextMeshProUGUI exploringBots_Text;
    public Slider exploringHumans_Slider;
    public Slider exploringBots_Slider;

    public Button explorePlanet_Button;


    public RARC_ResourceTab exploringResource1_Tab;
    public RARC_ResourceTab exploringResource2_Tab;
    public RARC_ResourceTab exploringResource3_Tab;





    public List<RARC_ResourceTab> storageResourceTabs_List;

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

    [Header("Space / Planet BG")]
    public RARC_SpaceTab space_Tab;

    [Header("Weeks In Space")]
    public TextMeshProUGUI weeksAtSpace_Text;



    [Header("BLANKVAR")]
    public GameObject gameoverContainer_GO;
    public GameObject gameoverImage_Crew;
    public GameObject gameoverImage_Fuel;
    public GameObject gameoverImage_Food;
    public GameObject gameoverImage_Hull;



    [HideInInspector]
    public GameObject currentSelectedRoom;

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

    public void Button_GameNavigate()
    {
        //Open Navigation Menu
        NavigationMenu_Main.SetActive(true);

        //Load Planet UI
        navigationPlanet1_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[0]);
        navigationPlanet2_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[1]);
        navigationPlanet3_Tab.SetPlanet(RARC_GameStateController.Instance.navigationPossiblePlanets_List[2]);
    }

    public void Button_GameNavigate_Close()
    {
        //Close Navigation Menu
        NavigationMenu_Main.SetActive(false);
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
        Button_GameNavigate_Close();

        //Refresh UI
        RefreshUI_NavigationDestination();
        RefreshUI_UrgentIcons();
        RefreshUI_ButtonInteractablity();
    }

    /////////////////////////////////////////////////////////////////

    public void Button_Game_Build()
    {
        //Open Build Menu
        ConstructionMenu_Main.SetActive(true);
    }

    public void Button_Game_Build_Close()
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);

        //current selected room is none
        currentSelectedRoom = null;
    }

    public void Button_Game_Build_Research()
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);

        if (currentSelectedRoom != null)
        {
            currentSelectedRoom.GetComponent<RARC_Room>().currentRoomType = RARC_Room.RoomType.RESEARCH;
        }
        else
        {
            //set cursor state
            RARC_GameStateController.Instance.currentCursorState = RARC_GameStateController.CursorState.BUILD_RESEARCH;
        }
    }

    public void Button_Game_Build_Medbay()
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);

        //set cursor state
        RARC_GameStateController.Instance.currentCursorState = RARC_GameStateController.CursorState.BUILD_MEDICAL;
    }

    public void Button_Game_Build_Food()
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);

        if (currentSelectedRoom != null)
        {
            currentSelectedRoom.GetComponent<RARC_Room>().currentRoomType = RARC_Room.RoomType.FOOD;
        }
        else
        {
            //set cursor state
            RARC_GameStateController.Instance.currentCursorState = RARC_GameStateController.CursorState.BUILD_FOOD;
        }
    }

    public void Button_Game_Build_Recreation()
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);

        if (currentSelectedRoom != null)
        {
            currentSelectedRoom.GetComponent<RARC_Room>().currentRoomType = RARC_Room.RoomType.RECREATION;
        }
        else
        {
            //set cursor state
            RARC_GameStateController.Instance.currentCursorState = RARC_GameStateController.CursorState.BUILD_RECREATION;
        }
    }

    public void Button_Game_Build_Factory()
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);

        if (currentSelectedRoom != null)
        {
            currentSelectedRoom.GetComponent<RARC_Room>().currentRoomType = RARC_Room.RoomType.FACTORY;
        }
        else
        {
            //set cursor state
            RARC_GameStateController.Instance.currentCursorState = RARC_GameStateController.CursorState.BUILD_FACTORY;
        }
    }

    public void Button_Game_Build_Storage()
    {
        //Close Build Menu
        ConstructionMenu_Main.SetActive(false);

        if (currentSelectedRoom != null)
        {
            currentSelectedRoom.GetComponent<RARC_Room>().currentRoomType = RARC_Room.RoomType.STORAGE;
        }
        else
        {
            //set cursor state
            RARC_GameStateController.Instance.currentCursorState = RARC_GameStateController.CursorState.BUILD_STORAGE;
        }
    }

    /////////////////////////////////////////////////////////////////

    public void Button_Event()
    {
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Count != 0)
        {
            //Open Event Menu
            EventMenu_Main.SetActive(true);

            //Load Event 0
            Event_LoadEvent(RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List[0]);
        }
    }

    public void Button_Event_Close()
    {
        //Open Event Menu
        EventMenu_Main.SetActive(false);

        //Load Event

    }

    public void Button_Event_Option(int optionNo)
    {
        //Close Event Menu
        EventMenu_Main.SetActive(false);

        //Convert to non savable data
        RARC_Event_SO eventSO = RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List[0].GetEventSO();

        //Find Option Choice
        switch (optionNo)
        {
            case 1:


                //Add Next Event If Avalible
                if (eventSO.eventOption1_Outcome.outcomeNextEvent != null)
                {
                    RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(eventSO.eventOption1_Outcome.outcomeNextEvent));
                }

                //Apply Event Changes
                Event_ApplyOutcomeReal(eventSO.eventOption1_Outcome.outcomeType1, eventSO.eventOption1_Outcome.outcomeValue1);
                Event_ApplyOutcomeReal(eventSO.eventOption1_Outcome.outcomeType2, eventSO.eventOption1_Outcome.outcomeValue2);
                Event_ApplyOutcomeReal(eventSO.eventOption1_Outcome.outcomeType3, eventSO.eventOption1_Outcome.outcomeValue3);

                break;

            case 2:

                //Add Next Event If Avalible
                if (eventSO.eventOption2_Outcome.outcomeNextEvent != null)
                {
                    RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(eventSO.eventOption2_Outcome.outcomeNextEvent));
                }

                //Apply Event Changes
                Event_ApplyOutcomeReal(eventSO.eventOption2_Outcome.outcomeType1, eventSO.eventOption2_Outcome.outcomeValue1);
                Event_ApplyOutcomeReal(eventSO.eventOption2_Outcome.outcomeType2, eventSO.eventOption2_Outcome.outcomeValue2);
                Event_ApplyOutcomeReal(eventSO.eventOption2_Outcome.outcomeType3, eventSO.eventOption2_Outcome.outcomeValue3);

                break;

            case 3:

                //Add Next Event If Avalible
                if (eventSO.eventOption3_Outcome.outcomeNextEvent != null)
                {
                    RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(eventSO.eventOption3_Outcome.outcomeNextEvent));
                }

                //Apply Event Changes
                Event_ApplyOutcomeReal(eventSO.eventOption3_Outcome.outcomeType1, eventSO.eventOption3_Outcome.outcomeValue1);
                Event_ApplyOutcomeReal(eventSO.eventOption3_Outcome.outcomeType2, eventSO.eventOption3_Outcome.outcomeValue2);
                Event_ApplyOutcomeReal(eventSO.eventOption3_Outcome.outcomeType3, eventSO.eventOption3_Outcome.outcomeValue3);

                break;
        }

        //Remove Event 0
        RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.RemoveAt(0);

        //Refresh UI
        RefreshUI_UrgentIcons();
        RefreshUI_ButtonInteractablity();
    }

    public void Event_LoadEvent(RARC_Event eventInfo)
    {
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

    public void Event_ApplyOutcomeReal(RARC_EventOutcome_SO.OutcomeType eventOutcomeType, int optionNo)
    {
        //Find Type
        switch (eventOutcomeType)
        {
            case RARC_EventOutcome_SO.OutcomeType.NULL:

                break;

            case RARC_EventOutcome_SO.OutcomeType.GAMEOVER:
                RARC_GameStateController.Instance.System_Gameover("death");
                break;
        }
    }

    /////////////////////////////////////////////////////////////////

    public void Button_Crew()
    {
        //Open Event Menu
        CrewMenu_Main.SetActive(true);

        //Load Event

    }

    public void Button_Crew_Close()
    {
        //Open Event Menu
        CrewMenu_Main.SetActive(false);

        //Load Event

    }

    /////////////////////////////////////////////////////////////////

    public void Button_Explore()
    {
        //Open Event Menu
        ExploreMenu_Main.SetActive(true);

        //Load Event
        explorePlanet_Tab.SetPlanet(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation);
        explorePlanetName_Text.text = RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetName;

        //Risk Factor
        explorationRiskFactor_Text.text = "Expedition Risk: " + RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetRiskFactor;
        explorationRiskFactor_FillImage.fillAmount = (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation.planetRiskFactor * 0.1f);

        
       

        explorationRate_Text.text = "Exploration Rate (0%)";
        exploringHumans_Slider.maxValue = Mathf.Clamp(RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List.Count, 0, 5);
        exploringBots_Slider.maxValue = Mathf.Clamp(RARC_DatabaseController.Instance.ship_SaveData.shipData_Bots_List.Count, 0, 5);


        exploringHumans_Slider.maxValue = 5;
        exploringBots_Slider.maxValue = 5;

        exploringHumans_Slider.value = 0;
        exploringBots_Slider.value = 0;

        Slider_Event_PeopleBotsChange();
    }

    public void Slider_Event_PeopleBotsChange()
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
    }

    public void Button_Explore_StartExploring()
    {
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
            RARC_GameStateController.Instance.AquireResources(tuple.Item3);
        }





        //Calculate Event



        //Remove Tag
        RARC_GameStateController.Instance.isReady_Explore = true;

        //Refresh UI
        RefreshUI_UrgentIcons();
        RefreshUI_ResourcesAndStorage();
        RefreshUI_ButtonInteractablity();

        //Close Event Menu
        ExploreMenu_Main.SetActive(false);
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

    /////////////////////////////////////////////////////////////////

    public void Button_Research()
        {
            //Open Event Menu
            ResearchMenu_Main.SetActive(true);

            //Load Event

        }

    public void Button_Research_Close()
    {
        //Open Event Menu
        ResearchMenu_Main.SetActive(false);

        //Load Event

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
            navigationDesination_Text.text = "<color=red>No Planet</color>";
            navigationTravelTime_Text.text = "<color=red>0</color>";
        }
        else
        {
            navigationDesination_Text.text = "<color=black>" + RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination.planetName + "</color>";
            navigationTravelTime_Text.text = "<color=black>" + RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress + "/" + RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination.planetTravelTime + "</color>"; ;
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
        resourcesFuel_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount;
        resourcesFood_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount;
        resourcesScrap_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount;

        foreach (RARC_ResourceTab tab in storageResourceTabs_List)
        {
            tab.gameObject.SetActive(false);
        }

        for (int i = 0; i < RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List.Count; i++)
        {
            storageResourceTabs_List[i].gameObject.SetActive(true);
            storageResourceTabs_List[i].SetResource_Storage(RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List[i]);
        }
    }

    public void RefreshUI_ButtonInteractablity()
    {
        if (RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Count != 0)
        {
            //Interactable
            EventButton_Main.interactable = true;
        }
        else
        {
            //Not
            EventButton_Main.interactable = false;
        }

        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation == null)
        {
            //Interactable
            //NavigationButton_Main.interactable = true;
        }


        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress == 0)
        {
            //Interactable
            NavigationButton_Main.interactable = true;
        }
        else
        {
            //Not
            NavigationButton_Main.interactable = false;
        }


        if (RARC_GameStateController.Instance.isReady_Explore == false)
        {
            //Interactable
            ExploreButton_Main.interactable = true;
        }
        else
        {
            //Not
            ExploreButton_Main.interactable = false;
        }




        //DEBUG
        CrewButton_Main.interactable = false;
        ResearchButton_Main.interactable = false;
    }

    /////////////////////////////////////////////////////////////////
}