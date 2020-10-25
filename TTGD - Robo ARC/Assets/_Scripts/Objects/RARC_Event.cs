using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_Event
{
    public string eventTitle;
    public string eventID;
    public string eventDescription;

    //Null is skip
    public string eventOption1_Choice;
    public string eventOption2_Choice;
    public string eventOption3_Choice;

    /////////////////////////////////////////////////////////////////

    public RARC_Event(RARC_Event_SO eventSO)
    {
        //Basic Info
        eventTitle = eventSO.eventTitle;
        eventID = eventSO.eventID;
        eventDescription = eventSO.eventDescription;

        //Basic Displayed Data
        eventOption1_Choice = eventSO.eventOption1_Choice;
        eventOption2_Choice = eventSO.eventOption2_Choice;
        eventOption3_Choice = eventSO.eventOption3_Choice;
    }

    public RARC_Event_SO GetEventSO()
    {
        //Searching for SO
        for (int i = 0; i < RARC_DatabaseController.Instance.events_DB.allEvents_List.Count; i++)
        {
            //Match Names
            if (RARC_DatabaseController.Instance.events_DB.allEvents_List[i].eventID == eventID)
            {
                //Return Event
                return RARC_DatabaseController.Instance.events_DB.allEvents_List[i];
            }
        }

        Debug.Log("Test Code: OOF No Matching Event SO");

        //Found Nothing Return Null
        return null;

        /*
        switch (eventID)
        {

            case "A New Hope - Event":
                
                break;



            case "Abandoned Ship - Event":
                eventSO = RARC_DatabaseController.Instance.events_DB.event_AbandonedShip;
                break;

            case "Abandoned Ship - Outcome Event 1":
                eventSO = RARC_DatabaseController.Instance.events_DB.event_AbandonedShip.eventOption1_Outcome.outcomeNextEvent;
                break;

            case "Abandoned Ship - Outcome Event 2":
                eventSO = RARC_DatabaseController.Instance.events_DB.event_AbandonedShip.eventOption2_Outcome.outcomeNextEvent;
                break;

            case "Abandoned Ship - Outcome Event 3":
                eventSO = RARC_DatabaseController.Instance.events_DB.event_AbandonedShip.eventOption3_Outcome.outcomeNextEvent;
                break;






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
                Debug.Log("Test Code: THIS IS AN ERROR, YOU DONE GOOFED");
                break;
        }
        */
    }

    /////////////////////////////////////////////////////////////////
}
