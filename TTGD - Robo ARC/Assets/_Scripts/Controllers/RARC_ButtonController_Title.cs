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
        //CHeck Save Slot
        switch (saveSlot)
        {
            case 1:

                if (RARC_DatabaseController.Instance.saveDataSet1_List.Count == 0)
                {
                    //Create New Data
                    RARC_ShipSaveData shipData = new RARC_ShipSaveData();
                    shipData.CreateNewSave();

                    //Set Transmission Data to Database
                    RARC_DatabaseController.Instance.ship_SaveSlot = saveSlot;
                    RARC_DatabaseController.Instance.ship_SaveData = shipData;

                    //Load Scene
                    SceneManager.LoadScene("RARC_Game");
                }
                else
                {
                    //Load Data 
                    RARC_ShipSaveData shipData = RARC_DatabaseController.Instance.saveDataSet1_List[RARC_DatabaseController.Instance.saveDataSet1_List.Count - 1];

                    //Set Transmission Data to Database
                    RARC_DatabaseController.Instance.ship_SaveSlot = saveSlot;
                    RARC_DatabaseController.Instance.ship_SaveData = shipData;

                    //Load Scene
                    SceneManager.LoadScene("RARC_Game");
                }
                break;

            case 2:

                if (RARC_DatabaseController.Instance.saveDataSet2_List.Count == 0)
                {
                    //Create New Data
                    RARC_ShipSaveData shipData = new RARC_ShipSaveData();
                    shipData.CreateNewSave();

                    //Set Transmission Data to Database
                    RARC_DatabaseController.Instance.ship_SaveSlot = saveSlot;
                    RARC_DatabaseController.Instance.ship_SaveData = shipData;

                    //Load Scene
                    SceneManager.LoadScene("RARC_Game");
                }
                else
                {
                    //Load Data 
                    RARC_ShipSaveData shipData = RARC_DatabaseController.Instance.saveDataSet2_List[RARC_DatabaseController.Instance.saveDataSet2_List.Count - 1];

                    //Set Transmission Data to Database
                    RARC_DatabaseController.Instance.ship_SaveSlot = saveSlot;
                    RARC_DatabaseController.Instance.ship_SaveData = shipData;

                    //Load Scene
                    SceneManager.LoadScene("RARC_Game");
                }
                break;

            case 3:

                if (RARC_DatabaseController.Instance.saveDataSet3_List.Count == 0)
                {
                    //Create New Data
                    RARC_ShipSaveData shipData = new RARC_ShipSaveData();
                    shipData.CreateNewSave();

                    //Set Transmission Data to Database
                    RARC_DatabaseController.Instance.ship_SaveSlot = saveSlot;
                    RARC_DatabaseController.Instance.ship_SaveData = shipData;

                    //Load Scene
                    SceneManager.LoadScene("RARC_Game");
                }
                else
                {
                    //Load Data 
                    RARC_ShipSaveData shipData = RARC_DatabaseController.Instance.saveDataSet3_List[RARC_DatabaseController.Instance.saveDataSet3_List.Count - 1];

                    //Set Transmission Data to Database
                    RARC_DatabaseController.Instance.ship_SaveSlot = saveSlot;
                    RARC_DatabaseController.Instance.ship_SaveData = shipData;

                    //Load Scene
                    SceneManager.LoadScene("RARC_Game");
                }
                break;
        }

    }

    /////////////////////////////////////////////////////////////////

    public void RefreshPlayUI()
    {
        //Set New Save file Lists
        RARC_DatabaseController.Instance.saveDataSet1_List = RARC_DatabaseController.Instance.FindGameData(1);
        RARC_DatabaseController.Instance.saveDataSet2_List = RARC_DatabaseController.Instance.FindGameData(2);
        RARC_DatabaseController.Instance.saveDataSet3_List = RARC_DatabaseController.Instance.FindGameData(3);



        //Check with Loaded Data
        if (RARC_DatabaseController.Instance.saveDataSet1_List.Count != 0)
        {
            saveSlot1_New.SetActive(false);
            saveSlot1_Load.SetActive(true);
        }
        else
        {
            saveSlot1_New.SetActive(true);
            saveSlot1_Load.SetActive(false);
        }

        //Check with Loaded Data
        if (RARC_DatabaseController.Instance.saveDataSet2_List.Count != 0)
        {
            saveSlot2_New.SetActive(false);
            saveSlot2_Load.SetActive(true);
        }
        else
        {
            saveSlot2_New.SetActive(true);
            saveSlot2_Load.SetActive(false);
        }

        //Check with Loaded Data
        if (RARC_DatabaseController.Instance.saveDataSet3_List.Count != 0)
        {
            saveSlot3_New.SetActive(false);
            saveSlot3_Load.SetActive(true);
        }
        else
        {
            saveSlot3_New.SetActive(true);
            saveSlot3_Load.SetActive(false);
        }
    }

    /////////////////////////////////////////////////////////////////
}
