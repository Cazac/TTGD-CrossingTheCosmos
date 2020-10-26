using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting", menuName = "Scriptables/New Crafting")]
public class RARC_Crafting_SO : ScriptableObject
{
    ////////////////////////////////

    [Header("Sprite")]
    public Sprite craftingSprite;

    [Header("Aquired Items")]
    public RARC_Resource.ResourceType resourceType;
    public int resourcePerCraft;
    public int crewPerCraft;
    public int botsPerCraft;

    [Header("Room Info")]
    public RARC_Room.RoomType roomRequired;
    public int resourceCraftsPerRoom;

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
