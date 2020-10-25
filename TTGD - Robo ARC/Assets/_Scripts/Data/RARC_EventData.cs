using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_EventData : MonoBehaviour
{
    ////////////////////////////////

    [Header("Intro")]
    public RARC_Event_SO event_ANewHope;

    [Header("Gameover")]
    public RARC_Event_SO event_TheEndIsNear_EmptyTank;
    public RARC_Event_SO event_TheEndIsNear_Starvation;
    public RARC_Event_SO event_TheEndIsNear_EveryoneIsGone;
    public RARC_Event_SO event_TheEndIsNear_CatastrophicBreakdown;

    ////////////////////////////////

    [Header("Travel Starting")]
    public RARC_Event_SO event_AbandonedShip;
    public RARC_Event_SO event_AliensOne;
    public RARC_Event_SO event_Bedtime;
    public RARC_Event_SO event_BigRock;
    public RARC_Event_SO event_Freedom;
    public RARC_Event_SO event_FriendlyGreeting;
    public RARC_Event_SO event_PartyTime;
    public RARC_Event_SO event_RottenFood;
    public RARC_Event_SO event_UnknownSignalOne;
    public RARC_Event_SO event_Zoom;

    [Header("Travel Aquired")]
    public RARC_Event_SO event_AliensThree;
    public RARC_Event_SO event_AliensTwo;
    public RARC_Event_SO event_Mutiny;
    public RARC_Event_SO event_UnknownSignalThree;
    public RARC_Event_SO event_UnknownSignalTwo;

    ////////////////////////////////

    [Header("Planet Starting")]
    public RARC_Event_SO event_AncientRuins;
    public RARC_Event_SO event_CrackInTheSurface;
    public RARC_Event_SO event_CreaturesOnTheGround;

    ////////////////////////////////

    [Header("All Events")]
    public List<RARC_Event_SO> allEvents_List;

    /////////////////////////////////////////////////////////////////

    public void BuildDatabase()
    {
        //Setup New List of All Things
        allEvents_List = new List<RARC_Event_SO>();

        //Intro
        CheckForNulls(event_ANewHope);

        //Gameover
        CheckForNulls(event_TheEndIsNear_EmptyTank);
        CheckForNulls(event_TheEndIsNear_Starvation);
        CheckForNulls(event_TheEndIsNear_EveryoneIsGone);
        CheckForNulls(event_TheEndIsNear_CatastrophicBreakdown);
    
        //Travel Starting
        CheckForNulls(event_AbandonedShip);
        CheckForNulls(event_AliensOne);
        CheckForNulls(event_Bedtime);
        CheckForNulls(event_BigRock);
        CheckForNulls(event_Freedom);
        CheckForNulls(event_FriendlyGreeting);
        CheckForNulls(event_PartyTime);
        CheckForNulls(event_RottenFood);
        CheckForNulls(event_UnknownSignalOne);
        CheckForNulls(event_Zoom);

        //Travel Aquired
        CheckForNulls(event_AliensThree);
        CheckForNulls(event_AliensTwo);
        CheckForNulls(event_Mutiny);
        CheckForNulls(event_UnknownSignalThree);
        CheckForNulls(event_UnknownSignalTwo);

        //Planet Starting
        CheckForNulls(event_AncientRuins);
        CheckForNulls(event_CrackInTheSurface);
        CheckForNulls(event_CreaturesOnTheGround);

        print("Test Code: Total Size Of Events List " + allEvents_List.Count);
    }

    /////////////////////////////////////////////////////////////////

    public void CheckForNulls(RARC_Event_SO eventSO)
    {
        if (eventSO != null)
        {
            allEvents_List.Add(eventSO);
            CheckForOutcomes(eventSO);
        }
    }

    public void CheckForOutcomes(RARC_Event_SO eventSO)
    {
        if (eventSO.eventOption1_Outcome != null)
        {
            if (eventSO.eventOption1_Outcome.outcomeNextEvent != null)
            {
                allEvents_List.Add(eventSO.eventOption1_Outcome.outcomeNextEvent);
            }
        }

        if (eventSO.eventOption2_Outcome != null)
        {
            if (eventSO.eventOption2_Outcome.outcomeNextEvent != null)
            {
                allEvents_List.Add(eventSO.eventOption2_Outcome.outcomeNextEvent);
            }
        }

        if (eventSO.eventOption3_Outcome != null)
        {
            if (eventSO.eventOption3_Outcome.outcomeNextEvent != null)
            {
                allEvents_List.Add(eventSO.eventOption3_Outcome.outcomeNextEvent);
            }
        }
    }

    /////////////////////////////////////////////////////////////////
}
