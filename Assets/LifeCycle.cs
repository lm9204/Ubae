using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
   void Update()
   {
      Vector3 vec = new Vector3(
         Input.GetAxis("Horizontal"),
         Input.GetAxis("Vertical"),
         0
         );
      transform.Translate(vec);
   }
}
