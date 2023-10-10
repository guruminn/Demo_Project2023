using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeVariables
{
    public static bool FadeOut;
    public static bool FadeIn;
}

public class FadeManager : MonoBehaviour
{
    // フェードアウトをするスピードを保存する変数
    [SerializeField, Range(0f, 10f)] private float _fadeOutSpeed = 1f;
    [SerializeField, Range(0f, 10f)] private float _fadeInSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        // フェードアウト/フェードインの判定を無効にする
        FadeVariables.FadeOut = false;
        FadeVariables.FadeIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // フェードアウトの演出をする関数
    public void FadeOut(Image _fadeImage,float _fadeAlpha)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.enabled = true;

        // 透明度を加算して上げる
        _fadeAlpha += _fadeOutSpeed * Time.deltaTime;

        // フェードアウトさせるパネルの透明度を設定する
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeAlpha);

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_fadeAlpha >= 1)
        {
            // パネルの透明度を固定する
            _fadeAlpha = 1;

            // フェードアウトの判定を有効にする
            FadeVariables.FadeOut = true;
        }
    }

    public void FadeIn(Image _fadeImage,float _fadeAlpha)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.enabled = true;

        // 透明度を加算して上げる
        _fadeAlpha -= _fadeInSpeed * Time.deltaTime;

        // フェードアウトさせるパネルの透明度を設定する
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeAlpha);

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_fadeAlpha <= 0)
        {
            // パネルの透明度を固定する
            _fadeAlpha = 0;

            // フェードアウトさせるパネルを非表示する
            _fadeImage.enabled = false;

            // フェードインの判定を有効にする
            FadeVariables.FadeIn = true;
        }
    }
}
