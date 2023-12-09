using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

// テキスト、ボイス、ビデオを流す

public class TutorialManager : MonoBehaviour
{
    // サウンドのおおもと
    [System.NonSerialized] public AudioSource _audioSource;
    // アナウンスのテキスト
    [SerializeField] TextMeshProUGUI _announceText;
    // アナウンスのサウンド
    [SerializeField] AudioClip[] _AnnounceSound;
    // アナウンスする文字の表示
    [SerializeField] string[] _Text;

    // Start is called before the first frame update
    void Start()
    {
        // audio
        _audioSource = gameObject.GetComponent<AudioSource>();
        AnnounceText(_Text[0]);
        _audioSource.PlayOneShot(_AnnounceSound[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnnounceText(string comment)
    {
        _announceText.text = comment;
    }
}
