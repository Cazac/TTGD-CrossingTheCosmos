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
    }

    /////////////////////////////////////////////////////////////////
}
