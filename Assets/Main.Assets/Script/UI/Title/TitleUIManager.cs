using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// タイトル画面の演出処理を記述したスクリプト
// 作成者：山﨑晶

public class TitleUIManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// 画像を表示する間隔の時間を保存
    /// </summary>
    [SerializeField, Range(0f, 10f)] 
    private float[] _intervalTIme = new float[2];

    [Space(10)]

    /// <summary>
    /// カメラが移動する速さを保存
    /// </summary>
    [SerializeField, Range(0f, 10f)] 
    private float _cameraMoveSpeed = 1f;
   
    /// <summary>
    /// カメラの初期位置を保存
    /// </summary>
    private Vector3 _startPosition;

    /// <summary>
    /// カメラの移動先の位置を保存
    /// </summary>
    [SerializeField] 
    private Vector3 _endPosition;

    [Space(10)]

    /// <summary>
    /// 「FadeManager」のインスタンスを生成
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    /// 背景画像のフェードアウトの設定
    /// </summary>
    private FadeManager.FadeSetting _blackFadeIn;

    /// <summary>
    /// タイトルロゴのフェードアウトの設定
    /// </summary>
    private FadeManager.FadeSetting _logoFadeOut;

    /// <summary>
    /// 「TranstionScenes」のインスタンスを生成
    /// </summary>
    private TranstionScenes transSystem;

    /// <summary>
    /// 「ui_startButton」と「ui_endButton」をゲームオブジェクトとして取得
    /// </summary>
    [SerializeField]
    private GameObject[] _buttonObj = new GameObject[2];

    /// <summary>
    /// 選択されていないボタンを暗く表示するためのImageを取得
    /// </summary>
    [SerializeField] 
    private GameObject[] _selectButtonImage = new GameObject[2];

    /// <summary>
    /// カメラを動かすために「MainCamera」をゲームオブジェクトとして保存
    /// </summary>
    [SerializeField] 
    private GameObject _cameraObj;

    /// <summary>
    /// 「TitleCanvas」をゲームオブジェクトとして取得
    /// </summary>
    [SerializeField]
    private GameObject _titleCanvas;

    /// <summary>
    /// 初期位置と移動先の距離を保存
    /// </summary>
    private float _distance;

    /// <summary>
    /// ２点間の移動する位置の値を保存
    /// </summary>
    private float _positionValue;

    /// <summary>
    /// 現在の時間を保存する変数
    /// </summary>
    private float _time;

    /// <summary>
    /// スタートボタンが押されたかの判定を保存
    /// </summary>
    private bool _isClickButton = false;

    /// <summary>
    /// ボタンの入力を受け付ける判定を保存
    /// </summary>
    private bool _isInputButton = false;

    /// <summary>
    /// カメラを動かす場面かの判定を保存する変数
    /// </summary>
    private bool _isStepScene = false;

    /// <summary>
    /// 現在の選択しているオブジェクトを保存する変数
    /// </summary>
    private GameObject _saveButton;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // タイトル画面のUIの初期化
        Initi_TitleUI();

        // スタートボタンが押された処理関係の初期化 
        Initi_TransFunction();
    }

    // Update is called once per frame
    void Update()
    {
        // 選択中のボタンの情報を保存する
        _saveButton = EventSystem.current.currentSelectedGameObject;

        if (_saveButton == _buttonObj[0])
        {
            _selectButtonImage[0].SetActive(false);
            _selectButtonImage[1].SetActive(true);
        }
        if( _saveButton == _buttonObj[1])
        {
            _selectButtonImage[1].SetActive(false);
            _selectButtonImage[0].SetActive(true);
        }

        // タイトル画面のUIの演出をするコルーチンを呼び出す
        StartCoroutine("Fade_UI");

        // ボタンを押した場合
        if (_isClickButton&& _isInputButton)
        {
            // カメラを動かす
            Move_CameraObj(1);
        }

        // ｙボタンを押した場合
        if (Input.GetKeyDown(KeyCode.JoystickButton3) )
        {
            // ＳＥを鳴らす
            AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

            // カメラを動かせる判定にする
            _isStepScene = true;

            // 現在のゲーム内の時間を変数に保存する
            _time = Time.time;

            // タイトル画面のUI表示を非表示にする
            _titleCanvas.SetActive(false);
        }

        // ｙボタンを押した場合のカメラ移動の処理
        if (_isStepScene && _isInputButton)
        {
            Move_CameraObj(2);
        }
    }


    /// <summary>
    /// タイトル画面のUIの演出をするコルーチン
    /// </summary>
    /// <returns> 待ち時間 </returns>
    private IEnumerator Fade_UI()
    {
        // 一番目に表示させる演出処理
        if (!FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            // フェードインをする関数を呼び出す
            _fadeSystem.FadeIn(_blackFadeIn);
        }

        // 二番目に表示させる演出処理
        if (FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            // 処理を待つ
            yield return new WaitForSeconds(_intervalTIme[0]);

            if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.bgmAudioSource))
            {
                AudioManager.audioManager.Play_BGMSound(BGMSoundData.BGM.Title);
            }

            // フェードアウトをする関数を呼び出す
            _fadeSystem.FadeOut(_logoFadeOut);
        }

        // 三番目に表示させる演出処理
        if (FadeManager.fadeIn && FadeManager.fadeOut)
        {
            // 処理を待つ
            yield return new WaitForSeconds(_intervalTIme[1]);

            // ボタンを表示させる処理
            for (int i = 0; i < _buttonObj.Length; i++)
            {
                _buttonObj[i].SetActive(true);
            }

            _isInputButton = true;
        }
    }

    /// <summary>
    /// カメラ移動演出関係の初期化の関数
    /// </summary>
    void Initi_TransFunction()
    {
        // カメラの初期位置を変数に保存する
        _startPosition = _cameraObj.transform.position;

        // 初期位置と移動先の位置同士の距離の長さを変数に保存する
        _distance = Vector3.Distance(_startPosition, _endPosition);

        // スタートボタンが押された判定を無効にする
        _isClickButton = false;
    }

    /// <summary>
    /// UI演出関係の初期化の関数
    /// </summary>
    void Initi_TitleUI()
    {
        // ボタンの表示を無効にする
        for (int i = 0; i < _buttonObj.Length; i++)
        {
            _buttonObj[i].SetActive(false);

            _selectButtonImage[i].SetActive(false);
        }

        // 初期に選択状態にしておくオブジェクトを設定する
        EventSystem.current.SetSelectedGameObject(_buttonObj[0]);
    }

    /// <summary>
    /// スタートボタンが押された時の処理の関数
    /// </summary>
    public void OnClick_StartButton()
    {
        AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

        // タイトル画面のUI表示を非表示にする
        _titleCanvas.SetActive(false);

        // スタートボタンが押された判定を有効にする
        _isClickButton = true;

        // 現在のゲーム内の時間を変数に保存する
        _time = Time.time;
    }

    /// <summary>
    /// エンドボタンが押された時の処理の関数
    /// </summary>
    public void OnClick_EndButton()
    {
        AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
        {
            transSystem.Trans_EndGame();
        }
    }

    /// <summary>
    /// カメラを動かす関数
    /// </summary>
    /// <param name="_seceneNumber"> 遷移したいシーンの番号 </param>
    private void Move_CameraObj(int _seceneNumber)
    {
        AudioManager.audioManager.Change_BGMVolume(0.01f);

        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
        {
            //AudioManager.audioManager.Play_SESound(SESoundData.SE.Audience);
            AudioManager.audioManager.Play_SESound(SESoundData.SE.Walk);
        }

        // 初期位置と移動先の距離の割合を計算する処理
        // 「(Time.time - _time) / _distance」は距離の長さを100として見て時間経過で距離の長さを割ることで２点の移動距離を指定する値を求める。
        _positionValue = ((Time.time - _time) / _distance) * _cameraMoveSpeed;

        // カメラの位置を動かす処理
        _cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);

        // カメラの位置が指定した位置に来た場合
        if (_cameraObj.transform.position == _endPosition)
        {
            // スタートボタンが押された判定を無効にする
            _isClickButton = false;

            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.seAudioSource);
            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);

            // チュートリアルのシーンに遷移する
            transSystem.Trans_Scene(_seceneNumber);
        }
    }

    #endregion ---Methods---
}
