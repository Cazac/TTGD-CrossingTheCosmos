using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_AnimationEventsTab : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////

    public void PlaySFX_NotifcationPing()
    {
        //Get Clip
        RARC_SFXController.Instance.PlayTrackSFX_NoLocation_SingleTrack(RARC_DatabaseController.Instance.sfx_DB.sfx_NotificationPing);
    }

    public void PlaySFX_CutsceneZoom()
    {
        //Get Clip
        RARC_SFXController.Instance.PlayTrackSFX_NoLocation_SingleTrack(RARC_DatabaseController.Instance.sfx_DB.sfx_CutsceneZoom);
    }

    public void PlaySFX_ShipZoom()
    {
        //Get Clip
        RARC_SFXController.Instance.PlayTrackSFX_NoLocation_SingleTrack(RARC_DatabaseController.Instance.sfx_DB.sfx_ShipZoom);
    }

    /////////////////////////////////////////////////////////////////
}
