using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject _head;
    public GameObject _lhand;
    public GameObject _rhand;
    public float _attackRange;

    float _distance;
    public Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _distance = _head.transform.position.y - _rhand.transform.position.y;
        //Debug.Log(_lhand.transform.position.y);
        //Debug.Log(_rhand.transform.position.y);
        //Debug.Log(_head.transform.position.y);
        //Debug.Log(_distance);
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        if ( _distance < _attackRange)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation
                | RigidbodyConstraints.FreezePositionY;

            //Debug.Log("aaaaaaaaaaa");
            //_attack.Attack();
        }
    }
}
