using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：山﨑晶
// プレイヤーのMocopiを使用した移動のソースコード

public class PlayerWalkManager : MonoBehaviour
{
    #region ---Fields---
    /// <summary>
    /// Rigidbodyを保存する変数
    /// </summary>
    private Rigidbody _rb;

    [SerializeField,Range(0,100)]
    private float _moveSpeed;

    /// <summary>
    /// プレイヤーのカメラを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerCamera;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody   Q ?   
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーを移動させる動力を保存 
        float moveSpeed = StandStill.powerSource;

        // ジョイスティックの入力を保存する
        float stickHorizontal = Input.GetAxis("Horizontal");

        // カメラの向きを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // カメラの向きかラプレイヤーの移動方向を設定する      
        Vector3 moveForward = cameraForward * stickHorizontal;

        // プレイヤーを移動させる
        _rb.velocity = moveForward * moveSpeed + new Vector3(0, _rb.velocity.y, 0);

        // プレイヤーの向きを変える
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    #endregion ---Methods---
}