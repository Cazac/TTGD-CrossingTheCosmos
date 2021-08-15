using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_CutsceneController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_CutsceneController Instance;

    ///////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            AttemptCutsceneSkip();
        }
    }

    ///////////////////////////////////////////////////////s

    public void AttemptCutsceneSkip()
    {
        if (RARC_GameStateController.Instance.currentCutscene_IEnum != null)
        {
            StopCoroutine(RARC_GameStateController.Instance.currentCutscene_IEnum);
            StartCoroutine(RARC_GameStateController.Instance.Player_EndCutscene());
        }
       
    }

    ///////////////////////////////////////////////////////
}
