using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Vector3 target = new Vector3(4,1,0);
    void Update()
    {
        //transform.position =
            //Vector3.MoveTowards(transform.position, target, 1f);

        //Vector3 velo = Vector3.zero;
        
        //transform.position =
            //Vector3.SmoothDamp(transform.position, target, ref velo, 1f);
            // transform.position =
            //     Vector3.Lerp(transform.position, target, 0.0 1f);
            transform.position =
                Vector3.Slerp(transform.position, target, 0.01f);
    }
}
