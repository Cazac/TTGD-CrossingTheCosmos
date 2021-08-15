using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_CrewAgent : MonoBehaviour
{
    ////////////////////////////////

    [Header("Animator")]
    public Animator crew_Animator;
    public GameObject crewRotation_GO;

    [Header("Current State")]
    public CrewState crewCurrent_State;

    [Header("Crew Info")]
    public RARC_Crew crewInfo;

    ////////////////////////////////

    [Header("Crew Main Animation Containers")]
    public GameObject crewForwardContainer_GO;
    public GameObject crewBackwardContainer_GO;

    [Header("Crew Tracking")]
    public GameObject crewPositionalGoal_GO;
    public RARC_RoomTab currentWorkingRoom;
    public int crewCurrentShipFloor;
    
    [Header("Elevator Info")]
    public bool isGoingInElevator;
    public bool isGoingUp;
    public bool isGoingDown;

    ////////////////////////////////

    [Header("Speeds")]
    public readonly float crewSpeed_Wandering = 0.6f;
    public readonly float crewSpeed_Traveling = 0.6f;

    [Header("Min Time In State")]
    public float currentTimeInState;
    public readonly float minTimeInState_Idle = 3f;
    public readonly int RandomChanceToChangeState_Idle = 500;
    public readonly float minTimeInState_Working = 4f;
    public readonly int RandomChanceToChangeState_Working = 500;


    ////////////////////////////////

    public enum CrewState
    {
        Idling,
        Wandering,
        Traveling,
        Working,
        Dying
    }

    /////////////////////////////////////////////////////////////////

    private void Start()
    {
        ChangeToState_Wandering();
    }

    private void Update()
    {
        GetCurrentState();
    }

    /////////////////////////////////////////////////////////////////

    private void GetCurrentState()
    {
        //print("Test Code: Crew State: " + crewCurrent_State);

        currentTimeInState += Time.deltaTime;

        //Check Current State
        switch (crewCurrent_State)
        {
            case CrewState.Idling:
                PlayState_Idling();
                break;

            case CrewState.Wandering:
                PlayState_Wandering();
                break;

            case CrewState.Traveling:
                PlayState_Traveling();
                break;

            case CrewState.Working:
                PlayState_Working();
                break;

            case CrewState.Dying:
                //PlayState_Dying();
                break;
        }
    }

    /////////////////////////////////////////////////////////////////

    private void PlayState_Idling()
    {
        //Wait For Min Time in State
        if (currentTimeInState >= minTimeInState_Idle)
        {
            //Check For Random State Change
            if (Random.Range(0, RandomChanceToChangeState_Idle) == 0)
            {
                //Change To Wandering
                ChangeToState_Wandering();
                return;
            }
        }
    }

    private void PlayState_Wandering()
    {
        //Check For Goal
        if (crewPositionalGoal_GO != null)
        {
            //Check If distance is close enough
            if (Vector3.Distance(crewPositionalGoal_GO.transform.position, gameObject.transform.position) <= 0.01f)
            {
                //Reset To Idle
                ChangeToState_Idling();
                return;
            }

            //Move Towards The Goal
            Vector3 goalPosition = crewPositionalGoal_GO.transform.position;
            Vector3 agentPosition = gameObject.transform.position;
            float speed = crewSpeed_Wandering * Time.deltaTime;

            //Move Agent
            gameObject.transform.position = Vector3.MoveTowards(agentPosition, goalPosition, speed);
        }
        else
        {
            //Reset To Idle
            ChangeToState_Idling();
            return;
        }
    }

    private void PlayState_Traveling()
    {
        //Check For Goal
        if (crewPositionalGoal_GO != null)
        {
            //Check If distance is close enough
            if (Vector3.Distance(crewPositionalGoal_GO.transform.position, gameObject.transform.position) <= 0.01f)
            {
                //Check For Room or Elevator Status
                if (isGoingInElevator)
                {
                    //print("Test Code: Floor Before " + crewCurrentShipFloor);

                    if (isGoingUp)
                    {
                        crewCurrentShipFloor++;
                        isGoingUp = false;
                        isGoingDown = false;
                    }
                    else if (isGoingDown)
                    {
                        crewCurrentShipFloor--;
                        isGoingUp = false;
                        isGoingDown = false;
                    }
                    //print("Test Code: Floor After " + crewCurrentShipFloor);

                    //In Elavator Check if Correct Floor
                    if (currentWorkingRoom.currentFloorLevel != crewCurrentShipFloor)
                    {
                        //print("Test Code: Wrong Floor go to next floor");
                        //Wrong Floor go to next floor
                        crewPositionalGoal_GO = RARC_CrewBotsController.Instance.GetNextElevatorPosition(crewCurrentShipFloor, currentWorkingRoom.currentFloorLevel);
                        Utility_SetFacingDirection();
                        Utlity_CalculateDirectionOfElevator();
                        isGoingInElevator = true;
                        return;
                    }
                    else
                    {
                        //print("Test Code: Correct Floor Now find the room");
                        //Correct Floor Now find the room
                        crewPositionalGoal_GO = RARC_CrewBotsController.Instance.GetTravelingPosition(currentWorkingRoom, gameObject.transform.position);
                        Utility_SetFacingDirection();
                        isGoingInElevator = false;
                        return;
                    }
                }
                else
                {
                    if (currentWorkingRoom.currentFloorLevel != crewCurrentShipFloor)
                    {
                        print("Test Code: Call This?");
                        crewPositionalGoal_GO = RARC_CrewBotsController.Instance.GetNextElevatorPosition(crewCurrentShipFloor, currentWorkingRoom.currentFloorLevel);
                        Utility_SetFacingDirection();
                        Utlity_CalculateDirectionOfElevator();
                        isGoingInElevator = true;
                        return;
                    }
                    else
                    {
                        //Found the spot start working
                        ChangeToState_Working();
                        return;
                    }
                }
            }

            //Move Towards The Goal
            Vector3 goalPosition = crewPositionalGoal_GO.transform.position;
            Vector3 agentPosition = gameObject.transform.position;
            float speed = crewSpeed_Traveling * Time.deltaTime;

            //Move Agent
            gameObject.transform.position = Vector3.MoveTowards(agentPosition, goalPosition, speed);
        } 
    }

    private void PlayState_Working()
    {
        //Wait For Min Time in State
        if (currentTimeInState >= minTimeInState_Working)
        {
            //Check For Random State Change
            if (Random.Range(0, RandomChanceToChangeState_Working) == 0)
            {
                //Change To Traveling
                ChangeToState_Traveling(RARC_RoomsController.Instance.roomsInShip_List[Random.Range(0, RARC_RoomsController.Instance.roomsInShip_List.Count)]);
                return;
            }
        }
    }

    /////////////////////////////////////////////////////////////////

    private void ChangeToState_Idling()
    {
        //Reset Timer
        currentTimeInState = 0;

        //Set Goal Spot on same level
        crewPositionalGoal_GO = null;

        //Set Animator
        Utility_SetAnimation_Idle();

        //Change To Wandering
        crewCurrent_State = CrewState.Idling;
    }

    private void ChangeToState_Wandering()
    {
        //Reset Timer
        currentTimeInState = 0;

        //Set Goal Spot on same level
        crewPositionalGoal_GO = RARC_CrewBotsController.Instance.GetWanderingNodePosition(crewCurrentShipFloor, gameObject.transform.position);
        Utility_SetFacingDirection();

        //Set Animator
        Utility_SetAnimation_Walk();

        //Change To Wandering
        crewCurrent_State = CrewState.Wandering;
    }

    private void ChangeToState_Traveling(RARC_RoomTab roomToWorkIn)
    {
        //Reset Timer
        currentTimeInState = 0;

        //Set Working Room
        currentWorkingRoom = roomToWorkIn;

        //Set Goal Spot
        if (currentWorkingRoom.currentFloorLevel == crewCurrentShipFloor)
        {
            crewPositionalGoal_GO = RARC_CrewBotsController.Instance.GetTravelingPosition(currentWorkingRoom, gameObject.transform.position);
            Utility_SetFacingDirection();
            isGoingInElevator = false;
        }
        else
        {
            crewPositionalGoal_GO = RARC_CrewBotsController.Instance.GetClosestElevatorPosition(crewCurrentShipFloor);
            Utility_SetFacingDirection();
            //Utlity_CalculateDirectionOfElevator();
            isGoingInElevator = true;
        }

        //Set Animator
        Utility_SetAnimation_Walk();

        //Change To Wandering
        crewCurrent_State = CrewState.Traveling;
    }

    private void ChangeToState_Working()
    {
        //Set Animator
        Utility_SetAnimation_Work();

        //Change To Wandering
        crewCurrent_State = CrewState.Working;
    }

    /////////////////////////////////////////////////////////////////

    private void Utility_SetFacingDirection()
        {
            //Check For Goal
            if (crewPositionalGoal_GO != null)
            {
                float xAxisChange = gameObject.transform.position.x - crewPositionalGoal_GO.transform.position.x;
                if (xAxisChange >= 0)
                {
                    //Bigger Go Right
                    crewRotation_GO.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    //Smaller Go Left
                    crewRotation_GO.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
            }
        }

    private void Utlity_CalculateDirectionOfElevator()
    {
        //Reset Values
        isGoingUp = false;
        isGoingDown = false;

        if (currentWorkingRoom.GetRandomNode(gameObject.transform.position).transform.position.y > gameObject.transform.position.y)
        {
            //print("Test Code: Going Up");
            isGoingUp = true;
        }
        else if (currentWorkingRoom.GetRandomNode(gameObject.transform.position).transform.position.y < gameObject.transform.position.y)
        {
            //print("Test Code: Going Down");
            isGoingDown = true;
        }
    }

    private void Utility_SetAnimation_Idle()
    {
        crewBackwardContainer_GO.SetActive(false);
        crewForwardContainer_GO.SetActive(true);
        crew_Animator.SetBool("isWalking", false);
    }

    private void Utility_SetAnimation_Walk()
    {
        crewBackwardContainer_GO.SetActive(false);
        crewForwardContainer_GO.SetActive(true);
        crew_Animator.SetBool("isWalking", true);
    }

    private void Utility_SetAnimation_Work()
    {
        crewForwardContainer_GO.SetActive(false);
        crewBackwardContainer_GO.SetActive(true);
    }

    /////////////////////////////////////////////////////////////////
}
