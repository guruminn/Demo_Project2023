using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoalSystem : MonoBehaviour
{
    [SerializeField] SystemEventManager eventManager;
    [SerializeField] GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PLAYER")
        {
            panel.SetActive(true);
            eventManager.gameClear = true;
        }
    }
}
