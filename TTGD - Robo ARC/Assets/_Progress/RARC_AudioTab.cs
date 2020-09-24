using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///////////////
/// <summary>
///     
/// TM_AudioTab
/// 
/// TAB CLASS 
/// 
/// 
/// </summary>
///////////////

public class RARC_AudioTab : MonoBehaviour
{
    ////////////////////////////////

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Options")]
    public RARC_Audio_SO currentAudio_SO;

    [Header("Audio Type")]
    public bool isMusic;
    public bool isSFX;

    /////////////////////////////////////////////////////////////////

    public void SetupAudioTrack(RARC_Audio_SO audioSO, float audioTypeSettingsVolume, bool isMuted)
    {
        //Save Scriptable 
        currentAudio_SO = audioSO;

        //Save Settings
        isMusic = audioSO.isMusic;
        isSFX = audioSO.isSFX;
        audioSource.clip = audioSO.audioClip;
        audioSource.loop = audioSO.canLoop;
        audioSource.pitch = audioSO.pitch + (Random.Range(-audioSO.pitchRange, audioSO.pitchRange));
        audioSource.spatialBlend = audioSO.spatialAudioRange;

        //Create a non-linear exponental increae for the voule as audio drop on a logarithmic Rate (Not using the ideal soultion but a close match)
        float logarithmicFalloffAudio = Mathf.Pow((audioTypeSettingsVolume / 100), 2);
        audioSource.volume = ((currentAudio_SO.volume + logarithmicFalloffAudio) / 2);

        //Muted Volume
        if (isMuted)
        {
            audioSource.volume = 0;
        }

        //Play Audio
        audioSource.Play();

        //Begin lerping volume of sound
        if (audioSO.canFadeIn == true)
        {
            //Skip If Muted
            if (!isMuted)
            {
                //Begin lerping volume of sound
                StartCoroutine(IAudioVolumeDampeningOnLoad((audioSource.volume / 60), audioSource.volume, currentAudio_SO.fadeInSpeed));
            }
        }

        //Begin Audio Destruction Countdown
        if (audioSO.canLoop == false)
        {
            //Begin Audio Destruction Countdown
            StartCoroutine(IAutoDestoryCountdown(currentAudio_SO.audioClip.length));
        }
    }

    /////////////////////////////////////////////////////////////////

    public IEnumerator IAudioVolumeDampeningOnLoad(float startVolume, float finalVolume, float volumeRampUpSpeed)
    {
        //Set Default Value
        audioSource.volume = startVolume;

        //Calculate Step Value Per Second
        float volumeStepValue = audioSource.volume / volumeRampUpSpeed;

        //Loop Enum Til the max is hit
        while (audioSource.volume < finalVolume)
        {
            //Increase Volume Per Frame
            audioSource.volume += Time.deltaTime * volumeStepValue;

            if (audioSource.volume >= finalVolume)
            {
                //Max Out Volume and Break Enum
                audioSource.volume = finalVolume;
                yield return null;
            }

            //Wait For Next Frame
            yield return new WaitForEndOfFrame();
        }

        //Max Out Volume and Break Enum
        audioSource.volume = finalVolume;
        yield return null;
    }

    public IEnumerator IAudioVolumeDampeningOnDestory(float volumeRampDownSpeed)
    {
        //Calculate Step Value Per Second
        float VolumeStepValue = audioSource.volume / volumeRampDownSpeed;

        //Loop Enum Til the max is hit
        while (audioSource.volume > 0)
        {
            //Increase Volume Per Frame
            audioSource.volume -= Time.deltaTime * VolumeStepValue;

            if (audioSource.volume <= 0)
            {
                //Max Out Volume and Break Enum
                audioSource.volume = 0;
                DestoryAudio_Instant();
                yield break;
            }

            //Wait For Next Frame
            yield return new WaitForEndOfFrame();
        }

        //Max Out Volume and Break Enum
        audioSource.volume = 0;
        DestoryAudio_Instant();
        yield break;
    }

    /////////////////////////////////////////////////////////////////

    private IEnumerator IAutoDestoryCountdown(float clipLength)
    {
        if (currentAudio_SO.canFadeOut)
        {
            //Caclualte Waiting Before Fade
            float waitLength = clipLength - currentAudio_SO.fadeOutSpeed;

            //Wait Till Clip Can fade Then Start Fading Then Break Out
            yield return new WaitForSeconds(waitLength);
            StartCoroutine(IAudioVolumeDampeningOnDestory(currentAudio_SO.fadeOutSpeed));
            yield break;
        }
        else
        {
            //Wait Till Clip is over + buffer room Then Destory Clip
            yield return new WaitForSeconds(clipLength + 0.1f);
            DestoryAudio_Instant();
            yield break;
        }
    }

    /////////////////////////////////////////////////////////////////

    public void DestoryAudio_Instant()
    {
        //Destory AudioClip Gameobeject
        Destroy(gameObject);
    }

    /////////////////////////////////////////////////////////////////
}