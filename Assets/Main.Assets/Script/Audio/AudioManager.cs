using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  作成者：山﨑晶   
//  BGM、SEを制御するソースコード

public class AudioManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// BGM  AudioSource
    /// </summary>
    [SerializeField]
    public AudioSource bgmAudioSource;

    /// <summary>
    /// SE  AudioSource
    /// </summary>
    [SerializeField]
    public AudioSource seAudioSource;

    [Space(10)]

    /// <summary>
    /// BGM、SEを含めた全体音量の変数
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float _masterVolume = 1;

    /// <summary>
    /// BGMの全体音量の変数
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float _bgmMasterVolume = 1;

    /// <summary>
    /// SEの全体音量の変数
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float _seMasterVolume = 1;

    [Space(10)]

    /// <summary>
    /// BGMの音声データのリスト
    /// </summary>
    [SerializeField]
    private List<BGMSoundData> _bgmSoundDatas;

    /// <summary>
    /// SEの音声データのリスト
    /// </summary>
    [SerializeField]
    private List<SESoundData> _seSoundDatas;

    #endregion ---Fields---

    #region ---Properties---

    // AudioManagerのインスタンスを作成   
    public static AudioManager audioManager { get; private set; }

    #endregion ---Properties---

    #region ---Methods---

    /// <summary>
    /// BGMを再生する関数
    /// </summary>
    /// <param name="bgm">  ?流すBGMのタイトル(列挙型) </param>
    public void Play_BGMSound(BGMSoundData.BGM bgm)
    {
        // 音声データから該当するデータを保存する   
        BGMSoundData data = _bgmSoundDatas.Find(data => data.bgm == bgm);

        // AudioClipをAudioSourceに設定する
        bgmAudioSource.clip = data.audioClip;

        // BGMの音量を設定する
        bgmAudioSource.volume = data.volume * _bgmMasterVolume * _masterVolume;

        // 再生する
        bgmAudioSource.Play();
    }

    /// <summary>
    /// SEを再生する関数
    /// </summary>
    /// <param name="se">  ?流すSEのタイトル(列挙型) </param>
    public void Play_SESound(SESoundData.SE se)
    {
        // 音声データから該当するデータを保存する
        SESoundData data = _seSoundDatas.Find(data => data.se == se);

        // SEの音量を設定する
        seAudioSource.volume = data.volume * _seMasterVolume * _masterVolume;

        // SEを再生する
        seAudioSource.PlayOneShot(data.audioClip);
    }

    /// <summary>
    /// 音が再生しているかを調べる関数
    /// </summary>
    /// <param name="audioSource"> 調べたいAudioSource </param>
    /// <returns> 音が再生中だったらfalse / 音が止まっていたらtrue </returns>
    public bool CheckPlaySound(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    ///  再生中の音を止める関数
    /// </summary>
    /// <param name="audioSource"> 止めたいAudioSource </param>
    public void Stop_Sound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    /// <summary>
    /// BGMの音量を変更する関数
    /// </summary>
    /// <param name="soundVolume"> 変更したい音量 </param>
    public void Change_BGMVolume(float soundVolume)
    {
        bgmAudioSource.volume = soundVolume * _bgmMasterVolume * _masterVolume;
    }

    /// <summary>
    /// SEの音量を変更する関数
    /// </summary>
    /// <param name="soundVolume"> 変更したい音量 </param>
    public void Change_SEVolume(float soundVolume)
    {
        seAudioSource.volume = soundVolume * _seMasterVolume * _masterVolume;
    }

    #endregion ---Methods---
}

#region ---Class---

/// <summary>
/// BGMの音声データ
/// </summary>
[System.Serializable]
public class BGMSoundData
{
    /// <summary>
    /// BGMのラベル
    /// </summary>
    public enum BGM
    {
        Title,
        Tutorial,
        Main,
        ClearEnd,
        OverEnd,
    }

    /// <summary>
    /// 列挙型の宣言
    /// </summary>
    public BGM bgm;

    /// <summary>
    /// BGMのAudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// BGMの音量
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1;
}

/// <summary>
/// SEの音声データ
/// </summary>
[System.Serializable]
public class SESoundData
{
    /// <summary>
    /// SEのラベル
    /// </summary>
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

    /// <summary>
    /// 列挙型の宣言
    /// </summary>
    public SE se;

    /// <summary>
    /// SEのAudioClip
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// SEの音量
    /// </summary>
    [Range(0f, 2f)]
    public float volume = 1;
}

#endregion ---Class---