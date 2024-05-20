using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("----------AudioClip--------------")]
    [SerializeField] public AudioClip backgroundMusic;
    [SerializeField] public AudioClip normalBattleHematos;
    public int bgMusicMain;
    public int bgNormalBattleHematos;
    [SerializeField] public GameSetting playerVolumeSetting;
    private void Start()
    {
        bgMusicMain = EazySoundManager.PlayMusic(backgroundMusic,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
        //Debug.Log(bgMusicMain);   
    }
    public void StartNormalBattleHematos(){
        EazySoundManager.PlayMusic(normalBattleHematos,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
    }


}
