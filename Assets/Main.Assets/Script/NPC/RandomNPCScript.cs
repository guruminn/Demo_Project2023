using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 観客をランダム生成
// 作成者：地引翼

public class RandomNPCScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成するGameObject")]
    GameObject createPrefab;

    [SerializeField]
    [Tooltip("生成する範囲A")]
    Transform rangeA;

    [SerializeField]
    [Tooltip("生成する範囲B")]
    Transform rangeB;

    [SerializeField]
    [Tooltip("生成する個数")]
    int pieces = 0;

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
        while (0 < pieces)
        {
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
            float y = Random.Range(rangeA.position.y, rangeB.position.y);
            // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            // 生成するオブジェクトの座標を保存する
            Vector3 pos = new Vector3(x, y, z);
            // 各軸についてのボックスサイズの半分を保存
            Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

            // 指定したボックスが他のコライダーと重なっているか確認
            if (!Physics.CheckBox(pos, halfExtents))
            {
                // GameObjectを上記で決まったランダムな場所に生成
                Instantiate(createPrefab, new Vector3(x, y, z), createPrefab.transform.rotation);
            }
            else
            {
                continue;
            }
            pieces--;
        }
    }
}
