using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  作成者：山﨑晶 
// ステージの最善にあるゲートと接触したときにゲームクリアにする処理

public class ClaerManager : MonoBehaviour
{
    //  クリア判定と当たった場合
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //  ゲームクリアの判定をする
            OutGameManager.gameClear = true;

            //Debug.Log("クリアになった");
        }
    }
}