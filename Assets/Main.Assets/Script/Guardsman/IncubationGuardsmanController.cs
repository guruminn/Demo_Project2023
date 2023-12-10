// 作成者：地引翼（廃棄予定）
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 潜伏型警備員の動き

public class IncubationGuardsmanController : MonoBehaviour
{
    [SerializeField]
    private ValueSettingManager settingManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ゲームオーバー当たり判定
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // ゲームオーバーの判定をtrueにする
            settingManager.gameOver = true;
            Debug.Log("ゲームオーバー");
        }
    }
}
