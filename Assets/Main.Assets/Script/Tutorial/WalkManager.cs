using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class WalkManager : MonoBehaviour
{
    // カウントのテキスト
    public TextMeshProUGUI _Text;
    // 音が鳴り終わったか
    private bool isAudioEnd;

    bool SEflag = true;
    // audio付ける
    [SerializeField] AudioManager _audioManager;
    // パネルを非表示にする
    [SerializeField] GameObject _kakiwakePanel;

    void OnEnable()
    {
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
