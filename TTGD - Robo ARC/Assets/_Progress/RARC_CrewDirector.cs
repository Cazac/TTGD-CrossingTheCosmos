using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_CrewDirector : MonoBehaviour
{


    [Header("Animator")]
    public Animator crew_Animator;

    [Header("Current State")]
    public CrewState crewCurrent_State;

    [Header("Crew Info")]
    public RARC_Crew crewInfo;


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
        crewCurrent_State = CrewState.Idling;
    }


    private void Update()
    {
        GetCurrentState();
    }

    /////////////////////////////////////////////////////////////////

    private void GetCurrentState()
    {
        print("Test Code: Crew State: " + crewCurrent_State);

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
        //Check For State Change
        if (Random.Range(0, 100) == 0)
        {
            //Change To Wandering
            ChangeToState_Wandering();

            return;
        }
    }


    private void PlayState_Wandering()
    {
        
    }

    /////////////////////////////////////////////////////////////////

    private void ChangeToState_Wandering()
    {
        //Set Goal Spot on same level
        //enemy_NavAgent.SetDestination(currentTargetLocation);

        //Set Animator
        crew_Animator.SetBool("Walk", true);


        //Change To Wandering
        crewCurrent_State = CrewState.Wandering;
    }

    /////////////////////////////////////////////////////////////////
}
