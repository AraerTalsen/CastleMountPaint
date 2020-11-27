using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.EventInstance SFXVolumeTest;

    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;

    float MasterVolume = 1f;
    float MusicVolume = 1f;
    float SFXVolume = 1f;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");

        SFXVolumeTest = FMODUnity.RuntimeManager.CreateInstance("event:/Test/Test Sound Effect");
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Master.setVolume(MasterVolume);
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;

        //makes sure it isn't playing over itself
        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTest.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTest.start();
        }
    }
}
