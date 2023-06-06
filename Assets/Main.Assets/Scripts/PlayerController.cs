//プレイヤーの挙動
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Rot = 0.0f;
    public int RotateSpeed = 0;
    public GameObject ArmLeft;
    public GameObject ArmRight;

    public float Speed = 0.0f;
    public float PositionSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rot = Input.GetAxis("Horizontal");
        //Debug.Log(Rot);

        //A,Dキーで左右に振り向く
        //RotateSpeedで回転の速度を変えれる
        //前進は進めるエリアができたら進むにしたほうがいいと思ったからキー入力で前後移動はいれてない

        //デバッグ用に前後移動やっぱりつけた
        if(Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * PositionSpeed;
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * PositionSpeed;
        }

        //これは回転
        if (Rot > 0.0f)
        {
            transform.Rotate(new Vector3(0, Rot + RotateSpeed, 0));
        }
        if(Rot < 0.0f)
        {
            transform.Rotate(new Vector3(0, Rot - RotateSpeed, 0));
        }

        //これは腕
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ArmLeft.transform.Rotate(new Vector3(0, -Speed, 0));
            ArmRight.transform.Rotate(new Vector3(0, Speed, 0));
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ArmLeft.transform.Rotate(new Vector3(0, Speed, 0));
            ArmRight.transform.Rotate(new Vector3(0, -Speed, 0));
        }
    }
}
