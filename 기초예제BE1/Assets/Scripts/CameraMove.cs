using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerT;
    Vector3 offset;

    void Awake()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerT.position;
    }

    void LateUpdate()
    {
        transform.position = playerT.position + offset;
    }
}
