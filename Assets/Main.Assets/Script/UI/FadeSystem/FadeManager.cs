using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 作成者：山﨑晶
// フェードに関するソースコード

public class FadeManager:MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// フェードアウトの判定を保存する変数
    /// </summary>
    public static bool fadeOut;

    /// <summary>
    /// フェードインの判定を保存する変数
    /// </summary>
    public static bool fadeIn;

    #endregion ---Fields---

    #region ---Methods---

    /// <summary>
    /// フェードアウトの演出をする関数
    /// </summary>
    /// <param name="fade"> フェードを設定する構造体 </param>
    public void FadeOut(FadeSetting fade)
    {
        float fadeColor = fade.fadeImage.color.a;

        // フェードアウトさせるパネルを表示する
        fade.fadeImage.enabled = true;

        // 透明度を加算して上げる
        fadeColor += fade.fadeSpeed * Time.deltaTime;

        // フェードアウトさせるパネルの透明度を設定する
        fade.fadeImage.color = new Color(fade.fadeImage.color.r, fade.fadeImage.color.g, fade.fadeImage.color.b, fadeColor);

        // パネルの透明度が指定した透明度の値になった時の処理
        if (fadeColor >= fade.fadeAlpha)
        {
            // パネルの透明度を固定する
            fadeColor = fade.fadeAlpha;

            // フェードアウトの判定を有効にする
            fadeOut = true;
        }
    }

    /// <summary>
    /// フェードインを演出する関数
    /// </summary>
    /// <param name="fade"> フェードを設定する構造体 </param>
    public void FadeIn(FadeSetting fade)
    {
        float fadeColor = fade.fadeImage.color.a;

        // フェードアウトさせるパネルを表示する
        fade.fadeImage.enabled = true;

        // 透明度を減算して下げる
        fadeColor -= fade.fadeSpeed * Time.deltaTime;

        // フェードアウトさせるパネルの透明度を設定する
        fade.fadeImage.color = new Color(fade.fadeImage.color.r, fade.fadeImage.color.g, fade.fadeImage.color.b, fadeColor);

        // パネルの透明度が指定した透明度の値になった時の処理
        if (fadeColor <= fade.fadeAlpha)
        {
            // パネルの透明度を固定する
            fadeColor = fade.fadeAlpha;

            // フェードアウトさせるパネルを非表示する
            fade.fadeImage.enabled = false;

            // フェードインの判定を有効にする
            fadeIn = true;
        }
    }

    #endregion ---Methods---

    #region ---Struct---

    /// <summary>
    /// フェードを設定する構造体
    /// </summary>
    public struct FadeSetting
    {
        /// <summary>
        /// フェードの説明欄
        /// </summary>
        public string fadeName;

        /// <summary>
        /// フェードをする画像
        /// </summary>
        public Image fadeImage;

        /// <summary>
        /// フェードの速さ
        /// </summary>
        [Range(0f, 10f)]
        public float fadeSpeed;

        /// <summary>
        /// フェードの透明度
        /// </summary>
        [Range(0f,1f)]
        public float fadeAlpha;
    }

    #endregion ---Struct---
}
