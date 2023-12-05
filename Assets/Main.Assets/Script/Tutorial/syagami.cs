using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

//　ビデオとテキストボイス流す

public class syagami : MonoBehaviour
{
    VideoClip _videoClip;
    GameObject screen;
    [SerializeField] TextMeshProUGUI _announceText;
    public AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        var videoPlayer = screen.AddComponent<VideoPlayer>();   // videoPlayeコンポーネントの追加

        videoPlayer.source = VideoSource.VideoClip; // 動画ソースの設定
        videoPlayer.clip = _videoClip;

        videoPlayer.isLooping = true;   // ループの設定
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AnnounceText("しゃがんでみましょう");
        audioSource.PlayOneShot(sound1);
    }
    public void VPControl()
    {
        var videoPlayer = GetComponent<VideoPlayer>();

        if (!videoPlayer.isPlaying) // ボタンを押した時の処理
            videoPlayer.Play(); // 動画を再生する。
        else
            videoPlayer.Pause();    // 動画を一時停止する。
    }
    public void AnnounceText(string comment)
    {
        _announceText.text = comment;
    }
}
