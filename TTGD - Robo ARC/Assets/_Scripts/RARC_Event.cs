using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RARC_Event
{
    public string eventTitle;
    public string eventDescription;
    public string eventSpriteName;

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
        eventTitle = eventSO.eventTitle;
        eventDescription = eventSO.eventDescription;
        eventSpriteName = eventSO.eventSpriteName;
        eventOption1_Choice = eventSO.eventOption1_Choice;
       // eventOption1_Outcome = eventSO.eventOption1_Outcome;
        //eventOption1_Requirement = eventSO.eventOption1_Requirement;
        eventOption2_Choice = eventSO.eventOption2_Choice;
       // eventOption2_Outcome = eventSO.eventOption2_Outcome;
        //eventOption2_Requirement = eventSO.eventOption2_Requirement;
        eventOption3_Choice = eventSO.eventOption3_Choice;
        //eventOption3_Outcome = eventSO.eventOption3_Outcome;
        //eventOption3_Requirement = eventSO.eventOption3_Requirement;
    }

    /////////////////////////////////////////////////////////////////
}
