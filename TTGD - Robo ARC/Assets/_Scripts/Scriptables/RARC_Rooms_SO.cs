using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Scriptables/New Room")]
public class RARC_Rooms_SO : ScriptableObject
{
    ////////////////////////////////

    [Header("Room Type")]
    public RARC_Room.RoomType roomType;

    [Header("Room Name")]
    public string roomName;

    [Header("Room Limits")]
    public int roomLimit;

    [Header("Sprites")]
    public Sprite activeRoomSprite;
    public Sprite inactiveRoomSprite;

    ////////////////////////////////

    [Header("Room Requirements 1")]
    public RARC_Resource.ResourceType resourceRequired_1;
    public int resourceRequiredAmount_1;

    [Header("Room Requirements 2")]
    public RARC_Resource.ResourceType resourceRequired_2;
    public int resourceRequiredAmount_2;

    [Header("Room Requirements 3")]
    public RARC_Resource.ResourceType resourceRequired_3;
    public int resourceRequiredAmount_3;

    [Header("Room Requirements 4")]
    public RARC_Resource.ResourceType resourceRequired_4;
    public int resourceRequiredAmount_4;

    [Header("Room Requirements 5")]
    public RARC_Resource.ResourceType resourceRequired_5;
    public int resourceRequiredAmount_5;

    ////////////////////////////////
}
