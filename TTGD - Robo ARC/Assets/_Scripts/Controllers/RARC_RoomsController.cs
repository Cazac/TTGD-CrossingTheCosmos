using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RARC_RoomsController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_RoomsController Instance;

    ////////////////////////////////

    [Header("BLANKVAR")]
    public List<RARC_RoomTab> roomsInShip_List;



    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    public List<RARC_RoomTab> GetRoomsOnFloorLevel(int floorLevel)
    {
       return roomsInShip_List.Where(x => x.currentFloorLevel == floorLevel).ToList();
    }

    public void GetElevatorOnFloorLevel()
    {

    }

    /////////////////////////////////////////////////////////////////
}
