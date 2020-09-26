using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RARC_RoomsController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_RoomsController Instance;

    ////////////////////////////////

    [Header("Rooms")]
    public List<RARC_RoomTab> roomsInShip_List;


    [Header("Elevator")]
    public RARC_ElevatorTab elevatorFloor4_Tab;
    public RARC_ElevatorTab elevatorFloor3_Tab;
    public RARC_ElevatorTab elevatorFloor2_Tab;
    public RARC_ElevatorTab elevatorFloor1_Tab;

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    /////////////////////////////////////////////////////////////////

    public List<RARC_RoomTab> GetRoomsOnFloorLevel(int floorLevel)
    {
        //Return the rooms on this floor
        return roomsInShip_List.Where(x => x.currentFloorLevel == floorLevel).ToList();
    }

    public RARC_ElevatorTab GetElevatorCurrentFloorLevel(int currentFloorLevel)
    {
        RARC_ElevatorTab newElebatorTab = null;

        switch (currentFloorLevel)
        {
            case 4:
                newElebatorTab = elevatorFloor4_Tab;
                break;

            case 3:
                newElebatorTab = elevatorFloor3_Tab;
                break;

            case 2:
                newElebatorTab = elevatorFloor2_Tab;
                break;

            case 1:
                newElebatorTab = elevatorFloor1_Tab;
                break;
        }

        return newElebatorTab;
    }

    public RARC_ElevatorTab GetNextElevatorFloorLevel(int currentFloorLevel, int goalFloorLevel)
    {
        RARC_ElevatorTab newElebatorTab = null;

        switch (currentFloorLevel)
        {
            case 4:
                newElebatorTab = elevatorFloor3_Tab;
                break;

            case 3:
                if (goalFloorLevel > 3)
                {
                    newElebatorTab = elevatorFloor4_Tab;
                }
                else
                {
                    newElebatorTab = elevatorFloor2_Tab;
                }
                break;

            case 2:
                if (goalFloorLevel > 2)
                {
                    newElebatorTab = elevatorFloor3_Tab;
                }
                else
                {
                    newElebatorTab = elevatorFloor1_Tab;
                }
                break;

            case 1:
                newElebatorTab = elevatorFloor2_Tab;
                break;
        }

        return newElebatorTab;
    }

    /////////////////////////////////////////////////////////////////
}
