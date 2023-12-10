using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    // サウンドのおおもと
    AudioSource _audioSource;
    // アナウンスのテキスト
    [SerializeField] TextMeshProUGUI _announceText;
    // アナウンスのサウンド
    [SerializeField] AudioClip _AnnounceSound;
    // アナウンスする文字の表示
    [SerializeField] string _Text;

    public int phase = 0;

    void OnEnable()
    {
        // audio
        _audioSource = gameObject.GetComponent<AudioSource>();
        AnnounceText(_Text);
        _audioSource.PlayOneShot(_AnnounceSound);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void AnnounceText(string comment)
    {
        _announceText.text = comment;
    }
}
