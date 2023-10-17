using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// タイトル画面の演出処理を記述したスクリプト
// 作成者：山﨑晶

public class TitleUIManager : MonoBehaviour
{
    [Header("画面表示の処理")]
    // 画像を表示する間隔の時間を保存する変数
    [SerializeField, Range(0f, 10f)] private float[] _waitTime = new float[2];

    [Space(10)]

    //「FadeSystem」のインスタンスを生成。
    [HideInInspector] public FadeManager _fadeSystem;

    //「ui_fadeImage」のコンポーネントを保存する変数
    private Image fadeImage;
    //「ui_titleImage」のコンポーネントを保存する変数
    private Image titleImage;
    // 「ui_startButton」と「ui_endButton」をゲームオブジェクトとして保存する変数
    private GameObject[] buttonObj = new GameObject[2];

    //「TitleUI」を親オブジェクトとして保存する変数
    private GameObject parent;

    [Header("スタートボタン後の処理")]
    [SerializeField, Range(0f, 10f)] private float _moveSpeed = 1f;

    //「TranstionScenes」のインスタンスを生成
    [HideInInspector] public TranstionScenes transSystem;

    // カメラを動かすために「MainCamera」をゲームオブジェクトとして保存する変数
    private GameObject cameraObj;

    //「TitleCanvas」をゲームオブジェクトとして保存する変数
    private GameObject titleCanvas;

    // カメラの初期位置を保存する変数
    private Vector3 _startPosition;
    // カメラの移動先の位置を保存する変数
    [SerializeField] private Vector3 _endPosition;

    // 「_distance」は初期位置と移動先の距離を保存する変数
    // 「_positionValue」は２点間の移動する位置の値を保存する変数
    // 「time」は現在のゲーム時間を保存する変数
    private float _distance, _positionValue, time;
    
    // スタートボタンが押されたかの判定を保存する変数
    private bool _isClickButton;

    // Start is called before the first frame update
    void Start()
    {
        // タイトル画面のUIの初期化
        Initi_TitleUI();

        // スタートボタンが押された処理関係の初期化 
        Initi_TransFunction();

        Debug.Log("_fadeSystem : " + _fadeSystem);
    }

    // Update is called once per frame
    void Update()
    {
        // タイトル画面のUIの演出をするコルーチンを呼び出す
        StartCoroutine("Fade_UI");

        if (_isClickButton)
        {
            Move_CameraObj();
        }
    }

    // タイトル画面のUIの演出をするコルーチン
    private IEnumerator Fade_UI()
    {
        // 一番目に表示させる演出処理
        if (!FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // フェードインをする関数を呼び出す
            _fadeSystem.FadeIn(fadeImage, fadeImage.color.a);
        }

        // 二番目に表示させる演出処理
        if (FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // 処理を待つ
            yield return new WaitForSeconds(_waitTime[0]);

            // フェードアウトをする関数を呼び出す
            _fadeSystem.FadeOut(titleImage, titleImage.color.a);
        }

        // 三番目に表示させる演出処理
        if (FadeVariables.FadeIn && FadeVariables.FadeOut)
        {
            // 処理を待つ
            yield return new WaitForSeconds(_waitTime[1]);

            // ボタンを表示させる処理
            for (int i = 0; i < buttonObj.Length; i++)
            {
                buttonObj[i].SetActive(true);
            }
        }
    }
    void Initi_TransFunction()
    {
        // カメラの初期位置を変数に保存する
        _startPosition = cameraObj.transform.position;

        // 初期位置と移動先の位置同士の距離の長さを変数に保存する
        _distance = Vector3.Distance(_startPosition, _endPosition);

        // スタートボタンが押された判定を無効にする
        _isClickButton = false;
    }

    void Initi_TitleUI()
    {
        //「TitleCanvas」をタグ検索から取得する
        titleCanvas = GameObject.Find("TitleCanvas").gameObject;

        //「ui_fadeImage」のコンポーネントを取得する
        fadeImage = GameObject.Find("FadeSystem/ui_fadeImage").GetComponentInChildren<Image>();

        // タイトル画面のUIの親オブジェクト「TitleUI」をタグ検索から取得する
        parent = GameObject.FindWithTag("TitleUI");

        // 「ui_titleImage」のコンポーネントを取得する
        titleImage = parent.GetComponentInChildren<Image>();

        //「ui_startButton」をゲームオブジェクトして取得する
        buttonObj[0] = parent.transform.GetChild(1).gameObject;

        //「ui_endButton」をゲームオブジェクトとして取得する
        buttonObj[1] = parent.transform.GetChild(2).gameObject;

        //「MainCamera」をゲームオブジェクトとして保存する
        cameraObj = GameObject.Find("Main Camera").gameObject;

        // ボタンの表示を無効にする
        for (int i = 0; i < buttonObj.Length; i++)
        {
            buttonObj[i].SetActive(false);
        }
    }

    public void OnClick_StartButton()
    {
        // タイトル画面のUI表示を非表示にする
        titleCanvas.SetActive(false);

        // スタートボタンが押された判定を有効にする
        _isClickButton = true;
        
        // 現在のゲーム内の時間を変数に保存する
        time = Time.time;
    }

    private void Move_CameraObj()
    {
        // 初期位置と移動先の距離の割合を計算する処理
        // 「(Time.time - time) / _distance」は距離の長さを100として見て時間経過で距離の長さを割ることで２点の移動距離を指定する値を求める。
        _positionValue = ((Time.time - time) / _distance) * _moveSpeed;

        // カメラの位置を動かす処理
        cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);

        // カメラの位置が指定した位置に来た場合
        if (cameraObj.transform.position == _endPosition)
        {
            // スタートボタンが押された判定を無効にする
            _isClickButton = false;

            // チュートリアルのシーンに遷移する
            transSystem.Trans_Scene(1);
        }
    }
}
