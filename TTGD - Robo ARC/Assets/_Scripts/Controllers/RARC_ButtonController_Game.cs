using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RARC_ButtonController_Game : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_ButtonController_Game Instance;

    ////////////////////////////////

    public GameObject currentSelectedRoom;

    [Header("Menu Panels Icons")]
    public GameObject NavigationPanel_Main;
    public GameObject ConstructionMenu_Main;

    public TextMeshProUGUI navigationDesination_Text;
    public TextMeshProUGUI navigationTravelTime_Text;

    [Header("Urgent Icons")]
    public GameObject urgentIcon_Navigation;
    public GameObject urgentIcon_EventLog;
    public GameObject urgentIcon_Research;
    public GameObject urgentIcon_Contruction;
    public GameObject urgentIcon_Storage;


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
        if (RARC_GameStateController.Instance.isReady_Launch)
        {
            //Finish Week and Animation
            RARC_GameStateController.Instance.Player_FinishWeek();

            //Create a New Week
            RARC_GameStateController.Instance.System_GenerateNewWeek();

            //Save Next Weeks Data
            RARC_DatabaseController.Instance.SaveShipData();
        }
        else
        {
            print("Test Code: Oh No! Not Ready!");
        }
    }

    public void Button_GameNavigate()
    {
        //Open Navigation Menu
        NavigationPanel_Main.SetActive(true);

        //Load Planet UI




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

    public void Button_MainSettings()
    {
        //Open Navigation Menu
        NavigationPanel_Main.SetActive(false);
    }



    /////////////////////////////////////////////////////////////////

    public void Button_PauseQuit()
    {
       
    }

    /////////////////////////////////////////////////////////////////

    public void RefreshUI_WeeksInSpace()
    {
        weeksAtSpace_Text.text = "Week in Space: " + RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived + "/52";
    }

    public void RefreshUI_UrgentIcons()
    {
        //Set Urgent Icons as Opposites of the current Ready State
        urgentIcon_Navigation.SetActive(!RARC_GameStateController.Instance.isReady_Navigation);
        urgentIcon_EventLog.SetActive(!RARC_GameStateController.Instance.isReady_EventLog);
        urgentIcon_Research.SetActive(!RARC_GameStateController.Instance.isReady_Research);
        urgentIcon_Contruction.SetActive(!RARC_GameStateController.Instance.isReady_Contruction);
        urgentIcon_Storage.SetActive(!RARC_GameStateController.Instance.isReady_Storage);
    }

    /////////////////////////////////////////////////////////////////
}
