using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RARC_RoomTab : MonoBehaviour
{
    ////////////////////////////////


    public RARC_ElevatorTab currentFloorLevel_ElevatorTab;


    public RARC_Room currentRoom;

    public int currentFloorLevel;


    public List<GameObject> roomNodes_List;

    /////////////////////////////////////////////////////////////////

    public void SetRoom(RARC_Room newRoom)
    {

    }

    public GameObject GetRandomNode(Vector3 currentMemberPosition)
    {
        //Get Random Node and Return it
        List<GameObject> filteredNodes_List = roomNodes_List.Where(x => Vector3.Distance(x.transform.position, currentMemberPosition) >= 0.5f).ToList();
        int randomValue = Random.Range(0, filteredNodes_List.Count);
        GameObject roomNode = filteredNodes_List[randomValue];
        return roomNode;
    }

    /////////////////////////////////////////////////////////////////
}
