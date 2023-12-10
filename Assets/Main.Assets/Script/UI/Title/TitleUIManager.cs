using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// タイトル画面のUI演出をするソースコード
//  作成者：山﨑晶

public class TitleUIManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  待ち時間を保存する変数
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private float[] _intervalTIme = new float[2];

    [Space(10)]

    /// <summary>
    /// カメラの移動する速さ
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private float _cameraMoveSpeed = 1f;

    /// <summary>
    ///  カメラの初期位置を保存する変数
    /// </summary>
    private Vector3 _startPosition;

    /// <summary>
    ///  カメラの移動先を保存する変数
    /// </summary>
    [SerializeField]
    private Vector3 _endPosition;

    [Space(10)]

    /// <summary>
    ///  FadeManagerを参照する変数
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    ///  背景画像のフェードを設定する
    /// </summary>
    private FadeManager.FadeSetting _blackFadeIn;

    /// <summary>
    ///  タイトルロゴのフェードを設定する
    /// </summary>
    private FadeManager.FadeSetting _logoFadeOut;

    /// <summary>
    ///  TranstionScenesを参照する変数
    /// </summary>
    private TranstionScenes transSystem;

    /// <summary>
    ///  ボタンを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject[] _buttonObj = new GameObject[2];

    /// <summary>
    /// 選択状態のボタンの画像を取得数変数
    /// </summary>
    [SerializeField]
    private GameObject[] _selectButtonImage = new GameObject[2];

    /// <summary>
    /// カメラを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _cameraObj;

    /// <summary>
    /// canvasを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _titleCanvas;

    /// <summary>
    /// カメラの距離を保存する変数
    /// </summary>
    private float _distance;

    /// <summary>
    ///  カメラの位置を保存する変数
    /// </summary>
    private float _positionValue;

    /// <summary>
    /// 時間を保存する変数
    /// </summary>
    private float _time;

    /// <summary>
    /// ボタンが押されたかを判定する変数
    /// </summary>
    private bool _isClickButton = false;

    /// <summary>
    /// ボタンが押せる状況化を判定する変数
    /// </summary>
    private bool _isInputButton = false;

    /// <summary>
    /// シーン遷移ができる状況化を判定する変数
    /// </summary>
    private bool _isStepScene = false;

    /// <summary>
    /// 選択状態のボタンを保存する変数? 
    /// </summary>
    private GameObject _saveButton;

    /// <summary>
    /// AudioManagerを取得する
    /// </summary>
    [SerializeField]
    private AudioManager audioManager;

    /// <summary>
    /// 値を参照するために取得する変数
    /// </summary>
    public ValueSettingManager settingManager;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        //  ^ C g    ? UI ?     
        Initi_TitleUI();

        //  X ^ [ g { ^         ?     ?W ?      
        Initi_TransFunction();
    }

    // Update is called once per frame
    void Update()
    {
        //  I ???{ ^   ?   ?     
        _saveButton = EventSystem.current.currentSelectedGameObject;

        if (_saveButton == _buttonObj[0])
        {
            _selectButtonImage[0].SetActive(false);
            _selectButtonImage[1].SetActive(true);
        }
        if (_saveButton == _buttonObj[1])
        {
            _selectButtonImage[1].SetActive(false);
            _selectButtonImage[0].SetActive(true);
        }

        //  ^ C g    ? UI ?  o      R   [ `     ?яo  
        StartCoroutine("Fade_UI");

        //  { ^           ?
        if (_isClickButton && _isInputButton)
        {
            //  J     ??   
            Move_CameraObj(1);
        }

        //    { ^           ?
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            //  r d  ??
            audioManager.Play_SESound(SEData.SE.ClickButton);

            //  J     ??    ?  ?   
            _isStepScene = true;

            //    ??Q [     ?  ? ?  ??     
            _time = Time.time;

            //  ^ C g    ? UI \     \   ?   
            _titleCanvas.SetActive(false);
        }

        //    { ^           ? ?J     ?  ?   
        if (_isStepScene && _isInputButton)
        {
            Move_CameraObj(2);
        }
    }


    /// <summary>
    /// UI   o ?W ?      ?? 
    /// </summary>
    void Initi_TitleUI()
    {
        //  { ^   ?\   ??  ?   
        for (int i = 0; i < _buttonObj.Length; i++)
        {
            _buttonObj[i].SetActive(false);

            _selectButtonImage[i].SetActive(false);
        }

        //      ?I    ??  ?    I u W F N g  ??  
        EventSystem.current.SetSelectedGameObject(_buttonObj[0]);
    }

    /// <summary>
    ///  J     ?    o ?W ?      ?? 
    /// </summary>
    void Initi_TransFunction()
    {
        //  J     ?    ?u  ?  ??     
        _startPosition = _cameraObj.transform.position;

        //      ?u ??   ??u   m ?    ?     ?  ??     
        _distance = Vector3.Distance(_startPosition, _endPosition);

        //  X ^ [ g { ^         ?    ??  ?   
        _isClickButton = false;
    }

    /// <summary>
    ///  ^ C g    ? UI ?  o      R   [ `  
    /// </summary>
    /// <returns>  ?      </returns>
    private IEnumerator Fade_UI()
    {
        //   ???\       鉉 o    
        if (!FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            //  t F [ h C        ?    ?яo  
            _fadeSystem.FadeIn(_blackFadeIn);
        }

        //   ???\       鉉 o    
        if (FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            //       ? 
            yield return new WaitForSeconds(_intervalTIme[0]);

            if (audioManager.CheckPlaySound(audioManager.bgmAudioSource))
            {
                audioManager.Play_BGMSound(BGMData.BGM.Title);
            }

            //  t F [ h A E g      ?    ?яo  
            _fadeSystem.FadeOut(_logoFadeOut);
        }

        //  O ???\       鉉 o    
        if (FadeManager.fadeIn && FadeManager.fadeOut)
        {
            //       ? 
            yield return new WaitForSeconds(_intervalTIme[1]);

            //  { ^    \       鏈  
            for (int i = 0; i < _buttonObj.Length; i++)
            {
                _buttonObj[i].SetActive(true);
            }

            _isInputButton = true;
        }
    }

    /// <summary>
    ///  X ^ [ g { ^         ?   ?    ?? 
    /// </summary>
    public void OnClick_StartButton()
    {
        audioManager.Play_SESound(SEData.SE.ClickButton);

        //  ^ C g    ? UI \     \   ?   
        _titleCanvas.SetActive(false);

        //  X ^ [ g { ^         ?     L   ?   
        _isClickButton = true;

        //    ??Q [     ?  ? ?  ??     
        _time = Time.time;
    }

    /// <summary>
    ///  G   h { ^         ?   ?    ?? 
    /// </summary>
    public void OnClick_EndButton()
    {
        audioManager.Play_SESound(SEData.SE.ClickButton);

        if (audioManager.CheckPlaySound(audioManager.seAudioSource))
        {
            transSystem.Trans_EndGame();
        }
    }

    /// <summary>
    ///  J     ??    ? 
    /// </summary>
    /// <param name="_seceneNumber">  J ?      V [   ??  </param>
    private void Move_CameraObj(int _seceneNumber)
    {
        audioManager.Change_BGMVolume(0.01f);

        if (audioManager.CheckPlaySound(audioManager.seAudioSource))
        {
            //AudioManager.audioManager.Play_SESound(SESoundData.SE.Audience);
            audioManager.Play_SESound(SEData.SE.Walk);
        }

        //      ?u ??   ?    ?      v Z   鏈  
        //  u(Time.time - _time) / _distance v ?    ?     100 ?  ?  ?  ?o ??    ?        邱 ??Q _ ??        w ?  l     ? B
        _positionValue = ((Time.time - _time) / _distance) * _cameraMoveSpeed;

        //  J     ??u ??       
        _cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);
        //  X ^ [ g { ^         ?    ??  ?   
        _isClickButton = false;

        audioManager.Stop_Sound(audioManager.seAudioSource);
        audioManager.Stop_Sound(audioManager.bgmAudioSource);

        //  `   [ g   A   ?V [   ?J ?   
        transSystem.Trans_Scene(_seceneNumber);
    }


    #endregion ---Methods---
}