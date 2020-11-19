using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_Room
{
    ////////////////////////////////

    public enum RoomType
    {
        EMPTY,

        ASTROMETRICS,
        CLONING,
        FACTORY,
        HYDROPONICS,
        KITCHEN,
        MEDBAY,
        QUARTERS,
        STORAGE
    }

    ////////////////////////////////

    public RoomType currentRoomType = RoomType.EMPTY;

    public RARC_Room(RoomType type)
    {
        currentRoomType = type;
    }

    /////////////////////////////////////////////////////////////////
}
