using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class kakiwakehantei : MonoBehaviour
{
    static List<GameObject> hitolist = new List<GameObject>();
    // カウントのテキスト
    public TextMeshProUGUI _Text;

    bool SEflag = true;

    // サウンドのおおもと
    AudioSource _audioSource;

    // 正解サウンド
    public AudioClip _correct;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hitolist.Count);
        _Text.text = hitolist.Count.ToString();

        if(hitolist.Count > 5)
        {
            _Text.text = "OK";
        }
        if (SEflag && hitolist.Count > 5)
        {
            _audioSource.PlayOneShot(_correct);
            SEflag = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            hitolist.Add(other.gameObject);
        }
    }
}
