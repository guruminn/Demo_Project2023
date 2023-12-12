using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] _panel;
    public int _phaseCount;

    public GameObject _bodyDownobj; 
    public GameObject _kakiwakeobj; 
    public GameObject _kakiwakeobj_2; 
    public GameObject _mobobj; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _panel[_phaseCount].SetActive(true);

        if(_phaseCount == 1)
        {
            _bodyDownobj.SetActive(true);
        }
        if(_phaseCount == 2)
        {
            _kakiwakeobj.SetActive(true);
            _kakiwakeobj_2.SetActive(true);
            _mobobj.SetActive(true);
        }
        if(_phaseCount == 3)
        {

        }
    }
}
