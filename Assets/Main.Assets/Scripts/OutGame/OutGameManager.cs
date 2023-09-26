using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OutGameManager : MonoBehaviour
{
    // フェードアウトをするパネルをもmageとして取得
    [SerializeField] Image fadePanel;

    [Space(6)]

    // フェードアウトする透明度の上限を保存する変数
    [Range(0f, 1f)] public float constAlpha;
    // フェードアウトをするスピードを保存する変数
    [Range(0f, 1f)] public float fadeSpeed;

    [Space(10)]

    // ゲームオーバー、ゲームクリアを表示するtextを取得
    [SerializeField] TextMeshProUGUI titleText;

    [Space(6)]

    // ゲームオーバーの時に表示させる文字を保存する変数
    public string overText;
    // ゲームクリアの時に表示させる文字を保存する変数
    public string clearText;

    [Space(10)]

    // プレイヤーの移動とカメラを動かしているscriptを参照するため、オブジェクトを取得
    public GameObject[] playerDontMove=new GameObject[2];
    
    // 警備員の移動を止めるため、オブジェクトを取得
    public GameObject guardDontMove;

    // フェードアウトをするパネルの透明度を保存する変数
    private float _imageAlpha;

    private void Start()
    {
        // 初期化
        fadePanel.color = new Color(0f, 0f, 0f, 0f);
        fadePanel.enabled = false;
        _imageAlpha = 0.0f;
    }

    private void Update()
    {
        // ゲームオーバーの時の処理
        if (VariablesController.gameOverControl)
        {
            // プレイヤー、警備員の動きを止める
            DontMove_AntherScript();

            // フェードアウトさせるパネルを表示する
            fadePanel.enabled = true;

            // 透明度を加算して上げる
            _imageAlpha += fadeSpeed;
            Debug.Log("_imageAlpha : " + _imageAlpha);

            // パネルに透明度を設定する 
            fadePanel.color = new Color(0f, 0f, 0f, _imageAlpha);

            // パネルの透明度が指定した透明度の値になった時の処理
            if (_imageAlpha >= constAlpha)
            {
                // パネルの透明度を固定する
                _imageAlpha = constAlpha;

                // ゲームオーバーのtextを表示する
                Display_TitleText(overText);
            }
        }

        // ゲームクリアの時の処理
        if (VariablesController.gameClearControl)
        {
            // プレイヤー、警備員の動きを止める
            DontMove_AntherScript();

            // フェードアウトさせるパネルを表示する
            fadePanel.enabled = true;

            // 透明度を加算して上げる
            _imageAlpha += fadeSpeed;
            Debug.Log("_imageAlpha : " + _imageAlpha);

            // パネルに透明度を設定する 
            fadePanel.color = new Color(0f, 0f, 0f, _imageAlpha);

            // パネルの透明度が指定した透明度の値になった時の処理
            if (_imageAlpha >= constAlpha)
            {
                // パネルの透明度を固定する
                _imageAlpha = constAlpha;

                // ゲームオーバーのtextを表示する
                Display_TitleText(clearText);
            }
        }

        // debug
        if (Input.GetKeyDown(KeyCode.M))
        {
            VariablesController.gameOverControl = true;
            Debug.Log("ゲームオーバー入った");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            VariablesController.gameOverControl = false;
            fadePanel.enabled = false;
            _imageAlpha = 0.0f;
        }
    }

    // フェードアウトの演出をする関数
    private void FadeOut()
    {
        // フェードアウトさせるパネルを表示する
        fadePanel.enabled = true;

        // 透明度を加算して上げる
        _imageAlpha += fadeSpeed;

        // フェードアウトさせるパネルの透明度を設定する
        fadePanel.color = new Color(0f, 0f, 0f, _imageAlpha);

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_imageAlpha >= constAlpha)
        {
            // パネルの透明度を固定する
            _imageAlpha = constAlpha;
        }
    }

    // スプリットインの演出をする関数
    private void SplitIn()
    {

    }

    // textを表示する関数
    private void Display_TitleText(string textWord)
    {
        titleText.text = textWord;
    }

    // プレイヤーの移動とカメラの動き、警備員の移動を止める処理の関数
    private void DontMove_AntherScript()
    {
        // プレイヤーの移動のscriptを無効にする
        playerDontMove[0].GetComponent<MocopiAvatar>().enabled = false;

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
