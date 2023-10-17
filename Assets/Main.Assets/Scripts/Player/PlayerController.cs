//プレイヤーの動き
//前後移動と左右回転,しゃがむ代わりにオブジェクト小さく
//デバッグ用
//作成者つばさ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Rot = 0.0f;
    public float rotateSpeed = 0; //回転スピード
    public float PositionSpeed = 0.0f; //前後移動スピード
    public Vector3 scale; //小さくした時の大きさ


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //左右回転の数値取得
        Rot = Input.GetAxis("Horizontal");

        //前後移動
        if (Input.GetKey(KeyCode.JoystickButton2))
        {
            transform.position -= transform.forward * PositionSpeed;

        }
        if (Input.GetKey(KeyCode.JoystickButton1))
        {
            transform.position += transform.forward * PositionSpeed;
        }

        //これは回転
        if (Rot > 0.0f)
        {
            transform.Rotate(new Vector3(0, Rot + rotateSpeed, 0));
        }
        if (Rot < 0.0f)
        {
            transform.Rotate(new Vector3(0, Rot - rotateSpeed, 0));
        }

        //スペースキーを押したときに大きさ変わる
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
