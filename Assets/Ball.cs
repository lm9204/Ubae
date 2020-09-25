using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        //rigid.velocity = new Vector3(2, 0 , 0);
        // if(Input.GetButtonDown("Jump"))
        //     rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
        //
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        //
        rigid.AddForce(vec, ForceMode.Impulse);
        
         //rigid.AddTorque(Vector3.up);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.name == "Cube")
            rigid.AddForce(Vector3.up * 3, ForceMode.Impulse);
    }
}
