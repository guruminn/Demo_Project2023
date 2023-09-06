//作成地引
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip buzzerClip;
    [SerializeField] UITimer _timer;
    [SerializeField] AroundGuardsmanController _controller;

    // フェードインにかかる時間（秒）★変更可
    [SerializeField] const float fade_time = 1.0f;

    // ループ回数（0はエラー）★変更可
    [SerializeField] const int loop_count = 50;


    // Start is called before the first frame update
    void Start()
    {
        //UITimer,AroundGuardsmanControllerを一時停止する
        _timer.enabled = false;
        _controller.enabled = false;

        audioSource = GetComponent<AudioSource>();

        //フェードインコルーチンスタート
        StartCoroutine("Color_FadeIn");

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Color_FadeIn()
    {
        //音楽を鳴らす
        audioSource.PlayOneShot(buzzerClip);

        //終了まで待機
        yield return new WaitWhile(() => audioSource.isPlaying);

        // 画面をフェードインさせるコールチン

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード元の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        //UITimer,AroundGuardsmanControllerを再生する
        _timer.enabled = true;
        _controller.enabled = true;
    }
}
