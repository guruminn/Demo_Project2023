//潜伏型警備員の動き
//作成者つばさ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncubationGuardsmanController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ゲームオーバー");
        }
    }
}
