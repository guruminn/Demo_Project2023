using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 作成者：山﨑晶
// ゲームオーバーに関するソースコード

public class GameOverManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// 「FadeManager」を参照
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    /// 背景画像のフェードを設定
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeIn;

    /// <summary>
    /// アイドル画像を取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _idolImage;

    /// <summary>
    /// 観客画像を取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _audienceImage;

    /// <summary>
    /// プレイヤー画像を取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    /// 戻るボタンを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _returnButton;

    /// <summary>
    /// タイトルボタンを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _backButton;

    /// <summary>
    /// ボタンが選択していないときの画像を取得する
    /// </summary>
    [SerializeField]
    private GameObject[] _selectButton = new GameObject[2];

    /// <summary>
    /// ボタンが選択できる状況かを判定する変数
    /// </summary>
    private bool _isButton = false;

    /// <summary>
    /// 現在選択しているオブジェクトを保存する変数
    /// </summary>
    private GameObject _button;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // ボタンのオブジェクトを非表示にする
        _returnButton.SetActive(false);
        _backButton.SetActive(false);

        // キャラクター画像を非表示にする
        _idolImage.SetActive(false);
        _audienceImage.SetActive(false);
        _playerImage.SetActive(false);

        // 選択画像を非表示にする
        foreach (GameObject selectImage in _selectButton)
        {
            selectImage.SetActive(false);
        }

        // 初期で選択状態にしておくオブジェクトの設定
        EventSystem.current.SetSelectedGameObject(_returnButton);
    }

    // Update is called once per frame
    void Update()
    {
        // 現在選択しているオブジェクトを保存する変数
        _button = EventSystem.current.currentSelectedGameObject;

        // フェードインが終わった場合
        if (FadeManager.fadeIn)
        {
            // キャラクター画像を表示する
            _idolImage.SetActive(true);
            _audienceImage.SetActive(true);
            _playerImage.SetActive(true);

            // ボタンの選択をできるようにする
            _isButton = true;
        }
        else if(!FadeManager.fadeIn)
        {
            _fadeSystem.FadeIn(_backGroundFadeIn);
        }

        // ボタンの選択演出
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
