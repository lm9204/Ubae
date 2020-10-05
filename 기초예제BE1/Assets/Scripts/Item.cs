using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotateSpeed = 120;
    void Update()
    {
        transform.Rotate(Vector3.left* rotateSpeed * Time.deltaTime, Space.World);
    }

    
}
