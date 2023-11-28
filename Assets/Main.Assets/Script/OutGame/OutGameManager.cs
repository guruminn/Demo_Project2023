using Cinemachine;
using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// ゲームオーバーやゲームクリアの判定が有効になった場合に演出をするソースコード
// 作成者：山﨑晶

public class OutGameManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// ゲームオーバーの判定を保存する変数
    /// </summary>
    public static bool gameOver;

    /// <summary>
    /// ゲームクリアの判定を保存する変数
    /// </summary>
    public static bool gameClear;

    /// <summary>
    /// 「FadeSystem」のインスタンスを生成
    /// </summary>
    private FadeManager _fadeSystem;

    /// <summary>
    /// ゲームクリア・オーバー画面に入った時のフェードアウトを設定
    /// </summary>
    private FadeManager.FadeSetting _blackFadeOut;

    /// <summary>
    /// 画面終了時のフェードアウトの設定
    /// </summary>
    private FadeManager.FadeSetting _endFadeOut;

    /// <summary>
    /// 「TranstionScene」のインスタンスを生成
    /// </summary>
    private TranstionScenes _transSystem;

    /// <summary>
    /// ゲームオーバー、ゲームクリアを表示するtextを取得
    /// </summary>
    [SerializeField] 
    private TextMeshProUGUI _logoText;

    /// <summary>
    /// ゲームオーバーの時に表示させる文字を保存する変数
    /// </summary>
    [SerializeField]
    private string _overText;

    /// <summary>
    /// ゲームクリアの時に表示させる文字を保存する変数
    /// </summary>
    [SerializeField] 
    private string _clearText;

    [Space(10)]

    /// <summary>
    /// プレイヤーのコンポーネントを参照するため、オブジェクトを取得
    /// </summary>
    [SerializeField]
    private GameObject _playerObj;

    /// <summary>
    /// カメラのコンポーネントを参照するため、オブジェクトを取得
    /// </summary>
    [SerializeField]
    private GameObject _cameraObj;

    /// <summary>
    /// 警備員のコンポーネントを参照するため、オブジェクトを取得
    /// </summary>
    [SerializeField]
    private GameObject _guardObj;

    [Space(10)]

    // ゲーム終了時のテキストから暗くなるまでの待ち時間を保存する変数
    [SerializeField, Range(0f, 10f)]
    private int _waitTime = 3;

    #endregion ---Fields---

    #region ---Methods---

    private void Start()
    {

    }

    private void Update()
    {
        // ゲームオーバーの時の処理
        if (gameOver)
        {
            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);
            Direction_UI(_overText, 4);
        }

        // ゲームクリアの時の処理
        if (gameClear)
        {
            AudioManager.audioManager.Stop_Sound(AudioManager.audioManager.bgmAudioSource);
            Direction_UI(_clearText, 3);
        }
    }

    /// <summary>
    /// UIの演出の処理をする関数
    /// </summary>
    /// <param name="textWord"> 表示するテキスト </param>
    /// <param name="sceneNumber"> 遷移したいシーンの番号 </param>
    /// <returns> 待ち時間 </returns>
    private IEnumerator Direction_UI(string textWord, int sceneNumber)
    {
        // プレイヤー、警備員の動きを止める
        DontMove_AntherScript();

        // フェードアウトの演出を呼び出す
        _fadeSystem.FadeOut(_blackFadeOut);

        // フェードアウトが終わった場合
        if (FadeManager.fadeOut)
        {
            // テキストを表示してからのシーン遷移をするコルーチンを呼び出す
            // 指定したテキストを表示
            _logoText.text = textWord;

            //「FadeOut」の判定を無効にする
            FadeManager.fadeOut = false;

            // 指定した秒数を待つ
            yield return new WaitForSeconds(_waitTime);

            //「FadeOut」を呼び出す。
            _fadeSystem.FadeOut(_endFadeOut);

            // 画面が暗くなった場合
            if (FadeManager.fadeOut)
            {
                // 指定した番号のシーンに遷移する
                _transSystem.Trans_Scene(sceneNumber);
            }
        }
    }

    /// <summary>
    /// プレイヤーの移動とカメラの動き、警備員の移動を止める処理の関数
    /// </summary>
    private void DontMove_AntherScript()
    {
        // プレイヤーの移動のscriptを無効にする
        _playerObj.GetComponent<MocopiAvatar>().enabled = false;

        // プレイヤーのコントローラーの移動用のscriptを無効にする
        _playerObj.GetComponent<PlayerController>().enabled = false;

        // プレイヤーのMocopiの移動用スクリプトを無効にする
        _playerObj.GetComponent<PlayerWalkManager>().enabled = false;

        // プレイヤーのカメラのscriptを無効にする
        _cameraObj.GetComponent<CinemachineBrain>().enabled = false;

        // 警備員の移動のscriptを無効にする
        _guardObj.GetComponent<AroundGuardsmanController>().enabled = false;

        // 警備員の移動のNavMeshAgentを無効にする
        _guardObj.GetComponent<NavMeshAgent>().enabled = false;

        // 警備員の移動のAnimatorを無効にする
        _guardObj.GetComponent<Animator>().enabled = false;
    }

    #endregion ---Methods---
}
