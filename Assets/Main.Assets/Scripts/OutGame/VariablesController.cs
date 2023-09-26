using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲームオーバー、ゲームクリアの判定を管理するソースコード
// 作成者：山﨑

public class VariablesController : MonoBehaviour
{
    // ゲームオーバーの判定を管理する変数
    [HideInInspector] public static bool gameOverControl;

    // ゲームクリアの判定を管理する変数
    [HideInInspector] public static bool gameClearControl;

    // 初期化をする処理
    private void Awake()
    {
        gameOverControl = false;
        gameClearControl = false;
    }
}
