using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//‚µ‚á‚ª‚ß‚½‚©”»’è‚·‚é‚â‚Â

public class JudgeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _countText;
    float _count;
    bool _active = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _countText.text = _count.ToString();
        if( _active)
        {
            _count += Time.deltaTime;
        }
        else
        {
            _count = 0;
        }

        if(_count > 3)
        {
            _countText.text = "OK";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _active = false;
    }
    private void OnTriggerExit(Collider other)
    {
        _active = true;
    }
}
