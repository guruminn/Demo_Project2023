using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：地引翼

public class PlayerController : MonoBehaviour
{
    // 左右回転の数値取得する変数
    float Rot = 0.0f;
    // 回転スピードを取得する変数
    public float RotateSpeed = 0.0f;
    // 前後移動スピードを取得する変数
    public float PositionSpeed = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 左右回転の数値取得
        Rot = Input.GetAxis("Horizontal");
        //Debug.Log(Rot);

        // 回転
        transform.Rotate(new Vector3(0, Rot * RotateSpeed, 0));

        // 前後移動
        // 前
        if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * PositionSpeed;
        }
        // 後ろ
        if (Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * PositionSpeed;
        }
    }
}
