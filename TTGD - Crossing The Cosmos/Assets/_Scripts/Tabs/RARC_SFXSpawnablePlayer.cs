using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_SFXSpawnablePlayer : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////

    public void PlaySFX_Button_Hover()
    {
        //Get Clip
        RARC_SFXController.Instance.PlayTrackSFX_NoLocation_SingleTrack(RARC_DatabaseController.Instance.sfx_DB.sfx_ButtonClick_Hover);
    }

    public void PlaySFX_Button_Crafting()
    {
        //Get Clip
        RARC_SFXController.Instance.PlayTrackSFX_NoLocation_SingleTrack(RARC_DatabaseController.Instance.sfx_DB.sfx_ButtonClick_Crafting);
    }

    /////////////////////////////////////////////////////////////////
}
