using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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




    [Header("Weeks In Space")]
    public TextMeshProUGUI weeksAtSpace_Text;


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
        //Debug 
        RARC_GameStateController.Instance.isReady_Launch = true;

        //Check If Launchable
        if (RARC_GameStateController.Instance.isReady_Launch)
        {
            //Finish Week and Animation
            RARC_GameStateController.Instance.Player_FinishWeek();

            //Create a New Week
            RARC_GameStateController.Instance.System_GenerateNewWeek(false);

            //Start new Week
            StartCoroutine(RARC_GameStateController.Instance.Player_StartWeek());

            //Save Next Weeks Data
            RARC_DatabaseController.Instance.SaveShipData();
        }
        //else
        {
            print("Test Code: Oh No! Not Ready!");
        }
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


        RefreshUI_NavigationDestination();
        RefreshUI_UrgentIcons();
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

        //Remove Event 0
        RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.RemoveAt(0);

        RefreshUI_UrgentIcons();
    }

    public void Event_LoadEvent(RARC_Event_SO eventSO)
    {
        eventTitle_Text.text = eventSO.eventTitle;
        eventDesc_Text.text = eventSO.eventDescription;


        if (eventSO.eventOption1_Choice != "")
        {
            eventOption1_GO.SetActive(true);
            eventOption1_Text.text = eventSO.eventOption1_Choice;
        }
        else
        {
            eventOption1_GO.SetActive(false);
        }


        if (eventSO.eventOption2_Choice != "")
        {
            eventOption2_GO.SetActive(true);
            eventOption2_Text.text = eventSO.eventOption2_Choice;
        }
        else
        {
            eventOption2_GO.SetActive(false);
        }


        if (eventSO.eventOption3_Choice != "")
        {
            eventOption3_GO.SetActive(true);
            eventOption3_Text.text = eventSO.eventOption3_Choice;
        }
        else
        {
            eventOption3_GO.SetActive(false);
        }


 


            // eventIcon_Image


            
            
            


            
      

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

    }

    public void Button_Explore_Close()
    {
        //Open Event Menu
        ExploreMenu_Main.SetActive(false);

        //Load Event

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

    public void RefreshUI_WeeksInSpace()
    {
        weeksAtSpace_Text.text = "Week in Space: " + RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived + "/52";
    }

    public void RefreshUI_NavigationDestination()
    {
        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination == null)
        {
            navigationDesination_Text.text = "<color=black>No Planet</color>";
            navigationTravelTime_Text.text = "<color=black>0</color>";
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

    public void RefreshUI_Resources()
    {
        resourcesFuel_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount;
        resourcesFood_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount;
        resourcesScrap_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount;
    }

    /////////////////////////////////////////////////////////////////
}