using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class kakiwake : MonoBehaviour
{
    // サウンドのおおもと
    AudioSource _audioSource;
    // アナウンスのサウンド
    [SerializeField] AudioClip _AnnounceSound;


    void OnEnable()
    {
        // audio
        _audioSource = gameObject.GetComponent<AudioSource>();
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
}
