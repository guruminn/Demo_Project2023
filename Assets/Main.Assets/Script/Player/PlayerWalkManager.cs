using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 作成者：山﨑晶
// プレイヤーを足踏みで移動する処理のソースコード

public class PlayerWalkManager : MonoBehaviour
{
    #region ---Fields---
    /// <summary>
    /// Rigidbodyを取得する変数
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// カメラのオブジェクトを取得する
    /// </summary>
    [SerializeField]
    private GameObject _playerCamera;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyを参照する
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 進む動力を保存する
        float moveSpeed = StandStill.powerSource;

        // スティックの入力を保存する
        float stickHorizontal = Input.GetAxis("Horizontal");

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * stickHorizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        _rb.velocity = moveForward * moveSpeed + new Vector3(0, _rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    #endregion ---Methods---
}
