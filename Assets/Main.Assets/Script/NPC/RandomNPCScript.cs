using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 観客をランダム生成
// 作成者：地引翼

public class RandomNPCScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成するGameObject")]
    GameObject _createPrefab;

    [SerializeField]
    [Tooltip("生成する範囲A")]
    Transform _rangeA;

    [SerializeField]
    [Tooltip("生成する範囲B")]
    Transform _rangeB;

    [SerializeField]
    [Tooltip("生成する個数")]
    int _pieces = 0;

    // Start is called before the first frame update
    void Start()
    {
        RundomNPC();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RundomNPC()
    {
        while (0 < _pieces)
        {
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            float _x = Random.Range(_rangeA.position.x, _rangeB.position.x);
            // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
            float _y = Random.Range(_rangeA.position.y, _rangeB.position.y);
            // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
            float _z = Random.Range(_rangeA.position.z, _rangeB.position.z);

            // 生成するオブジェクトの座標を保存する
            Vector3 _pos = new Vector3(_x, _y, _z);
            // 各軸についてのボックスサイズの半分を保存
            Vector3 _halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

            // 指定したボックスが他のコライダーと重なっているか確認
            if (!Physics.CheckBox(_pos, _halfExtents))
            {
                // GameObjectを上記で決まったランダムな場所に生成
                Instantiate(_createPrefab, new Vector3(_x, _y, _z), _createPrefab.transform.rotation);
            }
            else
            {
                continue;
            }
            _pieces--;
        }
    }
}
