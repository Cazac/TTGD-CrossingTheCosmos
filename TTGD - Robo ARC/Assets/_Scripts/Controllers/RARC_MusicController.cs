using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///////////////
/// <summary>
///     
/// RARC_MusicController controls the credits sequence at the end of the game or though the menu.
/// 
/// CONTROLLER CLASS
/// Controller classes are used as a manager of an entire system. 
/// Each controller is assigned a singleton for easy access.
/// 
/// </summary>
///////////////

public class RARC_MusicController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_MusicController Instance;

    ////////////////////////////////

    [Header("Prefabs")]
    public GameObject audioTrack_Prefab;

    [Header("Containers")]
    public GameObject audioTrack_Container;

    [Header("Current Track")]
    public RARC_Audio_SO currentMusicTrack_SO;

    ////////////////////////////////

    [Header("Scenes")]
    private readonly string titleScene_Name = "RARC_Title";
    private readonly string gameScene_Name = "RARC_Game";
    private readonly string creditsScene_Name = "RARC_Credits";

    ///////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    private void Start()
    {
        //Setup
        GetSceneStartupMusic();
    }

    ///////////////////////////////////////////////////////

    private void GetSceneStartupMusic()
    {
        //Get Name Of Current Scene
        string currentScene = SceneManager.GetActiveScene().name;

        //Check the type by scene
        if (currentScene == titleScene_Name)
        {
            PlayMusic_TitleMain();
        }
        else if (currentScene == gameScene_Name)
        {
            //Nothing Another Manager Willp Play This
        }
        else if (currentScene == creditsScene_Name)
        {
            PlayMusic_Credits();
        }
    }

    /////////////////////////////////////////////////////////////////

    public void PlayTrackMusic_SpatialLocation(RARC_Audio_SO audioSO, GameObject locationalParent)
    {
        //Stop Other Music
        StopTrackMusic_All();

        //Instantiate New Audio Source At Location And Set Name
        GameObject newTrack = Instantiate(audioTrack_Prefab, locationalParent.transform);
        newTrack.name = audioSO.name;

        //Record The current Track
        currentMusicTrack_SO = audioSO;

        //Music Setup
        newTrack.GetComponent<RARC_AudioTab>().SetupAudioTrack(audioSO, RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isMusicMuted);
    }

    public void PlayTrackMusic_NoLocation(RARC_Audio_SO audioSO)
    {
        //Stop Other Music
        StopTrackMusic_All();

        //Instantiate New Audio Source User Container
        GameObject newTrack = Instantiate(audioTrack_Prefab, audioTrack_Container.transform);
        newTrack.name = audioSO.name;

        //Record The current Track
        currentMusicTrack_SO = audioSO;

        //Music Setup
        newTrack.GetComponent<RARC_AudioTab>().SetupAudioTrack(audioSO, RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isMusicMuted);
    }

    /////////////////////////////////////////////////////////////////

    public GameObject PlayTrackMusic_SpatialLocation_RandomTrackList(List<RARC_Audio_SO> audioSO_List, GameObject locationalParent)
    {
        //Stop Other Music
        StopTrackMusic_All();

        //Filter the Possible Tracks First
        audioSO_List = FilterCurrentTrackOutOfList(audioSO_List);

        //Random Choice
        int randomChoice = Random.Range(0, audioSO_List.Count);

        //Instantiate New Audio Source At Location
        GameObject newTrack = Instantiate(audioTrack_Prefab, locationalParent.transform);
        newTrack.name = audioSO_List[randomChoice].name;

        //SFX Setup
        newTrack.GetComponent<RARC_AudioTab>().SetupAudioTrack(audioSO_List[randomChoice], RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isMusicMuted);

        //Record The current Track
        currentMusicTrack_SO = audioSO_List[randomChoice];

        //Return Track
        return newTrack;
    }

    public GameObject PlayTrackMusic_NoLocation_RandomTrackList(List<RARC_Audio_SO> audioSO_List)
    {
        //Stop Other Music
        StopTrackMusic_All();

        //Filter the Possible Tracks First
        audioSO_List = FilterCurrentTrackOutOfList(audioSO_List);

        //Random Choice
        int randomChoice = Random.Range(0, audioSO_List.Count);

        //Instantiate New Audio Source Unser Container
        GameObject newTrack = Instantiate(audioTrack_Prefab, audioTrack_Container.transform);
        newTrack.name = audioSO_List[randomChoice].name;

        //SFX Setup
        newTrack.GetComponent<RARC_AudioTab>().SetupAudioTrack(audioSO_List[randomChoice], RARC_DatabaseController.Instance.player_SaveData.settings_MusicVolume, RARC_DatabaseController.Instance.player_SaveData.settings_isMusicMuted);

        //Record The current Track
        currentMusicTrack_SO = audioSO_List[randomChoice];

        //Return Track
        return newTrack;
    }

    /////////////////////////////////////////////////////////////////

    public List<RARC_Audio_SO> FilterCurrentTrackOutOfList(List<RARC_Audio_SO> audioSO_List)
    {
        //Create a new filtered List
        List<RARC_Audio_SO> filterdAudioSO_List = new List<RARC_Audio_SO>();

        //Loop all tracks searching for the same name
        for (int i = 0; i < audioSO_List.Count; i++)
        {
            if (currentMusicTrack_SO != null)
            {
                if (currentMusicTrack_SO.name == audioSO_List[i].name)
                {
                    audioSO_List.RemoveAt(i);
                    break;
                }
            }
            else
            {
                break;
            }
        }

        //Return the filtered List
        return audioSO_List;
    }

    /////////////////////////////////////////////////////////////////

    public void StopTrackMusic_Single(GameObject audioTrack)
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

    public void StopTrackMusic_All()
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
        //Loop All Current Music Tracks
        foreach (Transform child in audioTrack_Container.transform)
        {
            //Get Music Tab
            RARC_AudioTab audioTab = child.gameObject.GetComponent<RARC_AudioTab>();

            //Check If Music
            if (audioTab.isMusic)
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
                    //float logarithmicFalloffAudio = Mathf.Pow((audioTypeSettingsVolume / 100), 2);
                    //audioTab.audioSource.volume = (audioTab.currentAudio_SO.volume * logarithmicFalloffAudio);
                    audioTab.audioSource.volume = (audioTab.currentAudio_SO.volume * audioTypeSettingsVolume);
                }
            }
        }
    }

    ///////////////////////////////////////////////////////////////// - Starting Music 

    public void PlayMusic_TitleMain()
    {
        //Get Clip
        PlayTrackMusic_NoLocation(RARC_DatabaseController.Instance.music_DB.musicTitle_Hopeless);
    }

    public void PlayMusic_Credits()
    {
        //Get Clip
        PlayTrackMusic_NoLocation(RARC_DatabaseController.Instance.music_DB.musicCredits_Hopeless);
    }

    public void PlayMusic_Cutscene()
    {
        //Get Clip
        PlayTrackMusic_NoLocation(RARC_DatabaseController.Instance.music_DB.musicCutscene_LastHope);
    }

    public void PlayMusic_FirstTrack()
    {
        //Get Clip
        PlayTrackMusic_NoLocation(RARC_DatabaseController.Instance.music_DB.musicGame_SpaceChillin);
    }

    public void PlayMusic_Gameover()
    {
        //Get Clip
        PlayTrackMusic_NoLocation(RARC_DatabaseController.Instance.music_DB.musicGameover_LastHope);
    }

    /////////////////////////////////////////////////////////////////
}
