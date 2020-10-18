using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class RARC_RoomTab : MonoBehaviour
{
    ////////////////////////////////

    [Header("Room Sprite Renderer")]
    public SpriteRenderer roomSpriteRenderer;

    [Header("Room Information")]
    //public RARC_Room currentRoom;
    public RARC_Rooms_SO currentRoom_SO;

    [Header("Pathing Information on Room")]
    public List<GameObject> roomNodes_List;
    public int currentFloorLevel;

    /////////////////////////////////////////////////////////////////

    private void OnMouseEnter()
    {
        //Change Color Highlight
        roomSpriteRenderer.sprite = currentRoom_SO.activeRoomSprite;
    }

    private void OnMouseExit()
    {
        //Change Color Regular
        roomSpriteRenderer.sprite = currentRoom_SO.inactiveRoomSprite;
    }

    private void OnMouseUp()
    {

        if (currentRoom_SO.roomType == RARC_Room.RoomType.EMPTY)
        {
            //Open Build Room Menu
            RARC_ButtonController_Game.Instance.Button_Game_Build(this);
        }
        else
        {
            //Open Edit Room Panel

        }
    }

    /////////////////////////////////////////////////////////////////

    public void LoadRoom(RARC_Rooms_SO newRoom)
    {
        //Set Room Info
        //currentRoom = newRoom;

        //Update Sprite
        roomSpriteRenderer.sprite = currentRoom_SO.inactiveRoomSprite;
    }

    public void BuildRoom(RARC_Rooms_SO newRoom_SO)
    {
        //Set Room Info
        currentRoom_SO = newRoom_SO;

        //Update Sprite
        roomSpriteRenderer.sprite = currentRoom_SO.inactiveRoomSprite;

        //Update With Save Data?
        print("Test Code: Save Data?");
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
