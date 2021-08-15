using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RARC_CreditsController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_CreditsController Instance;

    ////////////////////////////////

    [Header("Canvas Credits Gameobject Animator")]
    public Animator CreditsAnimator;

    [Header("Animation Speed Variables")]
    private int animatorDelay = 2;
    private int animatorSpeed_Fast = 5;
    private int animatorSpeed_Regular = 1;

    ///////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    private void Start()
    {
        //Start the Coroutine to play the slideshow after a delay
        StartCoroutine(CreditsPlay());
    }

    private void Update()
    {
        //Set Scroll Speed
        SpeedUp();

        //Quit the Credits
        ExitCredits();
    }

    ///////////////////////////////////////////////////////

    private void SpeedUp()
    {
        //Look for the spacebar key to speed up the credits
        if (Input.GetKey(KeyCode.Space))
        {
            //Go faster
            CreditsAnimator.speed = animatorSpeed_Fast;
        }
        else
        {
            //Go slower
            CreditsAnimator.speed = animatorSpeed_Regular;
        }
    }

    private void ExitCredits()
    {
        //Look for the escape key to exit the credits
        if (Input.GetKey(KeyCode.Escape))
        {
            //Load back into main game
            SceneManager.LoadScene("RARC_Title");
        }
    }

    ///////////////////////////////////////////////////////

    private IEnumerator CreditsPlay()
    {
        //Wait before starting the slideshow
        yield return new WaitForSeconds(animatorDelay);

        //Play Credits Slideshow
        CreditsAnimator.Play("Play");
    }

    ///////////////////////////////////////////////////////
}
