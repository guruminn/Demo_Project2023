using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  作成者：山�ｱ晶 
// ステージの最善にあるゲートと接触したときにゲームクリアにする処理

public class ClaerManager : MonoBehaviour
{
    // 値を参照するために取得する変数
    [SerializeField]
    private ValueSettingManager settingManager;

    //  クリア判定と当たった場合
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //  ゲームクリアの判定をする
            settingManager.gameClear = true;

            //Debug.Log("クリアになった");
        }
    }
}