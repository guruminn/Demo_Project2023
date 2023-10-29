using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource bgmAudioSource;
    [SerializeField] public AudioSource seAudioSource;

    [Space(10)]

    [Range(0f, 1f)] public float masterVolume = 1;
    [Range(0f, 1f)] public float bgmMasterVolume = 1;
    [Range(0f, 1f)] public float seMasterVolume = 1;

    [Space(10)]

    [SerializeField] private List<BGMSoundData> bgmSoundDatas;
    [SerializeField] private List<SESoundData> seSoundDatas;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play_BGMSound(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.Play();
    }


    public void Play_SESound(SESoundData.SE se)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        seAudioSource.PlayOneShot(data.audioClip);
    }

    public bool CheckPlaySound(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    public void Stop_Sound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void Change_BGMVolume(float soundVolume)
    {
        bgmAudioSource.volume = soundVolume * bgmMasterVolume * masterVolume;
    }

    public void Change_SEVolume(float soundVolume)
    {
        seAudioSource.volume = soundVolume * seMasterVolume * masterVolume;
    }
}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        Tutorial,
        Main,
        ClearEnd,
        OverEnd,
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0f, 1f)]public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        Audience,
        Shutters,
        Buzzer,
        ClickButton,
        FoundSecurity,
        Correct,
        Squwat,
        Walk,
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0f, 2f)]public float volume = 1;
}