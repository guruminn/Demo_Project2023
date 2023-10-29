using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// ゲームオーバーやゲームクリアの判定が有効になった場合に演出をするソースコード
// 作成者：山﨑晶
public class VariablesController
{
    // ゲームオーバーの判定を管理する変数
    public static bool gameOverControl;

    // ゲームクリアの判定を管理する変数
    public static bool gameClearControl;

    public static void Initi_Game()
    {
        gameOverControl = false;
        gameClearControl = false;
    }
}

public class OutGameManager : MonoBehaviour
{
    //「FadeSystem」のインスタンスを生成
    private FadeManager fadeSystem=new FadeManager();
    //「TranstionScene」のインスタンスを生成
    public TranstionScenes transSystem;

    [Space(10)]

    // ゲーム終了時のUIの親オブジェクトとして保存する変数
    //[SerializeField]private GameObject finishTag;
    // ゲーム画面から暗くなる画像のコンポーネントを保存する変数
    [SerializeField] private Image splitImage;
    // ゲーム終了時のテキストから暗くなる画像のコンポーネントを保存する変数
    [SerializeField] private Image fadeImage;

    // ゲームオーバー、ゲームクリアを表示するtextを取得
    [SerializeField] private TextMeshProUGUI titleText;
    // ゲームオーバーの時に表示させる文字を保存する変数
    [SerializeField]private string overText;
    // ゲームクリアの時に表示させる文字を保存する変数
    [SerializeField] private string clearText;

    [Space(10)]

    // プレイヤーの移動とカメラを動かしているscriptを参照するため、オブジェクトを取得
    [SerializeField]private GameObject[] playerDontMove=new GameObject[2];

    // 警備員の移動を止めるため、オブジェクトを取得
    [SerializeField]private GameObject guardDontMove;

    [Space(10)]

    // ゲーム終了時のテキストから暗くなるまでの待ち時間を保存する変数
    [SerializeField, Range(0f, 10f)] private int _waitTime = 3;

    // 画像のフェードの速さを保存する変数
    [SerializeField, Range(0f, 10f)] private float _fadeOutSpeed = 0.1f;

    // テキストのフェードの速さを保存する変数
    [SerializeField, Range(0f, 10f)] private float _textOutSpeed=0.1f;

    [SerializeField, Range(0f, 1f)] private float _fadeImageAlpha;

    private void Start()
    {
        FadeVariables.Initi_Fade();

        VariablesController.Initi_Game();
    }

    private void Update()
    {
        // ゲームオーバーの時の処理
        if (VariablesController.gameOverControl)
        {
            AudioManager.Instance.Stop_Sound(AudioManager.Instance.bgmAudioSource);
            Direction_UI(overText, 4);
        }

        // ゲームクリアの時の処理
        if (VariablesController.gameClearControl)
        {
            AudioManager.Instance.Stop_Sound(AudioManager.Instance.bgmAudioSource);
            Direction_UI(clearText, 3);
        }
    }

    // UIの演出の処理をする関数
    private void Direction_UI(string textWord, int sceneNumber)
    {
        // プレイヤー、警備員の動きを止める
        DontMove_AntherScript();

        Debug.Log("動き留めた");

        // フェードアウトの演出を呼び出す
        fadeSystem.FadeOut(splitImage, splitImage.color.a,_fadeOutSpeed, _defaultValue: _fadeImageAlpha);

        // フェードアウトが終わった場合
        if (FadeVariables.FadeOut)
        {
            // テキストを表示してからのシーン遷移をするコルーチンを呼び出す
            StartCoroutine(Display_TitleText(textWord, sceneNumber));
        }
    }

    // テキストを表示して数秒経ったら画面を暗くする演出のコルーチン
    private IEnumerator Display_TitleText(string textWord,int sceneNumber)
    {
        // 指定したテキストを表示
        titleText.text = textWord;

        //「FadeOut」の判定を無効にする
        FadeVariables.FadeOut = false;

        // 指定した秒数を待つ
        yield return new WaitForSeconds(_waitTime);

        //「FadeOut」を呼び出す。
        fadeSystem.FadeOut(fadeImage, fadeImage.color.a, _textOutSpeed);

        // 画面が暗くなった場合
        if (FadeVariables.FadeOut)
        {
            Debug.Log("fadeOut");
            // 指定した番号のシーンに遷移する
            transSystem.Trans_Scene(sceneNumber);
        }
    }

    // プレイヤーの移動とカメラの動き、警備員の移動を止める処理の関数
    private void DontMove_AntherScript()
    {
        // プレイヤーの移動のscriptを無効にする
        playerDontMove[0].GetComponent<MocopiAvatar>().enabled = false;

        // プレイヤーのコントローラーの移動用のscriptを無効にする
        playerDontMove[0].GetComponent<PlayerController>().enabled = false;

        // プレイヤーのカメラのscriptを無効にする
        playerDontMove[1].GetComponent<CameraController>().enabled = false;

        // 警備員の移動のscriptを無効にする
        guardDontMove.GetComponent<AroundGuardsmanController>().enabled = false;

        // 警備員の移動のNavMeshAgentを無効にする
        guardDontMove.GetComponent<NavMeshAgent>().enabled = false;

        // 警備員の移動のAnimatorを無効にする
        guardDontMove.GetComponent<Animator>().enabled = false;
    }
}
