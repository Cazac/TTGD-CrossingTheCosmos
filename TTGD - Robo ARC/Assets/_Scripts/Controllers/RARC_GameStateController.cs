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

    public string cursorState;


    public List<RARC_Planet> navigationPossiblePlanets_List;


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

        //set cursor state
        cursorState = "free";
    }

    /////////////////////////////////////////////////////////////////

    public void System_GenerateNewWeek()
    {
        //Progress Time
        RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived++;
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();

        //Generate Events




        //Reset Navigation Planets
        navigationPossiblePlanets_List.Clear();
        //navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
        //navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
        //navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());









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
