using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//  ? ?F R    
//  V [   ?J ?   

public class TranstionScenes : MonoBehaviour
{
    public void Trans_Scene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    //    ??V [     ???  ?   
    public void Trans_Retry()
    {
        SceneManager.LoadScene(2);
    }

    //  Q [     I        
    public void Trans_EndGame()
    {
        Application.Quit();
    }
}
