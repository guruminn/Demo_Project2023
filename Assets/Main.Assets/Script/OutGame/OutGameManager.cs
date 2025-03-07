﻿using Cinemachine;
using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//  ゲームがゲームオーバー・クリアになった時の処理
//  作成者：山﨑晶

public class OutGameManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// 「FadeSystem」を参照する変数
    /// </summary>
    [SerializeField]
    private FadeManager _fadeSystem;

    /// <summary>
    /// 「TranstionScene」を参照する変数
    /// </summary>
    [SerializeField]
    private TranstionScenes _transSystem;

    /// <summary>
    /// 「AudioManager」を参照する変数
    /// </summary>
    [SerializeField]
    private AudioManager audioManager;

    /// <summary>
    /// 値を管理するアセットから値を参照する変数
    /// </summary>
    [SerializeField]
    private ValueSettingManager settingManager;

    /// <summary>
    /// 背景画像のフェードの設定
    /// </summary>
    private FadeManager.FadeSetting _blackFadeOut;

    /// <summary>
    /// シーン遷移時のフェードの設定
    /// </summary>
    private FadeManager.FadeSetting _endFadeOut;

    /// <summary>
    ///  テキストを取得する変数
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _logoText;

    /// <summary>
    /// ゲームオーバー時に表示する文字を保存する変数
    /// </summary>
    [SerializeField]
    private string _overText;

    /// <summary>
    /// ゲームクリア時に表示する文字を保存する変数
    /// </summary>
    [SerializeField]
    private string _clearText;

    [Space(10)]

    /// <summary>
    /// プレイヤーのオブジェクトを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerObj;

    /// <summary>
    /// カメラのオブジェクトを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _cameraObj;

    /// <summary>
    /// 警備員のオブジェクトを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _guardObj;

    [Space(10)]

    /// <summary>
    /// テキストを表示してからシーン遷移するまでの待ち時間を保存する変数
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    private int _waitTime = 3;

    #endregion ---Fields---

    #region ---Methods---

    private void Start()
    {

    }

    private void Update()
    {
        // ゲームオーバー時の処理  
        if (settingManager.gameOver)
        {
            audioManager.StopSound(audioManager.bgmAudioSource);
            StartCoroutine(Direction_UI(_overText, 4));
        }

        // ゲームクリア時の処理
        if (settingManager.gameClear)
        {
            audioManager.StopSound(audioManager.bgmAudioSource);
            StartCoroutine(Direction_UI(_clearText, 3));
        }
    }

    /// <summary>
    /// UI演出の関数
    /// </summary>
    /// <param name="textWord"> 表示したい文字 </param>
    /// <param name="sceneNumber"> 遷移したいシーンの番号  </param>
    /// <returns>  ?      </returns>
    private IEnumerator Direction_UI(string textWord, int sceneNumber)
    {
        // プレイヤーと警備員の動きを止める関数の呼び出し
        DontMove_AntherScript();

        // 背景画像をフェードをする
        _fadeSystem.FadeOut(_blackFadeOut);

        // フェードが終わった場合
        if (FadeManager.fadeOut)
        {
            // テキストを表示
            _logoText.text = textWord;

            // フェードの判定をオフにする
            FadeManager.fadeOut = false;

            // 待ち時間
            yield return new WaitForSeconds(_waitTime);

            // シーン遷移のフェードをする
            _fadeSystem.FadeOut(_endFadeOut);

            // フェードが終わった場合
            if (FadeManager.fadeOut)
            {
                // シーンを遷移する
                _transSystem.Trans_Scene(sceneNumber);
            }
        }
    }

    /// <summary>
    ///  オブジェクトの動きを止める関数
    /// </summary>
    private void DontMove_AntherScript()
    {
        //  プレイヤーのMocopiを止める
        _playerObj.GetComponent<MocopiAvatar>().enabled = false;

        // プレイヤーのジョイコンを止める
        _playerObj.GetComponent<PlayerController>().enabled = false;

        // プレイヤーの足踏みを止める
        _playerObj.GetComponent<PlayerWalkManager>().enabled = false;

        // カメラ移動を止める
        _cameraObj.GetComponent<CinemachineBrain>().enabled = false;

        // 警備員の移動を止める 
        _guardObj.GetComponent<AroundGuardsmanController>().enabled = false;

        // 警備員の動作を止める
        _guardObj.GetComponent<NavMeshAgent>().enabled = false;

        // 警備員のアニメーションを止める
        _guardObj.GetComponent<Animator>().enabled = false;
    }

    #endregion ---Methods---
}