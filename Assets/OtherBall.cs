using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBall : MonoBehaviour
{
    private MeshRenderer mesh;
    private Material mat;
    
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Ball")
            mat.color = new Color(0,0,0);
    }
    
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.name == "Ball")
            mat.color = new Color(1,1,1);
    }

}
