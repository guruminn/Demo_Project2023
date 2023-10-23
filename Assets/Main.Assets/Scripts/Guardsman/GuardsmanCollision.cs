using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardsmanCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void OnTriggerEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        // ゲームオーバーの判定をtrueにする
    //        VariablesController.gameOverControl = true;

    //        Debug.Log("ゲームオーバー");
    //    }
    //}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // ゲームオーバーの判定をtrueにする
            VariablesController.gameOverControl = true;

            Debug.Log("ゲームオーバー");
        }
    }
}
