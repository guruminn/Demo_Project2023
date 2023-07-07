using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPC_Movement : MonoBehaviour
{
    [SerializeField] public GameObject cheakPoint;  //チェックポイントを取得
    public bool moveLR;
    public bool comePoint;

    public float limitTime;
    private float npcSpeed = 1.0f;

    float length = 0.1f;
    float speed = 0.00001f;
    private Vector3 startPos;
    private Rigidbody rb;

    float saveTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //チェックポイントと現在の位置の角度を求める
        Quaternion lookRotation = Quaternion.LookRotation(cheakPoint.transform.position - this.transform.position);

        //現在の角度からチェックポイントまでの角度に回転する
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 0.1f);

        //進みたい方向に加える力
        Vector3 _npcFoward = new Vector3(0f, 0f, (npcSpeed * Time.deltaTime));

        //NPCを前進させる
        this.transform.Translate(_npcFoward);

        //saveTime += Time.deltaTime;

        //if (saveTime < limitTime)
        //{
        //    //チェックポイントと現在の位置の角度を求める
        //    Quaternion lookRotation = Quaternion.LookRotation(cheakPoint.transform.position - this.transform.position);

        //    //現在の角度からチェックポイントまでの角度に回転する
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 0.1f);

        //    //進みたい方向に加える力
        //    Vector3 _npcFoward = new Vector3(0f, 0f, (npcSpeed * Time.deltaTime));

        //    //NPCを前進させる
        //    this.transform.Translate(_npcFoward);
        //}
        //else
        //{
        //    npcSpeed *= -1.0f;
        //    saveTime = 0f;
        //}
    }
}
