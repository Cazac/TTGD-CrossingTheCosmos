using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_Event
{
    public string eventTitle;
    public string eventDescription;

    //Null is skip
    public string eventOption1_Choice;
    //public RARC_EventOutcome_SO eventOption1_Outcome;
    //public RARC_EventRequirement_SO eventOption1_Requirement;


    public string eventOption2_Choice;
   // public RARC_EventOutcome_SO eventOption2_Outcome;
    //public RARC_EventRequirement_SO eventOption2_Requirement;


    public string eventOption3_Choice;
    //public RARC_EventOutcome_SO eventOption3_Outcome;
    //public RARC_EventRequirement_SO eventOption3_Requirement;

    /////////////////////////////////////////////////////////////////

    public RARC_Event(RARC_Event_SO eventSO)
    {
        //Basic Info
        eventTitle = eventSO.eventTitle;
        eventDescription = eventSO.eventDescription;

        //Basic Displayed Data
        eventOption1_Choice = eventSO.eventOption1_Choice;
        eventOption2_Choice = eventSO.eventOption2_Choice;
        eventOption3_Choice = eventSO.eventOption3_Choice;
    }

    public RARC_Event_SO GetEventSO()
    {
        RARC_Event_SO eventSO = null;

        switch (eventTitle)
        {
            case "The End is Near (Catastrophic Breakdown))":
                eventSO = RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_CatastrophicBreakdown;
                break;

            case "The End is Near (Empty Tank)":
                eventSO = RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_EmptyTank;
                break;

            case "The End is Near (Everyone is Gone)":
                eventSO = RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_EveryoneIsGone;
                break;

            case "The End is Near (Starvation)":
                eventSO = RARC_DatabaseController.Instance.events_DB.event_TheEndIsNear_Starvation;
                break;

            default:
                eventSO = RARC_DatabaseController.Instance.events_DB.event_ANewHope;
                break;

        }



        return eventSO;
    }

    /////////////////////////////////////////////////////////////////
}
