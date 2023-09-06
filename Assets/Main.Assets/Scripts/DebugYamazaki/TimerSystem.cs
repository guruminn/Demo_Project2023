using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI time;
    //float timer;

    // Start is called before the first frame update
    void Start()
    {
        //timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //timer = Time.deltaTime;

        time.text = Mathf.Round(Time.time).ToString();
    }
}
