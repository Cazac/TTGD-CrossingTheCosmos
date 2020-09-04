using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_Room : MonoBehaviour
{
    public enum RoomType
    {
        NONE,
        RESEARCH, MEDICAL, FOOD, RECREATION, FACTORY, STORAGE
    }

    public RoomType currentRoomType;

    /////////////////////////////////////////////////////////////////

    private void Start()
    {
        currentRoomType = RoomType.NONE;

    }

    private void OnMouseDown()
    {
        if (RARC_GameStateController.Instance.currentCursorState == RARC_GameStateController.CursorState.NORMAL)
        {
            switch (currentRoomType)
            {
                case RoomType.NONE:
                    RARC_ButtonController_Game.Instance.ConstructionMenu_Main.SetActive(true);
                    RARC_ButtonController_Game.Instance.currentSelectedRoom = gameObject;
                    break;
                case RoomType.RESEARCH:
                    //research_menu.SetActive(true);
                    break;
                case RoomType.MEDICAL:
                    //medical_menu.SetActive(true);
                    break;
                case RoomType.FOOD:
                    //farm_menu.SetActive(true);
                    break;
                case RoomType.RECREATION:
                    //sanity_menu.SetActive(true);
                    break;
                case RoomType.FACTORY:
                    //factory_menu.SetActive(true);
                    break;
                case RoomType.STORAGE:
                    //storage_menu.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (RARC_GameStateController.Instance.currentCursorState)
            {
                case RARC_GameStateController.CursorState.BUILD_RESEARCH:
                    //subtract cost
                    //change room image
                    currentRoomType = RoomType.RESEARCH;
                    break;

                case RARC_GameStateController.CursorState.BUILD_MEDICAL:
                    //subtract cost
                    //change room image
                    currentRoomType = RoomType.MEDICAL;
                    break;

                case RARC_GameStateController.CursorState.BUILD_FOOD:
                    //subtract cost
                    //change room image
                    currentRoomType = RoomType.FOOD;
                    break;

                case RARC_GameStateController.CursorState.BUILD_RECREATION:
                    //subtract cost
                    //change room image
                    currentRoomType = RoomType.RECREATION;
                    break;

                case RARC_GameStateController.CursorState.BUILD_FACTORY:
                    //subtract cost
                    //change room image
                    currentRoomType = RoomType.FACTORY;
                    break;

                case RARC_GameStateController.CursorState.BUILD_STORAGE:
                    //subtract cost
                    //change room image
                    currentRoomType = RoomType.STORAGE;
                    break;

                default:
                    break;
            }
            RARC_GameStateController.Instance.currentCursorState = RARC_GameStateController.CursorState.NORMAL;
        }

    }//OnMouseDown end

}
