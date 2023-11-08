using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 観客の挙動
// 作成者：地引翼

public class NPCManager : MonoBehaviour
{
    // このオブジェクトの座標を取得
    Vector3 _pos;
    // Vector3(0, 0, 0) を取得
    Vector3 _velocity = Vector3.zero;
    // _posへ到達するまでのおおよその時間の変数
    [Tooltip("値が小さいほど速くなる")]
    public float smoothTime = 0.1f;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _pos = this.transform.position;
        var _rb = GetComponent<Rigidbody>();

        //constraintをオンにする
        //_rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _current = transform.position;
        // SmoothDamp(現在位置, 目的地, 現在の速度, _target へ到達するまでのおおよその時間。値が小さいほど、_target に速く到達)
        transform.position = Vector3.SmoothDamp(_current, _pos, ref _velocity, smoothTime);
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Hand")
    //    {
    //        Debug.Log("当たった");
    //        _rb.constraints = RigidbodyConstraints.None;
    //    }
    //}
}
