using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// フレームレートの表示

public class FPSDisplay : MonoBehaviour
{
    // フレームレート
    float _fps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _fps = 1f / Time.deltaTime;
        //Debug.Log(_fps.ToString("F2"));
    }
}
