using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

// 作成者：地引翼

public class FadeController : MonoBehaviour
{
    // 不要なので消しました。by山﨑晶
    //AudioSource audioSource;
    //[Tooltip("ここにブザー音を入れる")]
    //[SerializeField] AudioClip buzzerClip;
    [Tooltip("いじらない")]
    [SerializeField] UITimer _timer;
    [Tooltip("いじらない")]
    [SerializeField] AroundGuardsmanController _controller;

    // フェードインにかかる時間（秒）★変更可
    [Tooltip("フェードインにかかる時間")]
    [SerializeField] const float fade_time = 1.0f;

    // ループ回数（0はエラー）★変更可
    [Tooltip("ループ回数、数が多いと滑らかになる")]
    [SerializeField] const int loop_count = 60;

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] Image countdownImage;
    [SerializeField] Image fadePanel;

    public NavMeshAgent guardsman;

    float countdown = 4f;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        guardsman = guardsman.GetComponent<NavMeshAgent>();

        //UITimer,AroundGuardsmanControllerを一時停止する
        _timer.enabled = false;
        _controller.enabled = false;
        fadePanel.enabled = true;
        guardsman.enabled = false;


        // 不要なので消しました。by山﨑晶
        //audioSource = GetComponent<AudioSource>();

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
        // SEのブザー音を再生します。by山﨑晶
        //audioSource.PlayOneShot(buzzerClip);
        AudioManager.Instance.Play_SESound(SESoundData.SE.Buzzer);

        //終了まで待機
        // 曲が流れているかチェックする関数を呼び、曲が流れ終わったらこの関数は「false」の値を持つのでこの書き方にしています。by 山﨑晶
        yield return new WaitWhile(() => (!AudioManager.Instance.CheckPlaySound(AudioManager.Instance.seAudioSource)));

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

        countdownText.gameObject.SetActive(true);
        countdownImage.gameObject.SetActive(true);

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            countdownImage.fillAmount = countdown % 1.0f;
            count = (int)countdown;
            countdownText.text = count.ToString();

            if (countdown <= 0)
            {
                //UITimer,AroundGuardsmanControllerを再生する
                _timer.enabled = true;
                _controller.enabled = true;
                guardsman.enabled = true;

                countdownText.gameObject.SetActive(false);
                countdownImage.gameObject.SetActive(false);

                // BGMを再生する by山﨑晶
                AudioManager.Instance.Play_BGMSound(BGMSoundData.BGM.Main);

                yield break;
            }
            yield return null;
        }

    }
}
