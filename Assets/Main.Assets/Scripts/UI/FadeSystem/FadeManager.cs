using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeVariables
{
    public static bool FadeOut;
    public static bool FadeIn;
}

public class FadeManager
{
    // フェードアウトをするスピードを保存する変数
    public float _fadeOutSpeed = 0.1f;
    public float _fadeInSpeed = 0.1f;

    // フェードアウトの演出をする関数
    public void FadeOut(Image _fadeImage, float _fadeColor, bool fadeType = false, float _defaultValue = 1)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.gameObject.SetActive(true);

        // 透明度を加算して上げる
        _fadeColor += _fadeOutSpeed * Time.deltaTime;

        switch (fadeType)
        {
            case false:
                // フェードアウトさせるパネルの透明度を設定する
                _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeColor);
                break;
            case true:
                // フェードアウトさせるパネルの透明度を設定する
                _fadeImage.color = new Color(_fadeColor, _fadeColor, _fadeColor, _fadeImage.color.a);
                break;
        }

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_fadeColor >= _defaultValue)
        {
            // パネルの透明度を固定する
            _fadeColor = _defaultValue;

            // フェードアウトの判定を有効にする
            FadeVariables.FadeOut = true;
        }
    }

    public void FadeIn(Image _fadeImage, float _fadeColor, bool fadeType = false, float _defaultValue = 0)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.enabled = true;

        // 透明度を加算して上げる
        _fadeColor += _fadeInSpeed * Time.deltaTime;

        switch (fadeType)
        {
            case false:
                // フェードアウトさせるパネルの透明度を設定する
                _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeColor);
                break;
            case true:
                // フェードアウトさせるパネルの透明度を設定する
                _fadeImage.color = new Color(_fadeColor, _fadeColor, _fadeColor, _fadeImage.color.a);
                break;
        }

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_fadeColor <= _defaultValue)
        {
            // パネルの透明度を固定する
            _fadeColor = _defaultValue;

            // フェードアウトさせるパネルを非表示する
            _fadeImage.gameObject.SetActive(false);

            // フェードインの判定を有効にする
            FadeVariables.FadeIn = true;
        }
    }
}
