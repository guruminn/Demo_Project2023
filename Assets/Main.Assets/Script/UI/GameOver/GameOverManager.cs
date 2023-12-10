using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 作成者：山﨑晶 
// ゲームオーバーのUI演出処理

public class GameOverManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    ///  FadeManager
    /// </summary>
    [SerializeField]
    private FadeManager _fadeSystem;

    /// <summary>
    /// 背景をフェードインする設定
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeIn;

    /// <summary>
    ///  アイドルの画像
    /// </summary>
    [SerializeField]
    private GameObject _idolImage;

    /// <summary>
    /// 　観客の画像
    /// </summary>
    [SerializeField]
    private GameObject _audienceImage;

    /// <summary>
    /// プレイヤーの画像
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    /// メイン画面に戻るボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _returnButton;

    /// <summary>
    /// タイトル画面に戻るボタンのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject _backButton;

    /// <summary>
    /// ボタンが選択されてないときに表示する画像
    /// </summary>
    [SerializeField]
    private GameObject[] _selectButton = new GameObject[2];

    /// <summary>
    /// ボタンがを表示する判定
    /// </summary>
    private bool _isButton = false;

    /// <summary>
    /// 現在選択しているオブジェクトを保存
    /// </summary>
    private GameObject _button;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // ボタンを非表示にする
        _returnButton.SetActive(false);
        _backButton.SetActive(false);

        // 画像を非表示にする
        _idolImage.SetActive(false);
        _audienceImage.SetActive(false);
        _playerImage.SetActive(false);

        // 選択画像を非表示にする
        foreach (GameObject selectImage in _selectButton)
        {
            selectImage.SetActive(false);
        }

        // 初期に選択状態にするオブジェクトを設定する
        EventSystem.current.SetSelectedGameObject(_returnButton);
    }

    // Update is called once per frame
    void Update()
    {
        // 現在選択しているボタンを保存する
        _button = EventSystem.current.currentSelectedGameObject;

        // フェードインが終わった場合
        if (FadeManager.fadeIn)
        {
            // 画像を表示する
            _idolImage.SetActive(true);
            _audienceImage.SetActive(true);
            _playerImage.SetActive(true);

            // ボタンを表示するようにする
            _isButton = true;
        }
        else if (!FadeManager.fadeIn)
        {
            _fadeSystem.FadeIn(_backGroundFadeIn);
        }

        // ボタンが表示される判定trueになった場合
        if (_isButton)
        {
            // ボタンを表示にする
            _returnButton.SetActive(true);
            _backButton.SetActive(true);
        }

        // ボタンが選択されている状態を表現する
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