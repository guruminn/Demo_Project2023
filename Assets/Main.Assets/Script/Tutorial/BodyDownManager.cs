using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//　しゃがんだ時の判定

public class BodyDownManager : MonoBehaviour
{
    // 経過時間
    float _count;
    // しゃがみ出来ているかできていないか
    bool _active = true;
    // カウントのテキスト
    public TextMeshProUGUI _countText;
    // 正解サウンド
    [SerializeField] AudioClip _correctAudio;
    // アナウンスサウンド
    [SerializeField] AudioClip _announceAudio;
    // サウンドのおおもと
    AudioSource _audioSource;
    // 音が鳴り終わったか
    private bool isAudioEnd;
    // 音が一度だけ再生するフラグ
    bool SEflag = true;
    [SerializeField] GameObject _crouchPanel;

    void OnEnable()
    {
        // audio
        _audioSource = gameObject.GetComponent<AudioSource>();
        // しゃがんでみましょうボイス
        _audioSource.PlayOneShot(_announceAudio);
    }

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

        if(_count >3)
        {
            _countText.text = "OK";
        }

        if (SEflag && _count > 3)
        {
            _audioSource.PlayOneShot(_correctAudio);
            SEflag = false;

            isAudioEnd = true;
        }
        if (!_audioSource.isPlaying && isAudioEnd)
        {
            _crouchPanel.SetActive(false);
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
