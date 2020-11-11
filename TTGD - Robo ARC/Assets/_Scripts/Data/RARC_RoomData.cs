using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_RoomData : MonoBehaviour
{
    ////////////////////////////////

    [Header("Rooms")]
    public RARC_Rooms_SO EmptyRoom_SO;
    public RARC_Rooms_SO CrewQuartersRoom_SO;
    public RARC_Rooms_SO MedbayRoom_SO;
    public RARC_Rooms_SO FactoryRoom_SO;
    public RARC_Rooms_SO KitchenRoom_SO;
    public RARC_Rooms_SO StorageBayRoom_SO;
    public RARC_Rooms_SO CloningLabRoom_SO;
    public RARC_Rooms_SO HydroponicsLabRoom_SO;
    public RARC_Rooms_SO AstrometricsLabRoom_SO;

    public RARC_Rooms_SO FindRoomType(RARC_Room.RoomType roomType)
    {
        RARC_Rooms_SO newRoomSO = EmptyRoom_SO;

        switch (roomType)
        {
            case RARC_Room.RoomType.EMPTY:
                newRoomSO = EmptyRoom_SO;
                break;

            case RARC_Room.RoomType.QUARTERS:
                newRoomSO = CrewQuartersRoom_SO;
                break;

            case RARC_Room.RoomType.MEDBAY:
                newRoomSO = MedbayRoom_SO;
                break;

            case RARC_Room.RoomType.FACTORY:
                newRoomSO = FactoryRoom_SO;
                break;

            case RARC_Room.RoomType.KITCHEN:
                newRoomSO = KitchenRoom_SO;
                break;

            case RARC_Room.RoomType.STORAGE:
                newRoomSO = StorageBayRoom_SO;
                break;

            case RARC_Room.RoomType.CLONING:
                newRoomSO = CloningLabRoom_SO;
                break;

            case RARC_Room.RoomType.HYDROPONICS:
                newRoomSO = HydroponicsLabRoom_SO;
                break;

            case RARC_Room.RoomType.ASTROMETRICS:
                newRoomSO = AstrometricsLabRoom_SO;
                break;
        }

        return newRoomSO;
    }


    ////////////////////////////////
}
