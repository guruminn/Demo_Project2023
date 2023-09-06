// 作成者：山﨑晶
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OutSystemManager : MonoBehaviour
{
    // オーバーまたはクリアのフラグが「true」になったときに表示するパネル
    [SerializeField, Tooltip("「げーむくりあ」を表示するパネルをアタッチする")] private GameObject clearPanel;
    [SerializeField, Tooltip("「げーむおーばー」を表示するパネルをアタッチする")] private GameObject overPanel;

    // 「SystemEventManager」を呼び出し
    [SerializeField, Tooltip("「InGameSystem」のオブジェクトをアタッチする")] SystemEventManager systemManager;


    [SerializeField] UnityEvent systemEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (systemManager == null)
        {
            systemEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // オーバーまたはクリアのフラグが「true」だった場合、オーバーまたはクリアの画面を表示させる
        if (systemManager.gameClear)
        {
            clearPanel.SetActive(true);
        }
        else if (systemManager.gameOver)
        {
            overPanel.SetActive(true);
        }
    }

    // ゲームオーバーまたはゲームクリアの画面に配置されているボタンが押されたときの処理
    public void OnClick_Button()
    {
        // オーバーまたはクリアのフラグが「true」だった場合、オーバーまたはクリアの画面を非表示にさせてフラグを「false」にする
        if (systemManager.gameClear)
        {
            clearPanel.SetActive(false);
            systemManager.gameClear = false;
        }
        else if (systemManager.gameOver)
        {
            overPanel.SetActive(false);
            systemManager.gameOver = false;
        }

        // ゲーム内の時間を作動刺せる
        Time.timeScale = 1;

        //特定のシーンに遷移させる。
        systemEvent.Invoke();
    }
}
