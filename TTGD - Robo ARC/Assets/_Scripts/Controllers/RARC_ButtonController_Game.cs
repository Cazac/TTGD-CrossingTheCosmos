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
    public GameObject NavigationPanel_Main;

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
    }

    /////////////////////////////////////////////////////////////////

    public void Button_GameLaunch()
    {
        //Add a new week
        RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived++;
        RefreshUI_WeeksInSpace();

        RARC_DatabaseController.Instance.SaveShipData();


    }

    public void Button_GameNavigate()
    {
        //Open Navigation Menu
        NavigationPanel_Main.SetActive(true);

        //Load Planet UI




    }

    public void Button_MainSettings()
    {

    }

    public void Button_MainCredits()
    {

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
