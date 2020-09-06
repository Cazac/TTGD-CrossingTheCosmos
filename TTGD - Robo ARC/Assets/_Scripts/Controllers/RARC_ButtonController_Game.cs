using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RARC_ButtonController_Game : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_ButtonController_Game Instance;

    ////////////////////////////////



    [Header("Menu Panels Icons")]
    public GameObject NavigationMenu_Main;
    public GameObject ConstructionMenu_Main;

    [Header("Navigation Desination")]
    public TextMeshProUGUI navigationDesination_Text;
    public TextMeshProUGUI navigationTravelTime_Text;

    [Header("Urgent Icons")]
    public GameObject urgentIcon_Navigation;
    public GameObject urgentIcon_Contruction;
    public GameObject urgentIcon_EventLog;
    public GameObject urgentIcon_Research;
    public GameObject urgentIcon_Storage;

    [Header("Planet Tabs")]
    public RARC_PlanetTab navigationPlanet1_Tab;
    public RARC_PlanetTab navigationPlanet2_Tab;
    public RARC_PlanetTab navigationPlanet3_Tab;


    [Header("Resources")]
    public TextMeshProUGUI resourcesFuel_Text;
    public TextMeshProUGUI resourcesFood_Text;
    public TextMeshProUGUI resourcesScrap_Text;


    [Header("Building Info")]
    public GameObject currentSelectedRoom;

    [Header("Weeks In Space")]
    public TextMeshProUGUI weeksAtSpace_Text;


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
        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination == null)
        {
            RARC_GameStateController.Instance.isReady_Navigation = false;
        }
        else
        {
            RARC_GameStateController.Instance.isReady_Navigation = true;
        }


        //Set Urgent Icons as Opposites of the current Ready State
        urgentIcon_Navigation.SetActive(!RARC_GameStateController.Instance.isReady_Navigation);
        urgentIcon_EventLog.SetActive(!RARC_GameStateController.Instance.isReady_EventLog);
        urgentIcon_Research.SetActive(!RARC_GameStateController.Instance.isReady_Research);
        urgentIcon_Contruction.SetActive(!RARC_GameStateController.Instance.isReady_Contruction);
        urgentIcon_Storage.SetActive(!RARC_GameStateController.Instance.isReady_Storage);
    }

    public void RefreshUI_Resources()
    {
        resourcesFuel_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount;
        resourcesFood_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount;
        resourcesScrap_Text.text = "x" + RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount;
    }

    /////////////////////////////////////////////////////////////////
}