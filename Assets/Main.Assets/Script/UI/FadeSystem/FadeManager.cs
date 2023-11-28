using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeVariables
{
    public static bool FadeOut;
    public static bool FadeIn;

    public static void Initi_Fade()
    {
        FadeOut = false;
        FadeIn = false;
    }
}

public class FadeManager
{
    // フェードアウトの演出をする関数
    public void FadeOut(Image _fadeImage, float _fadeOutColor,float fadeSpeed=0.1f,bool fadeType = false, float _defaultValue = 1)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.gameObject.SetActive(true);

        // 透明度を加算して上げる
        _fadeOutColor += fadeSpeed * Time.deltaTime;

        switch (fadeType)
        {
            case false:
                // フェードアウトさせるパネルの透明度を設定する
                _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeOutColor);
                break;
            case true:
                // フェードアウトさせるパネルの透明度を設定する
                _fadeImage.color = new Color(_fadeOutColor, _fadeOutColor, _fadeOutColor, _fadeImage.color.a);
                break;
        }

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_fadeOutColor >= _defaultValue)
        {
            // パネルの透明度を固定する
            _fadeOutColor = _defaultValue;

            // フェードアウトの判定を有効にする
            FadeVariables.FadeOut = true;
        }
    }

    public void FadeIn(Image _fadeImage, float _fadeInColor, float fadeSpeed = 0.1f, bool fadeType = false, float _defaultValue = 0)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.enabled = true;

        // 透明度を減算して下げる
        _fadeInColor -= fadeSpeed * Time.deltaTime;

        switch (fadeType)
        {
            case false:
                // フェードアウトさせるパネルの透明度を設定する
                _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeInColor);
                break;
            case true:
                // フェードアウトさせるパネルの透明度を設定する
                _fadeImage.color = new Color(_fadeInColor, _fadeInColor, _fadeInColor, _fadeImage.color.a);
                break;
        }

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_fadeInColor <= _defaultValue)
        {
            // パネルの透明度を固定する
            _fadeInColor = _defaultValue;

            // フェードアウトさせるパネルを非表示する
            _fadeImage.gameObject.SetActive(false);

            // フェードインの判定を有効にする
            FadeVariables.FadeIn = true;
        }
    }
}
