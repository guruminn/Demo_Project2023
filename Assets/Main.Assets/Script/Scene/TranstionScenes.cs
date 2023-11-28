using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 作成者：山﨑晶
// シーンの遷移処理

public class TranstionScenes : MonoBehaviour
{
    public void Trans_Scene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // 現在のシーンを再読み込みする
    public void Trans_Retry()
    {
        SceneManager.LoadScene(2);
    }

    // ゲームを終了させる
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}
