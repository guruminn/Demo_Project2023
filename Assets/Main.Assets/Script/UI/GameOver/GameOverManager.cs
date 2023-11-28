using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  ? ?F R    
//  Q [   I [ o [ ??   \ [ X R [ h

public class GameOverManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  uFadeManager v   Q  
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    ///  w i ? ?t F [ h  ? 
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeIn;

    /// <summary>
    ///  A C h   ?   èÔ    ? 
    /// </summary>
    [SerializeField]
    private GameObject _idolImage;

    /// <summary>
    ///  ?q ?   èÔ    ? 
    /// </summary>
    [SerializeField]
    private GameObject _audienceImage;

    /// <summary>
    ///  v   C   [ ?   èÔ    ? 
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    ///  ? { ^     èÔ    ? 
    /// </summary>
    [SerializeField]
    private GameObject _returnButton;

    /// <summary>
    ///  ^ C g   { ^     èÔ    ? 
    /// </summary>
    [SerializeField]
    private GameObject _backButton;

    /// <summary>
    ///  { ^     I     ?  ?  ?  ??   èÔ    
    /// </summary>
    [SerializeField]
    private GameObject[] _selectButton = new GameObject[2];

    /// <summary>
    ///  { ^     I   ?   ??  ???  ? 
    /// </summary>
    private bool _isButton = false;

    /// <summary>
    ///    ?I     ?   I u W F N g  ?     ? 
    /// </summary>
    private GameObject _button;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        //  { ^   ?I u W F N g   \   ?   
        _returnButton.SetActive(false);
        _backButton.SetActive(false);

        //  L     N ^ [ ?   \   ?   
        _idolImage.SetActive(false);
        _audienceImage.SetActive(false);
        _playerImage.SetActive(false);

        //  I   ?   \   ?   
        foreach (GameObject selectImage in _selectButton)
        {
            selectImage.SetActive(false);
        }

        //      ?I    ??  ?    I u W F N g ?? 
        EventSystem.current.SetSelectedGameObject(_returnButton);
    }

    // Update is called once per frame
    void Update()
    {
        //    ?I     ?   I u W F N g  ?     ? 
        _button = EventSystem.current.currentSelectedGameObject;

        //  t F [ h C     I      ?
        if (FadeManager.fadeIn)
        {
            //  L     N ^ [ ?  \      
            _idolImage.SetActive(true);
            _audienceImage.SetActive(true);
            _playerImage.SetActive(true);

            //  { ^   ?I     ?   ? ?   
            _isButton = true;
        }
        else if (!FadeManager.fadeIn)
        {
            _fadeSystem.FadeIn(_backGroundFadeIn);
        }

        //  { ^   ?I     o
        if (_isButton)
        {
            _returnButton.SetActive(true);
            _backButton.SetActive(true);
        }

        if (_button == _returnButton && _isButton)
        {
            _selectButton[0].SetActive(false);
            _selectButton[1].SetActive(true);
        }
        if (_button == _backButton && _isButton)
        {
            _selectButton[0].SetActive(true);
            _selectButton[1].SetActive(false);
        }
    }

    #endregion ---Methods---
}