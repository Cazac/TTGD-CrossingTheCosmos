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

    private void Start()
    {
        //AddNewCrew();
        RespawnAllCrewAndBots();
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



        //Spawn Crew
        foreach (RARC_Crew child in RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List)
        {
            SpawnCrewInRoom(RARC_RoomsController.Instance.roomsInShip_List[0], child);
        }

        foreach (RARC_Crew child in RARC_DatabaseController.Instance.ship_SaveData.shipData_Bots_List)
        {
            //GameObject botMember_GO = Instantiate(bot_Prefab);
        }
    }




    /////////////////////////////////////////////////////////////////

    public void AddNewBot()
    {

    }

    public void AddNewCrew()
    {
        RARC_Crew newCrewMember = new RARC_Crew();
        RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List.Add(newCrewMember);
    }

    /////////////////////////////////////////////////////////////////

    public void SpawnBotInRoom()
    {

    }


    public void SpawnCrewInRoom(RARC_RoomTab roomTab, RARC_Crew newCrewMember)
    {
        Vector3 spawnPoint = roomTab.GetRandomNode(new Vector3(0,0,0)).transform.position;


        GameObject newCrewMember_GO = Instantiate(crew_Prefab, crew_Container.transform);
        newCrewMember_GO.transform.position = spawnPoint;

        //Set Crew Info For Current Level
        newCrewMember_GO.GetComponent<RARC_CrewAgent>().crewCurrentShipFloor = roomTab.currentFloorLevel;
    }

    public GameObject GetWanderingNodePosition(int shipFloorLevel, Vector3 currentMemberPosition)
    {

        List<RARC_RoomTab> roomsOnFloorLevel_List = RARC_RoomsController.Instance.GetRoomsOnFloorLevel(shipFloorLevel);

        RARC_RoomTab selectedRoom = roomsOnFloorLevel_List[Random.Range(0, roomsOnFloorLevel_List.Count)];

        return selectedRoom.GetRandomNode(currentMemberPosition);
    }

    /////////////////////////////////////////////////////////////////
}
