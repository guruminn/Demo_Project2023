//担当者：山﨑
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class SystemEventManager : MonoBehaviour
{
    // ゲームオーバー、クリアをこの変数でフラグ管理
    [HideInInspector]public bool gameOver;
    [HideInInspector]public bool gameClear;

    // Start is called before the first frame update
    void Start()
    {
        // フラグの初期化
        gameClear = false;
        gameOver = false;

        // フラグの確認用デバック
        Debug.Log("gameOver:" + gameOver);
        Debug.Log("gameClear:" + gameClear);
    }

    // Update is called once per frame
    void Update()
    {
        // フラグが「true」になった時の処理
        if (gameOver || gameClear)
        {
            // ゲーム内の時間を止める
            Time.timeScale = 0;

            // ゲーム内の時間が止まっているかの確認用デバック
            Debug.Log("Time : " + Time.timeScale);
        }
    }
}
