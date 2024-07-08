using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("--------------AudioClip--------------")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip logInBackground;
    [SerializeField] private AudioClip normalBattleHematos;
    [SerializeField] private float turnOnOffDuration = 0.5f;
    [Space(10)]
    [Header("Adjust")]
    public int bgMusicMain;
    public int bgNormalBattleHematos;
    [SerializeField] public GameSetting playerVolumeSetting;
    private void Start()
    {
        StartLogInBackground();
        //Debug.Log(bgMusicMain);   
    }

    public void StartLogInBackground(){
        // EazySoundManager.StopAllMusic();
        EazySoundManager.PlayMusic(logInBackground,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
        // StartCoroutine(IEStopMusic());
        // StartCoroutine(IEStartMusic(logInBackground));
    }
    public void StartMainMenuBackGround(){
        // EazySoundManager.PlayMusic(backgroundMusic,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
        StartCoroutine(IEStartMusic(backgroundMusic));
    }
    public void StartNormalBattleHematos(){
        StartCoroutine(IEStartMusic(normalBattleHematos));
        // EazySoundManager.PlayMusic(normalBattleHematos,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
    }
    public void StopCurrentMusic(){
        StartCoroutine(IEStopMusic());
    }
    public IEnumerator IEStopMusic(){
        float duration = turnOnOffDuration;
        while(duration>0){
            yield return new WaitForSecondsRealtime(0.01f);
            EazySoundManager.GlobalMusicVolume -= playerVolumeSetting.gameVolume/(turnOnOffDuration/0.01f);
            duration-= 0.01f;
        }
        EazySoundManager.GlobalMusicVolume = 0;
        EazySoundManager.StopAllMusic();
    }
    public IEnumerator IEStartMusic(AudioClip audioClip){
        float duration = turnOnOffDuration;
        // yield return new WaitForSeconds(turnOnOffDuration*2);
        EazySoundManager.PlayMusic(audioClip,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
        EazySoundManager.GlobalMusicVolume = 0;
        while(duration>0){
            yield return new WaitForSeconds(0.01f);
            EazySoundManager.GlobalMusicVolume += playerVolumeSetting.gameVolume/(turnOnOffDuration/0.01f);
            duration -= 0.01f;
        }
        EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume;
    }

}
