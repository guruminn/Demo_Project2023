using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static FadeManager;


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

[System.Serializable]
public class FadeManager
{
    public  enum FadeType
    {
        Alpha,
        Color,
    }

    public float FadeOutSpeed;
    public float FadeInSpeed;

    // フェードアウトの演出をする関数
    public void FadeOut(Image _fadeImage, float _fadeColor, FadeType fadeType = FadeType.Alpha, float _defaultValue = 1)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.gameObject.SetActive(true);

        // 透明度を加算して上げる
        _fadeColor += FadeOutSpeed * Time.deltaTime;

        _fadeImage.color = SetColor(fadeType, _fadeImage, _fadeColor);

        // パネルの透明度が指定した透明度の値になった時の処理
        if (_fadeColor >= _defaultValue)
        {
            // パネルの透明度を固定する
            _fadeColor = _defaultValue;

            // フェードアウトの判定を有効にする
            FadeVariables.FadeOut = true;
        }
    }

    public void FadeIn(Image _fadeImage, float _fadeColor, FadeType fadeType = FadeType.Alpha, float _defaultValue = 0)
    {
        // フェードアウトさせるパネルを表示する
        _fadeImage.enabled = true;

        // 透明度を減算して下げる
        _fadeColor -= FadeInSpeed * Time.deltaTime;

        _fadeImage.color = SetColor(fadeType, _fadeImage, _fadeColor);

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

    private Color SetColor(FadeType fadeType, Image _fadeImage, float _fadeColor)
    {
        switch (fadeType)
        {
            case FadeType.Alpha:
                // フェードアウトさせるパネルの透明度を設定する
                return new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeColor);
            case FadeType.Color:
                // フェードアウトさせるパネルの透明度を設定する
                return new Color(_fadeColor, _fadeColor, _fadeColor, _fadeImage.color.a);
            default:
                return Color.clear;
        }
    }
}
