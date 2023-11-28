using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 作成者：山﨑晶
// ゲームクリアに関するソースコード

public class GameClearManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// 「FadeManager」を参照
    /// </summary>
    private FadeManager _fadeManager;

    /// <summary>
    /// 「TranstionScenes」を参照
    /// </summary>
    private TranstionScenes _transSystem;

    /// <summary>
    /// 背景のフェードアウトの設定
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _backGroundFadeOut;

    /// <summary>
    /// チェキのフェードアウトの設定
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _chekiFadeOut;

    /// <summary>
    /// 画面終了時のフェードアウトの設定
    /// </summary>
    [SerializeField]
    private FadeManager.FadeSetting _endFadeOut;

    /// <summary>
    /// アイドルの画像を保存する変数
    /// </summary>
    [SerializeField]
    private RectTransform[] _idolImage = new RectTransform[2];

    /// <summary>
    /// アイドル画像の初期位置を保存する変数
    /// </summary>
    private Vector2[] _startPosition = new Vector2[2];

    /// <summary>
    /// アイドル画像の移動先を保存する変数
    /// </summary>
    [SerializeField]
    private  Vector2[] _endPosition = new Vector2[2];

    /// <summary>
    /// アイドル画像の初期位置と移動先の距離を保存する変数
    /// </summary>
    private float[] _distance = new float[2];

    /// <summary>
    /// 時間を保存する変数
    /// </summary>
    private float _time;

    /// <summary>
    /// アイドル画像を動かす速さを保存する変数
    /// </summary>
    [SerializeField,Range(0f, 100f)]
    private float _moveSpeed;

    /// <summary>
    /// プレイヤーの画像を取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerImage;

    /// <summary>
    /// カウントダウンの時間を保存する変数
    /// </summary>
    [SerializeField,Range(0,10f)]
    private float _countDownTime;

    /// <summary>
    /// カウントダウンを表示するテキストオブジェクトを取得する変数
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _countText;

    /// <summary>
    /// カウントダウンの時間をint型で保存する変数
    /// </summary>
    private int _uiCount;

    /// <summary>
    /// 終盤に表示するテキストのオブジェクトを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _lastText;

    /// <summary>
    /// チェキを撮った後の待機時間を保存する変数
    /// </summary>
    [SerializeField,Range(0f,10f)]
    private float _changeSpeed;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        Initi_UI();
    }

    // Update is called once per frame
    void Update()
    {
        direction_UI();
    }

    /// <summary>
    /// 初期の関数
    /// </summary>
    private void Initi_UI()
    {
        // アイドルの位置を初期化
        for (int i = 0; i < _idolImage.Length; i++)
        {
            _startPosition[i] = _idolImage[i].anchoredPosition;
            _distance[i] = Vector2.Distance(_startPosition[i], _endPosition[i]);
        }

        // カウントダウンを初期化
        _uiCount = 0;
    }

    /// <summary>
    /// UIの演出関数
    /// </summary>
    private void direction_UI()
    {
        switch (_uiCount)
        {
            // 背景画像をフェードする
            case 0: 
                _fadeManager.FadeOut(_backGroundFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _uiCount++;
                }
                break;
            
            // チェキをフェードする
            case 1:               
                _fadeManager.FadeOut(_chekiFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _time = Time.time;
                    _uiCount++;
                }
                break;

            // アイドル画像を横にスライドする
            case 2:               
                Move_IdolImage();
                break;

            // プレイヤー画像を表示する
            case 3:
                _playerImage.SetActive(true);
                _uiCount++;
                break;

            // カウントダウンを表示する
            case 4:
                CountDown_Text();
                break;

            // チェキを撮影する
            case 5:
                StartCoroutine(ShotPhoto());
                break;

            // 画面終了時のフェードする
            case 6:
                _fadeManager.FadeOut(_endFadeOut);
                if (FadeManager.fadeOut)
                {
                    FadeManager.fadeOut = false;
                    _transSystem.Trans_Scene(0);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// アイドル画像を動かす関数
    /// </summary>
    private void Move_IdolImage()
    {
        float _positionValue;

        for (int i = 0; i < _idolImage.Length; i++)
        {
            // 初期位置と移動先の距離の割合を計算する処理
            // 「(Time.time - time) / _distance」は距離の長さを100として見て時間経過で距離の長さを割ることで２点の移動距離を指定する値を求める。
            _positionValue = ((Time.time - _time) / _distance[i]) * _moveSpeed;

            // アイドル画像の位置を動かす処理
            _idolImage[i].anchoredPosition = Vector2.Lerp(_startPosition[i], _endPosition[i], _positionValue);

            // アイドル画像の位置が指定した位置に来た場合
            if ((_idolImage[0].anchoredPosition == _endPosition[0]) && (_idolImage[1].anchoredPosition == _endPosition[1]))
            {
                _lastText.SetActive(false);
                _uiCount++;
            }
        }
    }

    /// <summary>
    /// カウントダウンを演出する関数
    /// </summary>
    private void CountDown_Text()
    {
        // 時間を保存する変数
        int _countDownText;

        // 時間を保存する
        _countDownTime -= Time.deltaTime;
        
        // 少数付きの時間を整数に変換する
        _countDownText = (int)_countDownTime;

        // カウントダウンの時間を表示
        _countText.text = (_countDownText + 1).ToString();

        // カウントダウンの時間が０より小さくなった場合
        if ((int)_countDownTime < 0)
        {
            _countText.enabled = false;
            _uiCount++;
        }
    }

    /// <summary>
    /// チェキを撮る演出の関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotPhoto()
    {
        // SEが鳴っていなかった場合
        if (AudioManager.audioManager.CheckPlaySound(AudioManager.audioManager.seAudioSource))
        {
            AudioManager.audioManager.Play_SESound(SESoundData.SE.Shutters);
        }

        // 待ち時間
        yield return new WaitForSeconds(_changeSpeed);

        // テキストを表示する
        _lastText.SetActive(true);

        // UIのカウントを進める
        if (_uiCount == 5)
        {
            _uiCount++;
        }
        
        // 終了
        yield return null;
    }

    #endregion ---Methods---
}
