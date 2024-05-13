using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("----------AudioClip--------------")]
    [SerializeField] public AudioClip backgroundMusic;
    public int sfx_music_main;
    [SerializeField]public GameSetting playerVolumeSetting;
    private void Start()
    {
        sfx_music_main = EazySoundManager.PlayMusic(backgroundMusic,EazySoundManager.GlobalMusicVolume = playerVolumeSetting.gameVolume , true, true);
        Debug.Log(sfx_music_main);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
