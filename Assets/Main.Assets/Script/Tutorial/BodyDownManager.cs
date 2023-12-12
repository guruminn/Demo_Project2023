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
    public TextMeshProUGUI _countTimeText;
    // 音が鳴り終わったか
    private bool isAudioEnd;
    // 音が一度だけ再生するフラグ
    bool SEflag = true;
    // パネルを非表示にする
    [SerializeField] GameObject _bodyDownPanel;
    // audio付ける
    [SerializeField] AudioManager _audioManager;

    // アクティブになった時に呼び出される
    void OnEnable()
    {
        // しゃがんでみましょうボイス
        _audioManager.PlaySESound(SEData.SE.BodyDownVoice);
    }

    // Update is called once per frame
    void Update()
    {
        _countTimeText.text = _count.ToString("F1");
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
            _countTimeText.text = "OK";
        }

        if (SEflag && _count > 3)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd)
        {
            _bodyDownPanel.SetActive(false);
            gameObject.SetActive(false);
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
