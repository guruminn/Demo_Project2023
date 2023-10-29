using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーが最前列に行ったときにクリア判定にする処理
// 作成者：山﨑晶

public class ClaerManager : MonoBehaviour
{
    // プレイヤーに当たった時の判定の処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ゲームクリアの判定をtrueにする
            VariablesController.gameClearControl = true;
            Debug.Log("クリアになったよ");
        }
    }
}
