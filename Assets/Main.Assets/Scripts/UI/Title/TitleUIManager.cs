using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// タイトル画面の演出処理を記述したスクリプト
// 作成者：山﨑晶

public class TitleUIManager : MonoBehaviour
{
    // 画像を表示する間隔の時間を保存
    [SerializeField, Range(0f, 10f)] private float[] _intervalTIme = new float[2];

    [Space(10)]

    // カメラが移動する速さを保存
    [SerializeField, Range(0f, 10f)] private float _cameraMoveSpeed = 1f;
   
    // カメラの初期位置を保存
    private Vector3 _startPosition;
    // カメラの移動先の位置を保存
    [SerializeField] private Vector3 _endPosition;

    [Space(10)]

    //// フェードインの速さを保存
    //[SerializeField, Range(0f, 10f)] private float _fadeSpeed = 0.1f;

    //// フェードアウトの速さを保存
    //[SerializeField, Range(0f, 10f)] private float _logoFadeSpeed = 0.1f;


    //「FadeSystem」のインスタンスを生成
    private FadeManager _fadeSystem ;
    //「TranstionScenes」のインスタンスを生成
    [HideInInspector] public TranstionScenes transSystem;

    //「ui_fadeImage」のコンポーネントを保存
    private Image _fadeImage;
    //「ui_titleImage」のコンポーネントを保存
    private Image _titleImage;
    // 「ui_startButton」と「ui_endButton」をゲームオブジェクトとして保存
    private GameObject[] _buttonObj = new GameObject[2];
    //「TitleUI」を親オブジェクトとして保存
    private GameObject _parent;
    // カメラを動かすために「MainCamera」をゲームオブジェクトとして保存
    private GameObject _cameraObj;
    //「TitleCanvas」をゲームオブジェクトとして保存
    private GameObject _titleCanvas;

    // 「_distance」は初期位置と移動先の距離を保存
    // 「_positionValue」は２点間の移動する位置の値を保存
    // 「&& _isInputButton」は現在のゲーム時間を保存
    private float _distance, _positionValue, _time;

    // スタートボタンが押されたかの判定を保存
    private bool _isClickButton = false;

    // ボタンの入力を受け付ける判定を保存
    private bool _isInputButton = false;

    private bool _isStepScene = false;

    private GameObject _saveButton;

    // Start is called before the first frame update
    void Start()
    {
        // タイトル画面のUIの初期化
        Initi_TitleUI();

        // スタートボタンが押された処理関係の初期化 
        Initi_TransFunction();

        FadeVariables.Initi_Fade();
    }

    // Update is called once per frame
    void Update()
    {
        // 選択中のボタンの情報を保存する
        _saveButton = EventSystem.current.currentSelectedGameObject;

        // タイトル画面のUIの演出をするコルーチンを呼び出す
        StartCoroutine("Fade_UI");

        if (_isClickButton&& _isInputButton)
        {
            Move_CameraObj(1);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button3) )
        {
            _isStepScene = true;

            // 現在のゲーム内の時間を変数に保存する
            _time = Time.time;

            // タイトル画面のUI表示を非表示にする
            _titleCanvas.SetActive(false);
        }

        if (_isStepScene && _isInputButton)
        {
            Move_CameraObj(2);
        }
    }


    // タイトル画面のUIの演出をするコルーチン
    private IEnumerator Fade_UI()
    {
        // 一番目に表示させる演出処理
        if (!FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // フェードインをする関数を呼び出す
            _fadeSystem.FadeIn(_fadeImage, _fadeImage.color.a);
        }

        // 二番目に表示させる演出処理
        if (FadeVariables.FadeIn && !FadeVariables.FadeOut)
        {
            // 処理を待つ
            yield return new WaitForSeconds(_intervalTIme[0]);

            // フェードアウトをする関数を呼び出す
            _fadeSystem.FadeOut(_titleImage, _titleImage.color.a);
        }

        // 三番目に表示させる演出処理
        if (FadeVariables.FadeIn && FadeVariables.FadeOut)
        {
            // 処理を待つ
            yield return new WaitForSeconds(_intervalTIme[1]);

            // ボタンを表示させる処理
            for (int i = 0; i < _buttonObj.Length; i++)
            {
                _buttonObj[i].SetActive(true);
            }

            _isInputButton = true;
        }
    }
    void Initi_TransFunction()
    {
        // カメラの初期位置を変数に保存する
        _startPosition = _cameraObj.transform.position;

        // 初期位置と移動先の位置同士の距離の長さを変数に保存する
        _distance = Vector3.Distance(_startPosition, _endPosition);

        // スタートボタンが押された判定を無効にする
        _isClickButton = false;
    }

    void Initi_TitleUI()
    {
        //「TitleCanvas」をタグ検索から取得する
        _titleCanvas = GameObject.Find("TitleCanvas").gameObject;

        //「ui_fadeImage」のコンポーネントを取得する
        _fadeImage = GameObject.Find("ui_fadeImage").GetComponent<Image>();

        // タイトル画面のUIの親オブジェクト「TitleUI」をタグ検索から取得する
        _parent = GameObject.FindWithTag("TitleUI");

        // 「ui_titleImage」のコンポーネントを取得する
        _titleImage = _parent.GetComponentInChildren<Image>();

        //「ui_startButton」をゲームオブジェクトして取得する
        _buttonObj[0] = _parent.transform.GetChild(1).gameObject;

        //「ui_endButton」をゲームオブジェクトとして取得する
        _buttonObj[1] = _parent.transform.GetChild(2).gameObject;

        //「MainCamera」をゲームオブジェクトとして保存する
        _cameraObj = GameObject.Find("Main Camera").gameObject;

        // ボタンの表示を無効にする
        for (int i = 0; i < _buttonObj.Length; i++)
        {
            _buttonObj[i].SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(_buttonObj[0]);
    }

    public void OnClick_StartButton()
    {
        // タイトル画面のUI表示を非表示にする
        _titleCanvas.SetActive(false);

        // スタートボタンが押された判定を有効にする
        _isClickButton = true;

        // 現在のゲーム内の時間を変数に保存する
        _time = Time.time;
    }

    private void Move_CameraObj(int _seceneNumber)
    {
        // 初期位置と移動先の距離の割合を計算する処理
        // 「(&& _isInputButton.&& _isInputButton - && _isInputButton) / _distance」は距離の長さを100として見て時間経過で距離の長さを割ることで２点の移動距離を指定する値を求める。
        _positionValue = ((Time.time-_time) / _distance) * _cameraMoveSpeed;

        // カメラの位置を動かす処理
        _cameraObj.transform.position = Vector3.Lerp(_startPosition, _endPosition, _positionValue);

        // カメラの位置が指定した位置に来た場合
        if (_cameraObj.transform.position == _endPosition)
        {
            // スタートボタンが押された判定を無効にする
            _isClickButton = false;

            // チュートリアルのシーンに遷移する
            transSystem.Trans_Scene(_seceneNumber);
        }
    }
}
