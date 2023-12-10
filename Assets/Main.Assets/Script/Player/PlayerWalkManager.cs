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

    /// <summary>
    /// プレイヤーが動くスピードを保存する変数
    /// </summary>
    private float _moveSpeed;

    /// <summary>
    /// プレイヤーのカメラを取得する変数
    /// </summary>
    [SerializeField]
    private GameObject _playerCamera;

    /// <summary>
    /// 値を参照する変数
    /// </summary>
    [SerializeField]
    private ValueSettingManager setttingManager;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // 値を管理するアセットから値を参照して保存する
        _moveSpeed = setttingManager.MOCOPI_PlayerMoveSpeed;

        // Rigidbody   Q ?   
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーを移動させる動力を保存 
        float moveSpeed = StandStill.powerSource* _moveSpeed;
        //Debug.Log("modeSpeed : " + moveSpeed);

        // ジョイスティックの入力を保存する
        //float stickHorizontal = Input.GetAxis("Horizontal");
        //Debug.Log("stickHorizontal : " + stickHorizontal);

        // カメラの向きを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //Debug.Log("cameraForward : " + cameraForward);

        // カメラの向きかラプレイヤーの移動方向を設定する      
        //Vector3 moveForward = cameraForward * stickHorizontal;
        //Debug.Log("moveForward : " + moveForward);

        // プレイヤーを移動させる
        _rb.velocity = cameraForward * moveSpeed + new Vector3(0, _rb.velocity.y, 0);
        //Debug.Log(_rb.velocity);
    }

    #endregion ---Methods---
}