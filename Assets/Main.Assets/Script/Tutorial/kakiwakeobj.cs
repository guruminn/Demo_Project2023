using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakiwakeobj : MonoBehaviour
{
    kakiwakeManager kakiwakeManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            kakiwakeManager.hitolist.Add(other.gameObject);
        }
    }

}
