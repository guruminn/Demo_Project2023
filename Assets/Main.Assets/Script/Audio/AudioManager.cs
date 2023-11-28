using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：山﨑晶
// 音に関するソースコード

public class AudioManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// BGMのAudioSource
    /// </summary>
    [SerializeField] 
    public AudioSource bgmAudioSource;

    /// <summary>
    /// SEのAudioSource
    /// </summary>
    [SerializeField] 
    public AudioSource seAudioSource;

    [Space(10)]

    /// <summary>
    /// BGMとSEを含めた全体音量の変数
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float _masterVolume = 1;

    /// <summary>
    /// BGMの全体音量の変数
    /// </summary>
    [SerializeField,Range(0f, 1f)] 
    private float _bgmMasterVolume = 1;

    /// <summary>
    /// SEの全体音量の変数
    /// </summary>
    [SerializeField,Range(0f, 1f)] 
    private float _seMasterVolume = 1;

    [Space(10)]

    /// <summary>
    /// BGMデータのリストの変数
    /// </summary>
    [SerializeField] 
    private List<BGMSoundData> _bgmSoundDatas;

    /// <summary>
    /// SEデータのリストの変数
    /// </summary>
    [SerializeField] 
    private List<SESoundData> _seSoundDatas;

    #endregion ---Fields---

    #region ---Properties---

    // クラスのインスタンスを作成する
    public static AudioManager audioManager { get; private set; }

    #endregion ---Properties---

    #region ---Methods---

    /// <summary>
    /// BGMの音声を鳴らす関数
    /// </summary>
    /// <param name="bgm"> 鳴らしたい音声クリップのタイトル（列挙型） </param>
    public void Play_BGMSound(BGMSoundData.BGM bgm)
    {
        // 音声データを音声データのリストから見つけて保存する
        BGMSoundData data = _bgmSoundDatas.Find(data => data.bgm == bgm);

        // 音声クリップを設定する
        bgmAudioSource.clip = data.audioClip;

        // 音声の音量を鳴らす
        bgmAudioSource.volume = data.volume * _bgmMasterVolume * _masterVolume;

        // 音声を鳴らす
        bgmAudioSource.Play();
    }

    /// <summary>
    /// SEの音声を鳴らす関数
    /// </summary>
    /// <param name="se"> 鳴らしたい音声クリップのタイトル（列挙型） </param>
    public void Play_SESound(SESoundData.SE se)
    {
        // 音声データを音声データのリストから見つけて保存する
        SESoundData data = _seSoundDatas.Find(data => data.se == se);

        // 音声の音量を設定する
        seAudioSource.volume = data.volume * _seMasterVolume * _masterVolume;

        // 音声を鳴らす
        seAudioSource.PlayOneShot(data.audioClip);
    }

    /// <summary>
    /// 音声が鳴っているかを調べる関数
    /// </summary>
    /// <param name="audioSource"> 調べたいAudioSource </param>
    /// <returns> 音が鳴っていたらfalse / 音が止まっていたらtrue </returns>
    public bool CheckPlaySound(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 再生している音声を止める関数
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
/// BGMの音声データクラス
/// </summary>
[System.Serializable]
public class BGMSoundData
{
    /// <summary>
    /// BGMの音声タイトル
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
/// SEの音声データクラス
/// </summary>
[System.Serializable]
public class SESoundData
{
    /// <summary>
    /// SEの音声タイトル
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