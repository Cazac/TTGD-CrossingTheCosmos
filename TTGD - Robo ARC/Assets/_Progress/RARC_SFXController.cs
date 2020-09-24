using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RARC_SFXController : MonoBehaviour
{
    //////////////////////////////// - Singleton Refference

    public static RARC_SFXController Instance;

    ////////////////////////////////

    [Header("Prefab")]
    public GameObject audioTrack_Prefab;

    [Header("Container")]
    public GameObject audioTrack_Container;

    ////////////////////////////////

    [Header("Scene Names")]
    private readonly string titleScene_Name = "RARC_Title";
    private readonly string gameScene_Name = "RARC_Game";
    private readonly string creditsScene_Name = "RARC_Credits";

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Self Refference Singleton
        Instance = this;
    }

    private void Start()
    {
        //Setup
        GetSceneSetupSFX();
    }

    /////////////////////////////////////////////////////////////////

    private void GetSceneSetupSFX()
    {
        //Get Name Of Current Scene
        string currentScene = SceneManager.GetActiveScene().name;

        //Check the type by scene
        if (currentScene == titleScene_Name)
        {
            //Nothing 
        }
        else if (currentScene == gameScene_Name)
        {
            //Nothing 
        }
        else if (currentScene == creditsScene_Name)
        {
            //Nothing 
        }
    }

    /////////////////////////////////////////////////////////////////

    public GameObject PlayTrackSFX_SpatialLocation_SingleTrack(RARC_Audio_SO audioSO, GameObject locationalParent)
    {
        //Instantiate New Audio Source At Location
        GameObject newTrack = Instantiate(audioTrack_Prefab, locationalParent.transform);

        //SFX Setup
        newTrack.GetComponent<RARC_AudioTab>().SetupAudioTrack(audioSO, RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isSFXMuted);
        newTrack.name = audioSO.name;

        //Return Track
        return newTrack;
    }

    public GameObject PlayTrackSFX_NoLocation_SingleTrack(RARC_Audio_SO audioSO)
    {
        //Instantiate New Audio Source Unser Container
        GameObject newTrack = Instantiate(audioTrack_Prefab, audioTrack_Container.transform);

        //SFX Setup
        newTrack.GetComponent<RARC_AudioTab>().SetupAudioTrack(audioSO, RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isSFXMuted);
        newTrack.name = audioSO.name;

        //Return Track
        return newTrack;
    }

    /////////////////////////////////////////////////////////////////

    public GameObject PlayTrackSFX_SpatialLocation_RandomTrackList(List<RARC_Audio_SO> audioSO_List, GameObject locationalParent)
    {
        //Random Choice
        int randomChoice = Random.Range(0, audioSO_List.Count);

        //Instantiate New Audio Source At Location
        GameObject newTrack = Instantiate(audioTrack_Prefab, locationalParent.transform);
        newTrack.name = audioSO_List[randomChoice].name;

        //SFX Setup
        newTrack.GetComponent<RARC_AudioTab>().SetupAudioTrack(audioSO_List[randomChoice], RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isSFXMuted);

        //Return Track
        return newTrack;
    }

    public GameObject PlayTrackSFX_NoLocation_RandomTrackList(List<RARC_Audio_SO> audioSO_List)
    {
        //Random Choice
        int randomChoice = Random.Range(0, audioSO_List.Count);

        //Instantiate New Audio Source Unser Container
        GameObject newTrack = Instantiate(audioTrack_Prefab, audioTrack_Container.transform);
        newTrack.name = audioSO_List[randomChoice].name;

        //SFX Setup
        newTrack.GetComponent<RARC_AudioTab>().SetupAudioTrack(audioSO_List[randomChoice], RARC_DatabaseController.Instance.player_SaveData.settings_SFXVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isSFXMuted);

        //Return Track
        return newTrack;
    }

    /////////////////////////////////////////////////////////////////

    public void StopTrackSFX_Single(GameObject audioTrack)
    {
        //Get Tab
        RARC_AudioTab audioTab = audioTrack.gameObject.GetComponent<RARC_AudioTab>();

        //Fade Out Or Destory
        if (audioTab.currentAudio_SO.canFadeOut)
        {
            StartCoroutine(audioTab.IAudioVolumeDampeningOnDestory(audioTab.currentAudio_SO.fadeOutSpeed));
        }
        else
        {
            audioTab.DestoryAudio_Instant();
        }
    }

    public void StopTrackSFX_All()
    {
        //Loop all Tabs
        foreach (Transform child in audioTrack_Container.transform)
        {
            //Get Tab
            RARC_AudioTab audioTab = child.gameObject.GetComponent<RARC_AudioTab>();

            //Fade Out Or Destory
            if (audioTab.currentAudio_SO.canFadeOut)
            {
                StartCoroutine(audioTab.IAudioVolumeDampeningOnDestory(audioTab.currentAudio_SO.fadeOutSpeed));
            }
            else
            {
                audioTab.DestoryAudio_Instant();
            }
        }
    }

    /////////////////////////////////////////////////////////////////

    public void VolumeLevels_UpdateAll(float audioTypeSettingsVolume, bool isMuted)
    {
        //Loop All Current SFX Tracks
        foreach (Transform child in audioTrack_Container.transform)
        {
            //Get Tab
            RARC_AudioTab audioTab = child.gameObject.GetComponent<RARC_AudioTab>();

            //Check If SFX
            if (audioTab.isSFX)
            {
                //Set Volume
                if (isMuted)
                {
                    //Mute
                    audioTab.audioSource.volume = 0;
                }
                else
                {
                    //Update Volume By Creating a non-linear exponental increae for the voule as audio drop on a logarithmic Rate (Not using the ideal soultion but a close match)
                    float logarithmicFalloffAudio = Mathf.Pow((audioTypeSettingsVolume / 100), 2);
                    audioTab.audioSource.volume = ((audioTab.currentAudio_SO.volume + logarithmicFalloffAudio) / 2);
                }
            }
        }
    }

    /////////////////////////////////////////////////////////////////
}
