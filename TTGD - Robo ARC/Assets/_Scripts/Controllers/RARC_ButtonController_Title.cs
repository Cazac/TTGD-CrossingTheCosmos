using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RARC_ButtonController_Title : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_ButtonController_Title Instance;

    ////////////////////////////////

    [Header("Containers")]
    public GameObject mainMenuPanel_GO;
    public GameObject playMenuPanel_GO;

    [Header("Save Slots")]
    public GameObject saveSlot1_New;
    public GameObject saveSlot1_Load;
    public GameObject saveSlot2_New;
    public GameObject saveSlot2_Load;
    public GameObject saveSlot3_New;
    public GameObject saveSlot3_Load;

    ////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    /////////////////////////////////////////////////////////////////

    public void Button_MainPlay()
    {
        mainMenuPanel_GO.SetActive(false);
        playMenuPanel_GO.SetActive(true);

        RefreshPlayUI();
    }

    public void Button_MainSettings()
    {

    }

    public void Button_MainCredits()
    {
        //Load Credits Scene
        SceneManager.LoadScene("RARC_Credits");
    }

    public void Button_MainQuit()
    {
        //Editor Quit
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        //Quit Application
        Application.Quit();
    }

    /////////////////////////////////////////////////////////////////

    public void Button_PlayCancel()
    {
        mainMenuPanel_GO.SetActive(true);
        playMenuPanel_GO.SetActive(false);
    }

    public void Button_PlaySelectSlot(int saveSlot)
    {

    }

    /////////////////////////////////////////////////////////////////

    public void RefreshPlayUI()
    {
        //Check with Loaded Data
        if (RARC_DatabaseController.Instance.ship_SaveData != null)
        {
            print("Test Code: Data Found");
        }
        else
        {
            print("Test Code: No Data");
        }

        if (true)
        {
            saveSlot1_New.SetActive(true);
            saveSlot1_Load.SetActive(false);
        }
        else
        {
            saveSlot1_New.SetActive(false);
            saveSlot1_Load.SetActive(true);
        }

        if (true)
        {
            saveSlot1_New.SetActive(true);
            saveSlot1_Load.SetActive(false);
        }
        else
        {
            saveSlot1_New.SetActive(false);
            saveSlot1_Load.SetActive(true);
        }

        if (true)
        {
            saveSlot1_New.SetActive(true);
            saveSlot1_Load.SetActive(false);
        }
        else
        {
            saveSlot1_New.SetActive(false);
            saveSlot1_Load.SetActive(true);
        }
    }




    /////////////////////////////////////////////////////////////////
}
