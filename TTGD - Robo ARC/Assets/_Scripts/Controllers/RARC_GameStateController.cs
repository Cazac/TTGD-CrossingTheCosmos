using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_GameStateController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_GameStateController Instance;

    ////////////////////////////////

    public bool isReady_Launch;


    public bool isReady_Navigation;
    public bool isReady_EventLog;
    public bool isReady_Research;
    public bool isReady_Contruction;
    public bool isReady_Storage;


    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    private void Start()
    {
        //Load Week UI from data


        //Set UI For Week
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
    }

    /////////////////////////////////////////////////////////////////

    public void System_GenerateNewWeek()
    {
        //Generate Events

        //Progress Time



        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination == null)
        {

        }
        else
        {

        }
    }

    /////////////////////////////////////////////////////////////////

    public void Player_StartWeek()
    {
        //Add Interatablity



    }

    public void Player_FinishWeek()
    {
        //Remove Interatablity

        //Play ANimaitons
    }

    /////////////////////////////////////////////////////////////////
}
