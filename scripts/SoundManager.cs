using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public enum Volumes { 
    Master = 0, 
    HitSFX = 1,
    VoiceOver = 2
};

public class SoundManager : MonoBehaviour {


    public AudioMixer masterMixer; //static public variable cannnot be exposed, if you know how to do that, please let me know
    private float[] previousVolumes;
    private static SoundManager instance;
    private float test;

    public static SoundManager GetInstance()
    {
        //if (instance == null) { instance = new SoundManager(); }
        return instance;
    }

    void Start() 
    {
        test = 0.0f;
        instance = new SoundManager();
        Debug.Log("start of 'Start()'");
        previousVolumes = new float[3];
        for (int i = 0; i < previousVolumes.Length; i++)
        {
            previousVolumes[i] = 0.0f;
        }
        Debug.Log("'Start()': going to call RecordAll");
        RecordAll();
    }

    private SoundManager() 
    {
        
    }

    public void SetMasterVolume(float masterVolume)
    {
        RecordMasterVolume();
        masterMixer.SetFloat("masterVolume", masterVolume);
    }

    public void SetHitSFXVolume(float hitSFXVolume)
    {
        RecordHitSFXVolume();
        masterMixer.SetFloat("hitSFXVolume", hitSFXVolume);
    }

    public void SetVoiceOverVolume(float voiceOverVolume) 
    {
        RecordVoiceOverVolume();
        masterMixer.SetFloat("voiceOverVolume", voiceOverVolume);
    }

    private void RecordMasterVolume() 
    {
        //masterMixer.GetFloat("masterVolume", out previousVolumes[(int) Volumes.Master]);
        masterMixer.GetFloat("masterVolume", out test);
    }

    private void RecordHitSFXVolume() 
    {
        masterMixer.GetFloat("hitSFXVolume", out previousVolumes[(int) Volumes.HitSFX]);
    }

    private void RecordVoiceOverVolume() 
    {
        masterMixer.GetFloat("voiceOverVolume", out previousVolumes[(int) Volumes.VoiceOver]);
    }

    private void RecordAll() 
    {
        RecordMasterVolume();
        RecordHitSFXVolume();
        RecordVoiceOverVolume();//
    }

    public void MuteAll()
    {
        SetMasterVolume(0.0f);
        SetHitSFXVolume(0.0f);
        SetVoiceOverVolume(0.0f);
    }

    public void UnmuteAll()
    {
        SetMasterVolume(previousVolumes[(int) Volumes.Master]);
        SetHitSFXVolume(previousVolumes[(int) Volumes.HitSFX]);
        SetVoiceOverVolume(previousVolumes[(int) Volumes.VoiceOver]);
    }
}
