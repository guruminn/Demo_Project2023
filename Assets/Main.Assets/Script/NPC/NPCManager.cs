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

    [SerializeField] GameObject _head;
    [SerializeField] GameObject _rhand;

    public float _attackRange;

    float _distance;


    // Start is called before the first frame update
    void Start()
    {
        _pos = this.transform.position;
        //Rigidbodyを取得
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _distance = _head.transform.position.y - _rhand.transform.position.y;


        Vector3 _current = transform.position;
        // SmoothDamp(現在位置, 目的地, 現在の速度, _target へ到達するまでのおおよその時間。値が小さいほど、_target に速く到達)
        transform.position = Vector3.SmoothDamp(_current, _pos, ref _velocity, smoothTime);
        //移動も回転もしないようにする
        _rb.constraints = RigidbodyConstraints.FreezeAll;

        if(Input.GetKey(KeyCode.JoystickButton14))
        {
            Attack();
        }
        if (_distance < _attackRange)
        {
            Attack();
        }
    }

    public void Attack()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation
                        | RigidbodyConstraints.FreezePositionY;

        //Debug.Log("攻撃");
    }
}
