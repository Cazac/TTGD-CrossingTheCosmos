using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_CrewBotsController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_CrewBotsController Instance;

    ////////////////////////////////

    [Header("BLANKVAR")]
    public GameObject crew_Prefab;
    public GameObject bot_Prefab;

    [Header("BLANKVAR")]
    public GameObject crew_Container;
    public GameObject bot_Container;

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    /////////////////////////////////////////////////////////////////

    public void RespawnAllCrewAndBots()
    {
        foreach (Transform child in crew_Container.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in bot_Container.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (RARC_Crew child in RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List)
        {
            GameObject crewMember_GO = Instantiate(crew_Prefab);
        }

        foreach (RARC_Crew child in RARC_DatabaseController.Instance.ship_SaveData.shipData_Bots_List)
        {
            GameObject botMember_GO = Instantiate(bot_Prefab);
        }




    }

    public void SpawnBot()
    {

    }

    public void SpawnCrew()
    {

    }


    /////////////////////////////////////////////////////////////////
}
