using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Event", menuName = "Scriptables/New Event")]
public class RARC_Event_SO : ScriptableObject
{
    ////////////////////////////////

    [Header("All Planets")]
    public string eventTitle;
    [TextArea()]
    public string eventDescription;

    public string eventSpriteName;

    //Null is skip
    [Header("Option 1 (Null = No Choice)")]
    [TextArea()]
    public string eventOption1_Choice;
    public RARC_Planet_SO eventOption1_Outcome;
    public RARC_Planet_SO eventOption1_Requirement;

    [Header("Option 2 (Null = No Choice)")]
    [TextArea()]
    public string eventOption2_Choice;
    public RARC_Planet_SO eventOption2_Outcome;
    public RARC_Planet_SO eventOption2_Requirement;

    [Header("Option 3 (Null = No Choice)")]
    [TextArea()]
    public string eventOption3_Choice;
    public RARC_Planet_SO eventOption3_Outcome;
    public RARC_Planet_SO eventOption3_Requirement;

    /////////////////////////////////////////////////////////////////
}
