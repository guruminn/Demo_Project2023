using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 作成者：地引翼
// 足踏み（歩く）フェーズの制御

public class WalkManager : MonoBehaviour
{
    #region ---Fields---

    /// <summary>
    /// 足踏みした回数を表示するテキスト変数
    /// </summary>
    [SerializeField] TextMeshProUGUI _countText;

    /// <summary>
    /// パネルオブジェクト取得
    /// </summary>
    [SerializeField] GameObject _walkPanel;

    /// <summary>
    /// AudioManager参照するための変数
    /// </summary>
    [SerializeField] AudioManager _audioManager;

    /// <summary>
    /// StandStill参照するための変数
    /// </summary>
     [SerializeField] StandStill _standStill;

    /// <summary>
    /// TutorialManager参照するための変数
    /// </summary>
    [SerializeField] TutorialManager _tutorialManager;

    /// <summary>
    /// TutorialManager参照するための変数
    /// </summary>
    public int _clearCount = 3;

    /// <summary>
    /// 音が鳴り終わったか判定するbool
    /// </summary>
    bool isAudioEnd;

    /// <summary>
    /// SEを一度だけ再生させるbool
    /// </summary>
    bool SEflag = true;

    #endregion ---Fields---

    #region ---Methods---

    void OnEnable()
    {
        // ボイス再生
        _audioManager.PlaySESound(SEData.SE.WalkVoice);
    }

    void Update()
    {
        // 足踏みした回数をText表示
        _countText.text = _standStill.WalkCount.ToString();

        if(_standStill.WalkCount > _clearCount)
        {
            _countText.text = "OK";
        }
        if (SEflag && _standStill.WalkCount > _clearCount)
        {
            _audioManager.PlaySESound(SEData.SE.Correct);
            SEflag = false;
            isAudioEnd = true;
        }
        if (_audioManager.CheckPlaySound(_audioManager.seAudioSource) && isAudioEnd || Input.GetKeyDown(KeyCode.Space))
        {
            _walkPanel.SetActive(false);
            _tutorialManager._phaseCount++;
        }
    }
    #endregion ---Methods---
}
