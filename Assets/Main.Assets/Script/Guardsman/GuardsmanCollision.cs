using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 警備員のゲームオーバー判定
// 作成者：地引翼

public class GuardsmanCollision : MonoBehaviour
{
    // 値を管理するアセットから値を参照する
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
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // ゲームオーバーの判定をtrueにする
            settingManager.gameOver = true;

            Debug.Log("ゲームオーバー");
        }
    }
}
