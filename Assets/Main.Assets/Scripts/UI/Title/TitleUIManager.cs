using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// タイトル画面の演出処理を記述したスクリプト
// 作成者：山﨑晶

public class TitleUIManager : MonoBehaviour
{
    // フェードさせる画像を取得
    [SerializeField] private Image fadePanel;
    // フェードさせる画像をオブジェクトとして取得
    [SerializeField] private GameObject fadePanelObj;

    [Space(10)]

    // タイトルロゴを取得
    [SerializeField] private Image titleImage;

    [Space(10)]

    // ボタンをオブジェクトとして取得
    [SerializeField] private GameObject[] buttonObj = new GameObject[2];

    [Space(10)]

    // フェードアウトをするスピードを保存する変数
    [SerializeField, Range(0f, 1f)] private float _fadeSpeed;

    [Space(10)]

    // 画像を表示する間隔の時間を保存する変数
    [SerializeField] private float[] _waitTime = new float[2];

    // フェードをする画像の透明度を保存する変数
    private float _fadeAlpha;

    // タイトルロゴの透明度を保存する変数
    private float _titleAlpha;

    // フェードアウトの判定を管理する変数
    private bool _isFadeOut;

    // フェードインの判定を管理する変数
    private bool _isFadeIn;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        // フェードアウト/フェードインの判定を無効にする
        _isFadeOut = false;
        _isFadeIn = false;

        // ボタンの表示を無効にする
        for (int i = 0; i < buttonObj.Length; i++)
        {
            buttonObj[i].SetActive(false);
        }

        // フェードする画像の透明度を保存する
        _fadeAlpha = fadePanel.color.a;
        // タイトルロゴの透明度を保存する
        _titleAlpha = titleImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        // タイトル画面のUIの演出をするコルーチンを呼び出す
        StartCoroutine("Fade_UI");
    }

    // タイトル画面のUIの演出をするコルーチン
    private IEnumerator Fade_UI()
    {
        // 一番目に表示させる演出処理
        if (!_isFadeOut && !_isFadeIn)
        {
            // フェードインをする関数を呼び出す
            FadeIn(fadePanel);
        }

        // 二番目に表示させる演出処理
        if (_isFadeIn && !_isFadeOut)
        {
            // フェードする画像を非表示にする
            fadePanelObj.SetActive(true);

            // 処理を待つ
            yield return new WaitForSeconds(_waitTime[0]);

            // フェードアウトをする関数を呼び出す
            FadeOut(titleImage);
        }

        // 三番目に表示させる演出処理
        if (_isFadeIn && _isFadeOut)
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

    // フェードアウトの演出をする関数
    private void FadeOut(Image _fadeImage)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.enabled = true;

        // 透明度を加算して上げる
        _titleAlpha += _fadeSpeed * Time.deltaTime;

        // フェードアウトさせるパネルの透明度を設定する
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _titleAlpha);

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_titleAlpha >= 1)
        {
            // パネルの透明度を固定する
            _titleAlpha = 1;

            // フェードアウトの判定を有効にする
            _isFadeOut = true;
        }
    }

    private void FadeIn(Image _fadeImage)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.enabled = true;

        // 透明度を加算して上げる
        _fadeAlpha -= _fadeSpeed * Time.deltaTime;

        // フェードアウトさせるパネルの透明度を設定する
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeAlpha);

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_fadeAlpha <= 0)
        {
            // パネルの透明度を固定する
            _fadeAlpha = 0;

            // フェードインの判定を有効にする
            _isFadeIn = true;
        }
    }
}
