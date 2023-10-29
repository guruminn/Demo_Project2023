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
    Vector3 velocity = Vector3.zero;
    // _posへ到達するまでのおおよその時間の変数
    public float smoothTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        // SmoothDamp(現在位置, 目的地, 現在の速度, target へ到達するまでのおおよその時間。値が小さいほど、target に速く到達)
        transform.position = Vector3.SmoothDamp(current, _pos, ref velocity, smoothTime);
    }
}
