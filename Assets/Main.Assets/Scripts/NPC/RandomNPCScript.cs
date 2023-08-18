//作成者地引翼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNPCScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("生成するGameObject")]
    private GameObject createPrefab;

    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Transform rangeA;

    [SerializeField]
    [Tooltip("生成する範囲B")]
    private Transform rangeB;

    [SerializeField]
    [Tooltip("生成する個数")]
    private int pieces;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        while(0 < pieces)
        {
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
            float y = Random.Range(rangeA.position.y, rangeB.position.y);
            // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            Vector3 pos = new Vector3(x, y, z);
            Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

            if(!Physics.CheckBox(pos,halfExtents))
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
