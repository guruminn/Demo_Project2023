//担当者：山﨑晶
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
// シーンの遷移処理
public class TranstionScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 『Title』シーンに遷移する
    public void Trans_Title()
    {
        SceneManager.LoadScene(0);
    }

    // 『WayOfPlaying』シーンに遷移する
    public void Trans_WayPlay()
    {
        SceneManager.LoadScene(1);
    }

    // 『Main』シーンに遷移する
    public void Trans_Main()
    {
        SceneManager.LoadScene(2);
    }

    // 現在のシーンを再読み込みする
    public void Trans_Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ゲームを終了させる
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}
