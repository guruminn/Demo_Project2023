//çÏê¨é“ínà¯óÉ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private Vector3 _pos;
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        transform.position = Vector3.SmoothDamp(current, _pos, ref velocity, smoothTime);
    }
}
