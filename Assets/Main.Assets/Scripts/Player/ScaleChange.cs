//しゃがむ代わりにオブジェクト小さくした
//作成者つばさ
//デバッグ用
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    //小さくした時の大きさ
    public Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーを押したときに大きさ変わる
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
