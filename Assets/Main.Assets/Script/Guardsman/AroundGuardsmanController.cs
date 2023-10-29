using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // ナビゲーション使う際に必要
using UnityEngine.UI;  // UI関連に必要

// Navigationを用いた巡回型警備員の動き
// 作成者：地引翼

public class AroundGuardsmanController : MonoBehaviour
{
    // 
    int destPoint = 0;
    // GuardsmanのNavMeshAgent取得
    NavMeshAgent agent;

    // 警備員の中継ポイント
    [SerializeField] Transform[] points;

    [Tooltip("Playerのオブジェクト入れる")]
    [SerializeField] GameObject target;

    bool flag = false;

    [SerializeField] Image haken;

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent取得
        agent = GetComponent<NavMeshAgent>();

        //autoBraking を無効にすると目標地点の間を継続的に移動
        //つまり、エージェントは目標地点に近づいても速度を落とさない
        agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        //エージェントが現目標地点に近づいてきたら次の目標地点を選択
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        if (flag == true)
        {
            agent.destination = target.transform.position;
        }
        if (flag == false)
        {
            agent.destination = points[destPoint].position;
        }
    }

    void GotoNextPoint()
    {
        //地点がなにも設定されていないときに返す
        if (points.Length == 0)
        {
            return;
        }

        //エージェントが現在設定された目標地点に行くように設定
        agent.destination = points[destPoint].position;

        // 配列内の次の位置を目標地点に設定し必要ならば出発地点にもどる
        destPoint = (destPoint + 1) % points.Length;

        if (destPoint == 4)
        {
            destPoint = 0;
        }
    }

    //視界に入ったら追いかけてくる
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("視界入った");
            flag = true;
            haken.gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("視界でた");
            flag = false;
            haken.gameObject.SetActive(false);
        }
    }

    //プレイヤーに当たったらゲームオーバー
    //public void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        // ゲームオーバーの判定をtrueにする
    //        VariablesController.gameOverControl = true;

    //        Debug.Log("ゲームオーバー");
    //    }
    //}
}
