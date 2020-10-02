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

    [Header("Random (Starting)")]
    public RARC_Event_SO event_AbandonedShip;



    [Header("Random (Aquired)")]
    public RARC_Event_SO event_333333aa3e;




    [Header("Gameover")]
    public RARC_Event_SO event_33333ff33e;

    [Header("Exploration (Random)")]
    public RARC_Event_SO event_33333fa33e;

    ////////////////////////////////
}
