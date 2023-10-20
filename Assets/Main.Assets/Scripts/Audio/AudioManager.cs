using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Inspectorに表示する間隔の長さ
    private const int spaceValue = 8;

    // BGMを鳴らすAudioSourceを取得
    [SerializeField]private  AudioSource _bgmAudioSource;
    // SEを鳴らすAudioSourceを取得
    [SerializeField] private  AudioSource _seAudioSource;
    
    [Space(spaceValue)]

    // BGM、SEを含めて全体の音量を設定する
    [Range(0f, 1f)] public float masterVolume = 1;　
    // BGMの全体の音量を設定する
    [Range(0f, 1f)] public float bgmMasterVolume = 1;
    // SEの全体の音量を設定する
    [Range(0f, 1f)] public float seMasterVolume = 1;

    [Space(spaceValue)]

    // BGMの音源を保存する
    [SerializeField] private List<BGMSoundData> _bgmSoundData;
    // SEの音源を保存する
    [SerializeField] private List<SESoundData> _seSoundData;

    // 
    public static AudioManager audioManager { get; private set; }

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = _bgmSoundData.Find(data => data.bgm == bgm);
        _bgmAudioSource.clip = data.audioClip;
        _bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        _bgmAudioSource.Play();
    }

    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = _seSoundData.Find(data => data.se == se);
        _seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        _seAudioSource.PlayOneShot(data.audioClip);
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
        Clear,
        Over,
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0f, 1f)] public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        ClickButton,
        Walk,
        Squat,
        Correct,
        Found,
        Buzzer,
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0f, 1f)] public float volume = 1;

}

