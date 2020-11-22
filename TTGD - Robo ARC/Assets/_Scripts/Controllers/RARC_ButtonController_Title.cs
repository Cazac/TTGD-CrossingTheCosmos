using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RARC_ButtonController_Title : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_ButtonController_Title Instance;

    ////////////////////////////////

    [Header("Containers")]
    public GameObject mainMenuPanel_GO;
    public GameObject playMenuPanel_GO;
    public GameObject settingsMenuPanel_GO;

    [Header("Save Slots")]
    public RARC_PlayMenuUITab saveSlot1;
    public RARC_PlayMenuUITab saveSlot2;
    public RARC_PlayMenuUITab saveSlot3;

    [Header("BLANKVAR")]
    public Slider settingsMusic_Slider;
    public Slider settingsSFX_Slider;

    public TextMeshProUGUI settingsMusic_Text;
    public TextMeshProUGUI settingsSFX_Text;

    [Header("Space")]
    public RARC_SpaceTab titleSpace;

    ////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    private void Start()
    {
        titleSpace.PlayTitleSpace();
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
        mainMenuPanel_GO.SetActive(false);
        settingsMenuPanel_GO.SetActive(true);

        RefreshSettingsUI();
    }

    public void Button_MainCredits()
    {
        //Load Scene
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

    public void Button_PlayDeleteSlot(int saveSlot)
    {
        RARC_DatabaseController.Instance.DeleteShipData(saveSlot);
        RefreshPlayUI();
    }

    /////////////////////////////////////////////////////////////////

    public void Button_SettingsCancel()
    {
        mainMenuPanel_GO.SetActive(true);
        settingsMenuPanel_GO.SetActive(false);

        //Save Player Data
        RARC_DatabaseController.Instance.SavePlayerData();
    }

    public void Slider_Settings_MusicVolume()
    {
        RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume = settingsMusic_Slider.value / 100;
        settingsMusic_Text.text = settingsMusic_Slider.value + "%";
        RARC_MusicController.Instance.VolumeLevels_UpdateAll(RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isMusicMuted);
    }

    public void Slider_Settings_SFXVolume()
    {
        RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume = settingsSFX_Slider.value / 100;
        settingsSFX_Text.text = settingsSFX_Slider.value + "%";
        RARC_SFXController.Instance.VolumeLevels_UpdateAll(RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isSFXMuted);
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
            saveSlot1.LoadPanel(RARC_DatabaseController.Instance.saveDataSet1_List[0], RARC_DatabaseController.Instance.saveDataSet1_List.Count);
        }
        else
        {
            saveSlot1.NewPanel();
        }

        //Check with Loaded Data
        if (RARC_DatabaseController.Instance.saveDataSet2_List.Count != 0)
        {
            saveSlot2.LoadPanel(RARC_DatabaseController.Instance.saveDataSet2_List[0], RARC_DatabaseController.Instance.saveDataSet2_List.Count);
        }
        else
        {
            saveSlot2.NewPanel();
        }

        //Check with Loaded Data
        if (RARC_DatabaseController.Instance.saveDataSet3_List.Count != 0)
        {
            saveSlot3.LoadPanel(RARC_DatabaseController.Instance.saveDataSet3_List[0], RARC_DatabaseController.Instance.saveDataSet3_List.Count);
        }
        else
        {
            saveSlot3.NewPanel();
        }
    }

    public void RefreshSettingsUI()
    {
        //Music
        settingsMusic_Slider.value = RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume * 100;
        settingsMusic_Text.text = settingsMusic_Slider.value + "%";
        //muteMusic_Toggle.isOn = RARC_DatabaseController.Instance.player_SaveData.settings_isMusicMuted;

        //SFX
        settingsSFX_Slider.value = RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume * 100;
        settingsSFX_Text.text = settingsSFX_Slider.value + "%";
        //muteSFX_Toggle.isOn = RARC_DatabaseController.Instance.player_SaveData.settings_isSFXMuted;
    }

    /////////////////////////////////////////////////////////////////
}
