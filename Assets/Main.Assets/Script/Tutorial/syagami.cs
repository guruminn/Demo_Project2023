using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

//　しゃがんだ時の判定

public class syagami : MonoBehaviour
{
    // 経過時間
    float _count;
    // しゃがみ出来ているかできていないか
    bool _active = true;
    // カウントのテキスト
    public TextMeshProUGUI _countText;
    // 正解サウンド
    public AudioClip _correct;
    // サウンドのおおもと
    AudioSource _audioSource;
    // 音が鳴り終わったか
    private bool isAudioEnd;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _countText.text = _count.ToString("F1");
        if (_active)
        {
            _count += Time.deltaTime;
        }
        else
        {
            _count = 0;
        }

        if (_count > 3)
        {
            _countText.text = "OK";

            //ここに正解SE
            _audioSource.PlayOneShot(_correct);
            isAudioEnd = true;
        }
        if (!_audioSource.isPlaying && isAudioEnd)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _active = false;
    }
    private void OnTriggerExit(Collider other)
    {
        _active = true;
    }
}
