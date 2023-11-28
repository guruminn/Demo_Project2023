using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ? ?F R    
//  v   C   [ ??  ???    ËY   ?\ [ X R [ h

public class PlayerWalkManager : MonoBehaviour
{
    #region ---Fields---
    /// <summary>
    /// Rigidbody   èÔ    ? 
    /// </summary>
    private Rigidbody _rb;

    [SerializeField,Range(0,100)]
    private float _moveSpeed;

    /// <summary>
    ///  J     ?I u W F N g   èÔ    
    /// </summary>
    [SerializeField]
    private GameObject _playerCamera;

    #endregion ---Fields---

    #region ---Methods---

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody   Q ?   
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 cameraForward = Vector3.Scale(_playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        //  i ?  ? ?     
        float moveSpeed = StandStill.powerSource;

        //Vector3 move = cameraForward * StandStill.powerSource;
        //  X e B b N ?  ? ?     
        float stickHorizontal = Input.GetAxis("Horizontal");

        _rb.velocity = transform.forward * 1 * _moveSpeed;
        //  J     ?       AX-Z   ??P ?x N g     èÔ
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        Debug.Log("PlayerWalkManager._rb.velocity : "+_rb.velocity);
        //      L [ ?  ?l ?J     ?       A ?           
        Vector3 moveForward = cameraForward * stickHorizontal;

        //transform.position += transform.forward * StandStill.powerSource * _moveSpeed;
        //  ?      ?X s [ h   |    B W     v Åù        ? ?A ?rY       ?  x x N g   ??  B
        _rb.velocity = moveForward * moveSpeed + new Vector3(0, _rb.velocity.y, 0);

        //Debug.Log("transform.position : " + transform.position);
        //  L     N ^ [ ?     i s      
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    #endregion ---Methods---
}