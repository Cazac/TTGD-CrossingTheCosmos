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



    public RARC_RoomTab lastUsedRoom_Tab;
    public GameObject lastUsedRoomNode_Go;

    public int crewCurrentShipFloor;

    public GameObject crewPositionalGoal_GO;
    public GameObject crewFinalGoal_GO;


    public float currentTimeInState;


    [Header("BLANKVAR")]
    public readonly float minTimeInState_Idle = 3f;

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
        ChangeToState_Idling();
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

            case CrewState.Working:
                //PlayState_Working();
                break;

            case CrewState.Traveling:
                //PlayState_Traveling();
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
            if (Random.Range(0, 600) == 0)
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
            if (Vector3.Distance(crewPositionalGoal_GO.transform.position, gameObject.transform.position) <= 0.01f)
            {
                //Create New Goal on same level
                //crewPositionalGoal_GO = RARC_CrewBotsController.Instance.GetWanderingNodePosition(crewCurrentShipFloor);
                //Utility_SetFacingDirection();
                ChangeToState_Idling();
            }
        }



        //Check For Goal
        if (crewPositionalGoal_GO != null)
        {
            //Move Towards The Goal
            Vector3 goalPosition = crewPositionalGoal_GO.transform.position;
            Vector3 agentPosition = gameObject.transform.position;
            float speed = 0.6f * Time.deltaTime;

            //Move Agent
            gameObject.transform.position = Vector3.MoveTowards(agentPosition, goalPosition, speed);
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, goalPosition, 0.01f);
        }
        else
        {
            ChangeToState_Idling();
            //Create New Goal on same level Then Rotate
            //crewPositionalGoal_GO = RARC_CrewBotsController.Instance.GetWanderingNodePosition(crewCurrentShipFloor);
            //Utility_SetFacingDirection();
        }

        //Check For State Change
        if (Random.Range(0, 500) == 0)
        {
            //Change To Wandering
            //ChangeToState_Idling();
            //return;
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
        crew_Animator.SetBool("isWalking", false);

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
        crew_Animator.SetBool("isWalking", true);

        //Change To Wandering
        crewCurrent_State = CrewState.Wandering;
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
                crewRotation_GO.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                //Smaller Go Left
                crewRotation_GO.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    /////////////////////////////////////////////////////////////////
}
