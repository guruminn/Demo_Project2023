using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//  ^ C g    ??  o       L q     X N   v g
//  ? ?F R    

public class TitleUIManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  ?  \      ?u ?  ? ? 
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private float[] _intervalTIme = new float[2];

    [Space(10)]

    /// <summary>
    ///  J       ?    ?    ? 
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private float _cameraMoveSpeed = 1f;

    //  J     ?    ?u  ? 
    /// <summary>
    ///  J     ?    ?u  ? 
    /// </summary>
    private Vector3 _startPosition;

    /// <summary>
    ///  J     ??   ??u  ? 
    /// </summary>
    [SerializeField]
    private Vector3 _endPosition;

    [Space(10)]

    //  X ^ [ g { ^         ?   ?    ? 
    /// <summary>
    ///  uFadeManager v ?C   X ^   X ?? 
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    ///  w i ? ?t F [ h A E g ?? 
    /// </summary>
    private FadeManager.FadeSetting _blackFadeIn;

    /// <summary>
    ///  ^ C g     S ?t F [ h A E g ?? 
    /// </summary>
    private FadeManager.FadeSetting _logoFadeOut;

    /// <summary>
    ///  uTranstionScenes v ?C   X ^   X ?? 
    /// </summary>
    private TranstionScenes transSystem;

    /// <summary>
    ///  uui_startButton v ?uui_endButton v   Q [   I u W F N g ?  ?èÔ
    /// </summary>
    [SerializeField]
    private GameObject[] _buttonObj = new GameObject[2];

    /// <summary>
    ///  I      ?  ?  { ^     ?  \     ? ? Image   èÔ
    /// </summary>
    [SerializeField]
    private GameObject[] _selectButtonImage = new GameObject[2];

    /// <summary>
    ///  J     ??      ??uMainCamera v   Q [   I u W F N g ?  ?? 
    /// </summary>
    [SerializeField]
    private GameObject _cameraObj;

    /// <summary>
    ///  uTitleCanvas v   Q [   I u W F N g ?  ?èÔ
    /// </summary>
    [SerializeField]
    private GameObject _titleCanvas;

    /// <summary>
    ///      ?u ??   ?     ? 
    /// </summary>
    private float _distance;

    /// <summary>
    ///  Q _ ???     ?u ?l  ? 
    /// </summary>
    private float _positionValue;

    /// <summary>
    ///    ??  ? ?     ? 
    /// </summary>
    private float _time;

    /// <summary>
    ///  X ^ [ g { ^         ?   ?    ? 
    /// </summary>
    private bool _isClickButton = false;

    //  { ^   ?  ?  ??t   ?   ? 
    /// <summary>
    ///  { ^   ?  ?  ??t   ?   ? 
    /// </summary>
    private bool _isInputButton = false;

    /// <summary>
    ///  J     ??     ?  ?    ?     ? 
    /// </summary>
    private bool _isStepScene = false;

    /// <summary>
    ///    ??I     ?   I u W F N g  ?     ? 
    /// </summary>
    private GameObject _saveButton;

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

        //  ^ C g    ? UI ?  o      R   [ `     ?Ñëo  
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
            AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

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


    //  ^ C g    ? UI ?  o      R   [ `  
    /// <summary>
    ///  ^ C g    ? UI ?  o      R   [ `  
    /// </summary>
    /// <returns>  ?      </returns>
    private IEnumerator Fade_UI()
    {
        //   ???\       ÁÈ o    
        if (!FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            //  t F [ h C        ?    ?Ñëo  
            _fadeSystem.FadeIn(_blackFadeIn);
        }

        //   ???\       ÁÈ o    
        if (FadeManager.fadeIn && !FadeManager.fadeOut)
        {
            //       ? 
            yield return new WaitForSeconds(_intervalTIme[0]);

            if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.bgmAudioSource))
            {
                AudioManager.audioManager.Play_BGMSound(BGMSoundData.BGM.Title);
            }

            //  t F [ h A E g      ?    ?Ñëo  
            _fadeSystem.FadeOut(_logoFadeOut);
        }

        //  O ???\       ÁÈ o    
        if (FadeManager.fadeIn && FadeManager.fadeOut)
        {
            //       ? 
            yield return new WaitForSeconds(_intervalTIme[1]);

            //  { ^    \       ËY  
            for (int i = 0; i < _buttonObj.Length; i++)
            {
                _buttonObj[i].SetActive(true);
            }

            _isInputButton = true;
        }
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
    ///  X ^ [ g { ^         ?   ?    ?? 
    /// </summary>
    public void OnClick_StartButton()
    {
        AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

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
        AudioManager.audioManager.Play_SESound(SESoundData.SE.ClickButton);

        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
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
        AudioManager.audioManager.Change_BGMVolume(0.01f);

        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
        {
            //AudioManager.audioManager.Play_SESound(SESoundData.SE.Audience);
            AudioManager.audioManager.Play_SESound(SESoundData.SE.Walk);
        }

        //      ?u ??   ?    ?      v Z   ËY  
        //  u(Time.time - _time) / _distance v ?    ?     100 ?  ?  ?  ?o ??    ?        Á∑ ??Q _ ??        w ?  l     ? B
        _positionValue = ((Time.time - _time) / _distance) * _cameraMoveSpeed;

        //  J     ??u ??       
        _cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);
        //  X ^ [ g { ^         ?    ??  ?   
        _isClickButton = false;

        AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.seAudioSource);
        AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);

        //  `   [ g   A   ?V [   ?J ?   
        transSystem.Trans_Scene(_seceneNumber);
    }


    #endregion ---Methods---
}