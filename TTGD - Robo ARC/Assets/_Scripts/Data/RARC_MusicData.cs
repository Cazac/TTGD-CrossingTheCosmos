using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_MusicData : MonoBehaviour
{
    ////////////////////////////////

    [Header("Title Music")]
    public RARC_Audio_SO musicTitle_Hopeless;

    [Header("Credits Music")]
    public RARC_Audio_SO musicCredits_Hopeless;

    [Header("Cutscene Music")]
    public RARC_Audio_SO musicCutscene_LastHope;

    [Header("Gameover Music")]
    public RARC_Audio_SO musicGameover_LastHope;

    [Header("Game Music")]
    public RARC_Audio_SO musicGame_NuclearReactor;
    public RARC_Audio_SO musicGame_SpaceChillin;
    public RARC_Audio_SO musicGame_UnravelingCosmos;
    public RARC_Audio_SO musicGame_WorkinBots;

    public List<RARC_Audio_SO> musicGame_List;


    public void BuildDatabase()
    {
        musicGame_List = new List<RARC_Audio_SO>();
        musicGame_List.Add(musicGame_NuclearReactor);
        musicGame_List.Add(musicGame_SpaceChillin);
        musicGame_List.Add(musicGame_UnravelingCosmos);
        musicGame_List.Add(musicGame_WorkinBots);
    }


    ////////////////////////////////
}
