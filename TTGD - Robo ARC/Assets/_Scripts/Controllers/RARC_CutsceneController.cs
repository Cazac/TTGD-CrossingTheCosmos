using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_CutsceneController : MonoBehaviour
{

    public static RARC_CutsceneController Instance;



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

    public void AttemptCutsceneSkip()
    {
        if (RARC_GameStateController.Instance.currentCutscene_IEnum != null)
        {
            StopCoroutine(RARC_GameStateController.Instance.currentCutscene_IEnum);
            RARC_GameStateController.Instance.cutScene_Animator.gameObject.SetActive(false);
            RARC_GameStateController.Instance.blacokoutCurtain_Animator.Play("Fade Out");
            RARC_GameStateController.Instance.currentCutscene_IEnum = null;
        }
       
    }
}
